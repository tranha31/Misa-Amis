using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Enums;
using MISA.Amis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.Api.Controllers
{
    /// <summary>
    /// Khai báo đường dẫn cho controller của Nhân viên
    /// Create by: TQHa (22/7/2021)
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController<Employee>
    {
        #region fields
        IEmployeeService _employeeService;
        #endregion

        #region constructor

        /// <summary>
        /// Khởi tạo đối tượng 
        /// crrate by: TQHa (27/7/2021)
        /// </summary>
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;

        }
        #endregion

        #region Filter

        /// <summary>
        /// Lọc nhân viên
        /// create by: TQHa (27/7/2021)
        /// </summary>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="pageNumber">Trang thứ mấy</param>
        /// <param name="filter">Nội dung lọc</param>
        /// <returns>Danh sách nhân viên thỏa mãn</returns>

        [HttpGet("employeeFilter")]
        public IActionResult Filter(int? pageSize, int? pageNumber, string filter)
        {
            var employeeFilter = new EmployeeFilter(pageSize, pageNumber, filter);
            var result = _employeeService.Filter(employeeFilter);
            if (result.Message.Code == MisaEnum.Success)
            {
                return Ok(result);
            }
            else if (result.Message.Code == MisaEnum.NotValid)
            {
                return StatusCode(400, result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
        #endregion

        #region GetNewEmployeeCode
        /// <summary>
        /// Trả về mã số nhân viên mới nhất
        /// create by: TQHa (27/7/2021)
        /// </summary>
        /// <returns>Mã số nhân viên mới</returns>

        [HttpGet("newEmployee")]
        public IActionResult GetNewEmployeeCode()
        {
            var result = _employeeService.GetNewEmployeeCode();
            if (result.Message.Code == MisaEnum.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        #endregion


        
    }
}
