using BlogApplication.Models.Entites;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApplication.Controllers
{
    public class AddController : Controller
    {

        SqlConnection conn = new SqlConnection(
             "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\MyProjects\\asp.net\\exam\\BlogApplication\\BlogApplication\\App_Data\\BlogDb.mdf;Integrated Security=True");
       
        public ActionResult Index()
        {
            var newPost = new Post();
            return View(newPost);
        }

        public ActionResult AddPost(Post p)
        {

             
                string query = "insert into Post (header, body, usernumber) values (@cheader, @cbody, @cuser)";
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    cmd.Connection = conn;
                    conn.Open();
                    cmd.Parameters.AddWithValue("@cheader", p.header);
                    cmd.Parameters.AddWithValue("@cbody", p.body);
                    cmd.Parameters.AddWithValue("@cuser", p.usernumber);
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }

            
            
            return View("Success");
        }

            
    }
}