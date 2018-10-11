
using BlogApplication.Models.Entites;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApplication.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection conn = new SqlConnection(
             "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\MyProjects\\asp.net\\exam\\BlogApplication\\BlogApplication\\App_Data\\BlogDb.mdf;Integrated Security=True");


        

        public ActionResult Index()
        {
             
            SqlDataReader reader = null;
            List<Post> postModel = new List<Post>();

            try
            {
                // 2. Open the connection
                conn.Open();

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Post", conn);

                
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var post = new Post();
                    post.Id = (int)reader["Id"];
                    post.header = (string)reader["header"];
                    post.body = (string)reader["body"];
                    post.usernumber = (int)reader["usernumber"];
                    

                    postModel.Add(post);
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
            return View(postModel);
        }


        public ActionResult getComments(int identi)
        {
            
            SqlDataReader reader = null;
            List<Comment> commentModel = new List<Comment>();
            

            try
            {
          
                // 2. Open the connection
                conn.Open();

                

                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Comment where @identi = post", conn);

                SqlParameter nameParam = new SqlParameter("@identi", identi);

                cmd.Parameters.Add(nameParam);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var comm = new Comment();
                    comm.usernumber = (string)reader["usernumber"];
                    comm.post = (int)reader["post"];
                    comm.body = (string)reader["body"];
                    

                    commentModel.Add(comm);
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

            return PartialView("_comments",commentModel);
        }

        public ActionResult personalPageAuth()
        {
           

            return View();
        }

        public ActionResult PersonalPage(User usr)
        {
            string query = "select * from Users where @cu = name";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cu", usr.name);
                
                                
                
            //
            SqlDataReader reader = null;



            try
            {

                                           
                reader = cmd.ExecuteReader();


                User nm = new User();
                while (reader.Read())
                {
                    nm.name = (string)reader["name"];
                    nm.Id = (int)reader["Id"];
                }


                if (nm.name != null)
                    return View(nm);
                else
                    return View("Error");
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

        public ActionResult Error()
        {
            ViewBag.Message = "There is no user with this name";

            return View();
        }

        public ActionResult PersonalPosts(int username)
        {
            SqlDataReader reader = null;
            List<Post> postModel = new List<Post>();


            try
            {

                // 2. Open the connection
                conn.Open();



                // 3. Pass the connection to a command object
                SqlCommand cmd = new SqlCommand("select * from Post where @usrn = usernumber", conn);

                SqlParameter nameParam = new SqlParameter("@usrn", username);

                cmd.Parameters.Add(nameParam);


                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    var post = new Post();
                    post.Id = (int)reader["Id"];
                    post.header = (string)reader["header"];
                    post.body = (string)reader["body"];
                    post.usernumber = (int)reader["usernumber"];


                    postModel.Add(post);
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

            return PartialView(postModel);
        }

        public ActionResult addComment(int sample)
        {
            

            return PartialView("_addComment");
        }

        public ActionResult SubmitComment(Comment c)
        {
            string query = "insert into Comment (post, body, usernumber) values (@cpost, @cbody, @cuser)";
            using (SqlCommand cmd = new SqlCommand(query))
            {
                cmd.Connection = conn;
                conn.Open();
                cmd.Parameters.AddWithValue("@cpost", c.post);
                cmd.Parameters.AddWithValue("@cbody", c.body);
                cmd.Parameters.AddWithValue("@cuser", c.usernumber);

                cmd.ExecuteNonQuery();

                
                conn.Close();

            }

            return View("Success");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}