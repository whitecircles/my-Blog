using BlogApplication.Models.Entites;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogApplication.Controllers
{
    public class RemoveController : Controller
    {
        SqlConnection conn = new SqlConnection(
             "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=D:\\MyProjects\\asp.net\\exam\\BlogApplication\\BlogApplication\\App_Data\\BlogDb.mdf;Integrated Security=True");

        // GET: Remove
        public ActionResult Index()
        {
            Post pst = new Post();

            return View(pst);
        }


        public ActionResult RemovePost(Post p)
        {

            conn.Open();



            // 3. Pass the connection to a command object
            SqlCommand cmd = new SqlCommand("delete from Post where Id = @cId", conn);

            SqlParameter nameParam = new SqlParameter("@cId", p.Id);

            cmd.Parameters.Add(nameParam);

            cmd.ExecuteNonQuery();
            conn.Close();

            



            return View("Success");
        }

    
        


    }
}