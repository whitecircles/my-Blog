using System;
using System.Collections.Generic;
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
        SqlConnection conn = new SqlConnection(
             "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\white\\Documents\\Visual Studio 2017\\Projects\\Tasks\\Tasks\\App_Data\\Notes.mdf;Integrated Security=True");


        [HttpGet]
        [Route("api/task")]
        public JsonResult<List<Task>> GetAllNotes()
        {
            SqlDataReader reader = null;
            List<Task> taskModel = new List<Task>();

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Notes", conn);


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
                    task.userId = (int)reader["user"];
                    



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
        [Route("api/task/add/{note}/{isChecked}/{priority}/{date}/{pendDate}/{userId}")]
        public void InsertNote(string note, bool isChecked, string priority, string date, string pendDate, int userId)
        {
            Task t = new Task();
            t.note = note;
            t.isChecked = isChecked;
            t.priority = priority;
            t.date = date;
            t.pendDate = pendDate;
            t.userId = userId;

            string query = "insert into Notes (note, isChecked, priority, date, pendDate, userId) values (@cnote, @cisChecked, @cpriority, @cdate, @cpendDate, @cuserId)";
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


                cmd.ExecuteNonQuery();


                conn.Close();

            }
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
        [Route("api/task/edit/{id}/{note}/{isChecked}/{priority}/{date}/{pendDate}/{userId}")]
        public void EditNote(int id, string note, bool isChecked, string priority, string date, string pendDate, int userId)
        {
            Task t = new Task();
            t.Id = id;
            t.note = note;
            t.isChecked = isChecked;
            t.priority = priority;
            t.date = date;
            t.pendDate = pendDate;
            t.userId = userId;

            string query = "update Notes set note = @cnote, isChecked = @cisChecked, priority = @cpriority, date = @cdate, pendDate = @cpendDate, userId =  @cuserId where Id = @cId;";
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


                cmd.ExecuteNonQuery();
                conn.Close();

            }

        }


    }
}

