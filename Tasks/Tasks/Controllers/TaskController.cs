using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Tasks.Models;

namespace Tasks.Controllers
{
    public class TaskController : ApiController
    {
        static string connection = ConfigurationManager.ConnectionStrings["Notesdb"].ConnectionString;
        SqlConnection conn = new SqlConnection(connection);


        [HttpGet]
        [Route("api/bool/add/")]
        public string IsChecked(bool newValue, int taskId)
        {
            
            string query = "update Notes set isChecked = @cisChecked where Id = @cId;";
            using (SqlCommand cmd = new SqlCommand(query))
            {

                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cId", taskId);
                cmd.Parameters.AddWithValue("@cisChecked", newValue);

                cmd.ExecuteNonQuery();
                conn.Close();
            }
            return "success";
        }

        [HttpGet]
        [Route("api/task/{id}")]
        public JsonResult<List<Task>> GetAllNotes(int id)
        {
            SqlDataReader reader = null;
            List<Task> taskModel = new List<Task>();

            try
            {
                // 2. Open the connection
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Notes where userId = @cId", conn);

                SqlParameter nameParam = new SqlParameter("@cId", id);
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




            return Json(taskModel);
        }

        //it works
        [HttpGet]
        [Route("api/task/add/{note}/{isChecked}/{priority}/{date}/{pendDate}/{userId}/{description}")]
        public int InsertNote(string note, bool isChecked, string priority, string date, string pendDate, int userId, string description)
        {
            Task t = new Task();
            t.note = note;
            t.isChecked = isChecked;
            t.priority = priority;
            t.date = date;
            t.pendDate = pendDate;
            t.userId = userId;
            t.description = description;

            string query = "insert into Notes (note, isChecked, priority, date, pendDate, userId, description) values (@cnote, @cisChecked, @cpriority, @cdate, @cpendDate, @cuserId, @cdesc)";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cnote", t.note);
                cmd.Parameters.AddWithValue("@cisChecked", t.isChecked);
                cmd.Parameters.AddWithValue("@cpriority", t.priority);
                cmd.Parameters.AddWithValue("@cdate", t.date);
                cmd.Parameters.AddWithValue("@cpendDate", t.pendDate);
                cmd.Parameters.AddWithValue("@cuserId", t.userId);
                cmd.Parameters.AddWithValue("@cdesc", t.description);


                cmd.ExecuteNonQuery();


                conn.Close();

            }

           


                // 2. Open the connection
                conn.Open();

                SqlCommand cmd2 = new SqlCommand("select MAX(Id) from Notes", conn);


                int identifier = (int)cmd2.ExecuteScalar();



                // 5. Close the connection
                if (conn != null)
                {
                    conn.Close();
                }
            

            return identifier;
        }

        //it works
        [HttpGet]
        [Route("api/task/delete/{id}/")]
        public void DeleteNote(int id)
        {
            conn.Open();



            // 3. Pass the connection to a command object
            SqlCommand cmd = new SqlCommand("delete from Notes where Id = @cId", conn);

            SqlParameter nameParam = new SqlParameter("@cId", id);

            cmd.Parameters.Add(nameParam);

            cmd.ExecuteNonQuery();
            conn.Close();


        }

        //it works
        [HttpGet]
        [Route("api/task/edit/{id}/{note}/{isChecked}/{priority}/{date}/{pendDate}/{userId}/{description}")]
        public void EditNote(int id, string note, bool isChecked, string priority, string date, string pendDate, int userId, string description)
        {
            Task t = new Task();
            t.Id = id;
            t.note = note;
            t.isChecked = isChecked;
            t.priority = priority;
            t.date = date;
            t.pendDate = pendDate;
            t.userId = userId;
            t.description = description;

            string query = "update Notes set note = @cnote, isChecked = @cisChecked, priority = @cpriority, date = @cdate, pendDate = @cpendDate, userId =  @cuserId, description = @cdesc where Id = @cId;";
            using (SqlCommand cmd = new SqlCommand(query))
            {

                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cId", t.Id);
                cmd.Parameters.AddWithValue("@cnote", t.note);
                cmd.Parameters.AddWithValue("@cisChecked", t.isChecked);
                cmd.Parameters.AddWithValue("@cpriority", t.priority);
                cmd.Parameters.AddWithValue("@cdate", t.date);
                cmd.Parameters.AddWithValue("@cpendDate", t.pendDate);
                cmd.Parameters.AddWithValue("@cuserId", t.userId);
                cmd.Parameters.AddWithValue("@cdesc", t.description);


                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }


    }
}

