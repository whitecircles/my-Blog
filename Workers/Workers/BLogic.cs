using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workers
{
    public class BLogic
    {
        public List<BQ> q = new List<BQ>
            {
                new BQ {position = "q1", salary = 1, quality_of_tasks = 1, teamwork = 1},
                new BQ {position = "q2", salary = 2, quality_of_tasks = 2, teamwork = 2},
                new BQ {position = "q3", salary = 3, quality_of_tasks = 3, teamwork = 3}
            };


        public List<PI> pi = new List<PI>
            {
                new PI {id = 1, name = "Austin Simmons", birthday = "5/3/1973", location = "2454 Kelly Dr"},
                new PI {id = 2, name = "Krin White", birthday = "5/1/1981", location = "4297 Bruce St"},
                new PI {id = 3, name = "Kenzi Curtis", birthday = "1/2/1979", location = "5286 Eagle Point Rd"}
            };

        public List<Prjct> p = new List<Prjct>
            {
                new Prjct {id = 1, name = "p1", customer = "p1", date = "p1", percent = 1},
                new Prjct {id = 2, name = "p2", customer = "p2", date = "p2", percent = 2}
            };
    }
}
