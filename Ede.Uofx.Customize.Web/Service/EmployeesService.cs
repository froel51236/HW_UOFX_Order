using Ede.Uofx.Customize.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO.Pipelines;

namespace Ede.Uofx.Customize.Web.Service
{
    public class EmployeesService
    {

        internal static List<Employees> GetEmployees(SqlTransaction trans)
        {
            string sql = $@"
                            SELECT EmployeeID,FirstName,Title,HomePhone
                            FROM [Employees]
                            ";

            List<SqlParameter> parameters = new List<SqlParameter>();

            //if (!string.IsNullOrEmpty(code))
            //{
            //    sql += "WHERE unit = @code";

            //    parameters.Add(new SqlParameter("@code", code));
            //}

            return Connection.ExecuteGetData<Employees>(trans, sql, parameters);
        }

    }
}
