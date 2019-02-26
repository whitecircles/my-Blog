using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tasks.Models;

namespace Tasks.Controllers
{
    public class HomeController : Controller
    {
        static string connection = ConfigurationManager.ConnectionStrings["Notesdb"].ConnectionString;
        SqlConnection conn = new SqlConnection(connection);

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public ActionResult PersonalPage(string name, string passwrd)
        {
            User usr = new User();
            usr.Name = name;
            usr.Passwrd = passwrd;

            string query = "select * from Users where @cPass = Passwrd and @cName = Name";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cPass", usr.Passwrd);
                cmd.Parameters.AddWithValue("@cName", usr.Name);


                               
                SqlDataReader reader = null;



                try
                {


                    reader = cmd.ExecuteReader();


                    User user = new User();
                    while (reader.Read())
                    {
                        user.Id = (int)reader["Id"];
                        user.Name = (string)reader["Name"];
                        user.Passwrd = (string)reader["Passwrd"];
                    }


                        if (user == null)
                            return View("OnError");
                        else 
                            return View(user);
                   
                }



                finally
                {
                    // close the reader
                    if (reader != null)
                    {
                        reader.Close();
                    }

                    // 5. Close the connection
                    if (conn != null)
                    {
                        conn.Close();
                    }
                }
            }

        }

        public ActionResult PersonalTasks(int usernumber)
        {
            SqlDataReader reader = null;
            List<Task> taskModel = new List<Task>();


            try
            {

                // 2. Open the connection
                conn.Open();



                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Notes where @usrn = userId", conn);

                SqlParameter nameParam = new SqlParameter("@usrn", usernumber);

                cmd.Parameters.Add(nameParam);


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var task = new Task();
                    task.Id = (int)reader["Id"];
                    task.note = (string)reader["note"];
                    task.isChecked = (bool)reader["isChecked"];
                    task.priority = (string)reader["priority"];
                    task.date = (string)reader["date"];
                    task.pendDate = (string)reader["pendDate"];
                    task.userId = (int)reader["userId"];
                    task.description = (string)reader["description"];




                    taskModel.Add(task);
                }
            }





            finally
            {
                // close the reader
                if (reader != null)
                {
                    reader.Close();
                }

                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return PartialView(taskModel);
        }

    }
}
