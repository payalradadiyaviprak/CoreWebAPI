using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Web.Http;
using System.Net.Http;
using System.Net.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace CoreWebAPI.Controllers
{
    //[Produces("application/json")]
    public class CompanyController : Controller
    {
        private string v1;
        //string v2 = _iConfig.GetValue<string>("DBConnection:ConnectionString");
       // private string _connectionString = "data source=VIPRAK\\SQLEXPRESS2014;initial catalog=PayalDemo;user id=sa;password=as;";
        public CompanyController(IConfiguration iConfig)
        {
            v1 = iConfig.GetSection("DBConnection").GetSection("ConnectionString").Value;
        }

        [System.Web.Http.HttpGet]
       // [ODataRoute("CompanyGet")]
        public async Task<IEnumerable<Company>> Get()
        
        {
            IEnumerable<Company> company;

            using (var connection = new SqlConnection(v1))
            {
                await connection.OpenAsync();

                company = await connection.QueryAsync<Company>("COMPANYDETAILS_GET",
                                commandType: CommandType.StoredProcedure);
                await connection.CloseAsync();
            }
            return company;
        }

      
        [System.Web.Http.HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCompany(Company company)
        {
            using (var connection = new SqlConnection(v1))
            {
                await connection.OpenAsync();
                DynamicParameters param = new DynamicParameters();
                param.Add("@project", company.ProjectName);
                param.Add("@groupname", company.GroupMeetingLeadName);
                param.Add("@Id", company.CompanyId);
                param.Add("@oper", company.Oper);
                param.Add("@SUCCESS", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                var affectedRows = connection.Execute("COMPANYDETAILS_CRUD", param, commandType: CommandType.StoredProcedure);
                var success = param.Get<string>("@SUCCESS");
                await connection.CloseAsync();

                return CreatedAtAction("success", success);
            
        }
        }
    }
}