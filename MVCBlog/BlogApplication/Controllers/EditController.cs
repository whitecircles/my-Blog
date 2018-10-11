using BlogApplication.Models.Entites;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApplication.Controllers
{
    public class EditController : Controller
    {

        SqlConnection conn = new SqlConnection(
             "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\MyProjects\\asp.net\\exam\\BlogApplication\\BlogApplication\\App_Data\\BlogDb.mdf;Integrated Security=True");
        // GET: Edit
        public ActionResult Index()
        {
            Post p = new Post();
            return View(p);
        }

        public ActionResult EditPost(Post p)
        {

            SqlDataReader reader = null;
            //List<Post> postModel = new List<Post>();
            var post = new Post();

            string query = "select * from Post where @cId = Id";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cId", p.Id);
                
                //p.Id = Convert.ToInt32(cmd.ExecuteScalar());
                

            
            reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    post.Id = (int)reader["Id"];
                    post.header = (string)reader["header"];
                    post.body = (string)reader["body"];
                    post.usernumber = (int)reader["usernumber"];

                    //postModel.Add(post);
                   
                }
            }
            conn.Close();
            return View(post);
        }

        public ActionResult EditSubmit(Post p)
        {
            string query = "update Post set header = @cheader, body = @cbody where Id = @cId;";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cId", p.Id);
                cmd.Parameters.AddWithValue("@cheader", p.header);
                cmd.Parameters.AddWithValue("@cbody", p.body);

                cmd.ExecuteNonQuery();
                conn.Close();

            }
                return View("Success");
        }
    }


}