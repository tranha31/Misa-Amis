using MISA.Amis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces
{
    public interface IEmployeeService : IBaseService<Employee>
    {
        #region Methods
        /// <summary>
        /// Lọc nhân viên
        /// create by: TQHa (27/7/2021)
        /// </summary>
        /// <param name="employeeFilter">Nội dung của đối tượng cần lọc</param>
        /// <returns>Danh sách nhân viên thỏa mãn</returns>
        ServiceResult Filter(EmployeeFilter employeeFilter);

        /// <summary>
        /// Trả về mã số nhân viên mới nhất
        /// create by: TQHa (27/7/2021)
        /// </summary>
        /// <returns>Mã số nhân viên mới</returns>
        ServiceResult GetNewEmployeeCode();



        #endregion
    }
}
