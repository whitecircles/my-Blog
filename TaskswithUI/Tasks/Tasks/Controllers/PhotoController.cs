using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

using Tasks.Models;

namespace Tasks.Controllers
{
    public class PhotoController : ApiController
    {

        static string connection = ConfigurationManager.ConnectionStrings["Notesdb"].ConnectionString;
        SqlConnection conn = new SqlConnection(connection);

        [HttpPost]
        [Route("api/photo/add/{taskId}")]
        public int InsertPhoto([FromBody] byte[] photo, int taskId)
        {
                
                Photo p = new Photo();
                p.taskId = taskId;
                p.photo = photo;

                string query = "insert into Photos(photo, taskId) values(@cphoto, @ctaskID)";

                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@cphoto", p.photo);
                    cmd.Parameters.AddWithValue("@ctaskId", p.taskId);

                    cmd.ExecuteNonQuery();

                }
            

        
            SqlCommand cmd2 = new SqlCommand("select MAX(Id) from Photos", conn);

            int identifier = (int)cmd2.ExecuteScalar();

            // 5. Close the connection
            if (conn != null)
            {
                conn.Close();
            }

   
            return identifier;

        }

        [HttpGet]
        [Route("api/photo/{id}")]
        public HttpResponseMessage GetPhoto(int id)
        {
            SqlDataReader reader = null;
            Photo resPhoto = new Photo();
            HttpResponseMessage response = new HttpResponseMessage();

            try
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("select * from Photos where taskId = @ctaskId", conn);
                cmd.Parameters.AddWithValue("@ctaskId", id);

                reader = cmd.ExecuteReader();
                

                while (reader.Read())
                {
                    
                    resPhoto.Id = (int)reader["Id"];
                    resPhoto.photo = (byte[])reader["photo"];
                    resPhoto.taskId = (int)reader["taskId"];

                  
                }

                    
                    response = new HttpResponseMessage();
                    response.Content = new ByteArrayContent(resPhoto.photo);
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                
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

            return response;
        }

        // it works
        [HttpGet]
        [Route("api/photo/delete/{id}/")]
        public void DeletePhoto(int id)
        {
            conn.Open();



            // 3. Pass the connection to a command object
            SqlCommand cmd = new SqlCommand("delete from Photos where taskId = @ctaskId", conn);

            SqlParameter nameParam = new SqlParameter("@ctaskId", id);

            cmd.Parameters.Add(nameParam);

            cmd.ExecuteNonQuery();
            conn.Close();


        }

        //it works
        [HttpPost]
        [Route("api/photo/edit/{taskId}")]
        public void EditPhoto([FromBody] byte[] photo, int taskId)
        {
          

                string query = "update Photos set photo = @cPhoto where taskId = @cId;";
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.Connection = conn;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@cId", taskId);
                    cmd.Parameters.AddWithValue("@cPhoto", photo);


                    cmd.ExecuteNonQuery();
                    conn.Close();

                }

            }
        }
    }


