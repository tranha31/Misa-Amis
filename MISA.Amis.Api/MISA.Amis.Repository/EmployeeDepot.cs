using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Enums;
using MISA.Amis.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Repository
{
    /// <summary>
    /// Truy cập database của đối tượng Nhân viên
    /// create by: TQHa (30/7/2021)
    /// </summary>
    public class EmployeeDepot : BaseDepot<Employee>, IEmployeeDepot
    {
        #region Fields
        IConfiguration _configuration;
        string _connectionString;
        IDbConnection _dbConnection;
        string _tableName;
        #endregion

        #region Constructor
        public EmployeeDepot(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = "Employee";
        }

        public ServiceResult Filter(EmployeeFilter employeeFilter)
        {
            var pageSize = employeeFilter.PageSize;
            var pageNumber = employeeFilter.PageNumber;
            var filter = employeeFilter.Filter;

            var result = new ServiceResult();
            var message = new Message();
            //Nếu filter trống
            if (String.IsNullOrEmpty(filter))
            {
                filter = "";

            }

            //Tránh tấn công sql
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@PageSize", pageSize);
            parameters.Add("@PageIndex", pageNumber);
            parameters.Add("@EmployeeFilter", filter);
            parameters.Add("@TotalRecord", direction: ParameterDirection.Output);
            parameters.Add("@TotalPage", direction: ParameterDirection.Output);



            try
            {
                var employee = _dbConnection.Query<Employee>("Proc_GetEmployeesFilterPaging", parameters, commandType: CommandType.StoredProcedure);

                var totalPage = parameters.Get<int>("TotalPage");
                var totalRecord = parameters.Get<int>("TotalRecord");

                result.Data = employee;
                var total = new TotalFilter(totalPage, totalRecord);

                message.Code = MisaEnum.Success;
                message.UserMsg = total;
                result.Message = message;


            }
            catch (Exception e)
            {

                message.Code = MisaEnum.ErrorSever;
                message.DevMsg = e.Message;
                message.UserMsg = Properties.Resources.API_ErrorSystem;
                result.Message = message;

            }
            
            return result;
        }

        public ServiceResult GetNewEmployeeCode()
        {
            var message = new Message();
            var result = new ServiceResult();
            var sql = $"SELECT CHAR_LENGTH(SUBSTRING(e.EmployeeCode,4, CHAR_LENGTH(e.EmployeeCode) - 3)) AS EmployeeCodeLength, "
                            + "CONVERT(SUBSTRING(e.EmployeeCode, 4, CHAR_LENGTH(e.EmployeeCode) - 3), INT) AS EmployeeCode "
                      + "FROM Employee e "
                      + "WHERE CHAR_LENGTH(SUBSTRING(e.EmployeeCode, 4, CHAR_LENGTH(e.EmployeeCode) - 3)) >="
                            + "ALL(SELECT CHAR_LENGTH(SUBSTRING(em.EmployeeCode, 4, CHAR_LENGTH(em.EmployeeCode) - 3)) FROM Employee em) "
                            + "AND CONVERT(SUBSTRING(e.EmployeeCode,4, CHAR_LENGTH(e.EmployeeCode) - 3), INT) >="
                                + "ALL(SELECT CONVERT(SUBSTRING(emp.EmployeeCode, 4, CHAR_LENGTH(emp.EmployeeCode) - 3), INT) FROM Employee emp); ";


            try
            {
                var employee = _dbConnection.QueryFirstOrDefault<NewEmployeeCode>(sql, commandType: CommandType.Text);
                var newCode = (employee.EmployeeCode + 1).ToString();
                var length = employee.EmployeeCodeLength;

                var tmp = newCode;
                //Nếu kích thước mã nhỏ hơn thì thêm các số 0 vào đầu
                if (newCode.Length < length)
                {
                    for (int i = 0; i < (length - newCode.Length); i++)
                    {
                        tmp = "0" + tmp;
                    }
                }
                tmp = "NV-" + tmp;
                result.Data = tmp;
                message.Code = MisaEnum.Success;
                result.Message = message;

                    

            }
            catch (Exception e)
            {
                message.Code = MisaEnum.ErrorSever;
                message.UserMsg = Properties.Resources.API_ErrorSystem;
                message.DevMsg = e.Message;
                result.Message = message;

                   

            }
            
            return result;
        }
        #endregion


    }
}