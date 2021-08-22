using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    /// <summary>
    /// Mã nhân viên mới
    /// create by: TQHa (31/7/2021)
    /// </summary>
    public class NewEmployeeCode
    {
        #region Properties
        /// <summary>
        /// Kích thước mã nhân viên
        /// create by: TQHa (28/7/2021)
        /// </summary>
        public int EmployeeCodeLength { get; set; }
        /// <summary>
        /// Dữ liệu mã nhân viên chuyển sang int
        /// create by: TQHa (28/7/2021)
        /// </summary>
        public int EmployeeCode { get; set; }
        #endregion
    }
}
