using Newtonsoft.Json;
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
    
    public class UserController : ApiController
    {
        static string connection = ConfigurationManager.ConnectionStrings["Notesdb"].ConnectionString;  
        SqlConnection conn = new SqlConnection(connection);
       

        // it works
        [HttpGet]
        [Route("api/user")]
        public JsonResult<List<User>> GetAllUsers()
        {
            SqlDataReader reader = null;
            List<User> userModel = new List<User>();

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Users", conn);


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var user = new User();
                    user.Id = (int)reader["Id"];
                    user.Name = (string)reader["name"];
                    user.Passwrd = (string)reader["passwrd"];
                    


                    userModel.Add(user);
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

            


            return Json(userModel); 
        }

        //it works
        [HttpGet]
        [Route("api/user/add/{name}/{password}")]
        public void InsertUser(string name, string password)
        {
            User user = new User();
            user.Name = name;
            user.Passwrd = password;

            string query = "insert into Users (name, passwrd) values (@cname, @cpassword)";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cname", user.Name);
                cmd.Parameters.AddWithValue("@cpassword", user.Passwrd);
                

                cmd.ExecuteNonQuery();


                conn.Close();

            }
        }

        // it works
        [HttpGet]
        [Route("api/user/delete/{id}/")]
        public void DeleteUser(int id)
        {
            conn.Open();



            // 3. Pass the connection to a command object
            SqlCommand cmd = new SqlCommand("delete from Users where Id = @cId", conn);

            SqlParameter nameParam = new SqlParameter("@cId", id);

            cmd.Parameters.Add(nameParam);

            cmd.ExecuteNonQuery();
            conn.Close();


        }

        //it works
        [HttpGet]
        [Route("api/user/edit/{id}/{name}/{password}")]
        public void EditUser(int id, string name, string password)
        {
            User user = new User();
            user.Id = id;
            user.Name = name;
            user.Passwrd = password;

            string query = "update Users set name = @cname, passwrd = @cpassword where Id = @cId;";
            using (SqlCommand cmd = new SqlCommand(query))
            {

                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cId", user.Id);
                cmd.Parameters.AddWithValue("@cname", user.Name);
                cmd.Parameters.AddWithValue("@cpassword", user.Passwrd);

                cmd.ExecuteNonQuery();
                conn.Close();

            }
            
        }


    }
}
