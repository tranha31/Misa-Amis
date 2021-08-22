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
     /// Kiểm tra dành riêng cho Nhân viên
     /// create by: TQHa (30/7/2021)
     /// </summary>
    public class EmployeeValidate : BaseValidate<Employee>
    {
        IEmployeeDepot _employeeDepot;

        #region Constructor
        public EmployeeValidate(IEmployeeDepot employeeDepot) : base(employeeDepot)
        {
            _employeeDepot = employeeDepot;
        }

        #endregion

        #region CheckNull
        /// <summary>
        /// Kiểm tra rỗng
        /// create by: TQHa (31/7/2021)
        /// </summary>
        /// <param name="value">giá trị</param>
        /// <returns></returns>
        public bool CheckNull(int? value)
        {
            return (value == null);
        }
        #endregion

        
    }
}
