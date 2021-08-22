using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    /// <summary>
    /// Lớp dịch vụ dành riêng cho đối tượng nhân viên
    /// creata by: TQHA (30/7/2021)
    /// </summary>
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeDepot _employeeDepot;

        #region Constructor
        public EmployeeService(IEmployeeDepot employeeDepot) : base(employeeDepot)
        {
            _employeeDepot = employeeDepot;
        }

        /// <summary>
        /// Lọc nhân viên
        /// create by: TQHa (27/7/2021)
        /// </summary>
        /// <param name="employeeFilter">Nội dung của đối tượng cần lọc</param>
        /// <returns>Danh sách nhân viên thỏa mãn</returns>
        public ServiceResult Filter(EmployeeFilter employeeFilter)
        {
            ServiceResult result = new ServiceResult();

            var pageSize = employeeFilter.PageSize;
            var pageNumber = employeeFilter.PageNumber;
            var filter = employeeFilter.Filter;
            
            var employeeValid = new EmployeeValidate(_employeeDepot);

           
            //Kiểm tra bắt buộc truyền vào Kích thước trang và Số thứ tự trang
            if (employeeValid.CheckNull(pageSize))
            {
                var message = new Message();
                message.Code = Enums.MisaEnum.NotValid;
                message.UserMsg = Properties.Resources.API_IsNullData + " : Kích thước trang";
                result.Message = message;
                return result;
            }
            if (employeeValid.CheckNull(pageNumber))
            {
                var message = new Message();
                message.Code = Enums.MisaEnum.NotValid;
                message.UserMsg = Properties.Resources.API_IsNullData + " : Số thứ tự trang";
                result.Message = message;
                return result;
            }

            //Chuyển điều khiển để lấy dữ liệu
            result = _employeeDepot.Filter(employeeFilter);
            
            return result;
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// create by: TQHa (30/7/2021)
        /// </summary>
        /// <returns></returns>
        public ServiceResult GetNewEmployeeCode()
        {
            var result = new ServiceResult();
            result = _employeeDepot.GetNewEmployeeCode();
            return result;
        }
        #endregion



    }
}
