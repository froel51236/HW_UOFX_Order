using Ede.Uofx.Customize.Web.Models;
using Ede.Uofx.Customize.Web.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;


namespace Ede.Uofx.Customize.Web.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        [HttpGet("GetEmployees")]
        public List<Employees> GetEmployees()
        {

            SqlTransaction trans = Connection.GetTransaction();
            try
            {
                Connection.OpenConnection(trans);
                return EmployeesService.GetEmployees(trans);
            }
            finally
            {
                Connection.CloseConnection(trans);
            }
        }


    }
}
