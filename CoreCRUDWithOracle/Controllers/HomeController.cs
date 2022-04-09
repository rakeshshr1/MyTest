using CoreCRUDWithOracle.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CoreCRUDWithOracle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            try 
            {
                string TNS = "Data Source=(DESCRIPTION =" +
        "(ADDRESS = (PROTOCOL = TCP)(HOST = localhost)(PORT = 1521))" +
        "(CONNECT_DATA =" +
        "(SERVER = DEDICATED)" +
        "(SERVICE_NAME = orclpdb)));" +
        "User Id= hr;Password=hr";
                using (OracleConnection conn=new OracleConnection(TNS))
                {
                    using (OracleCommand cmd=new OracleCommand())
                    {
                        conn.Open();
                        DataTable tab = new DataTable();
                        OracleDataAdapter da = new OracleDataAdapter("select * from employees", conn);
                        da.Fill(tab);
                        conn.Close();
                        conn.Dispose();
                    }
                }
            }
            catch(Exception ex)
            {

            }
             return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
