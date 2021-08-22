using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    /// <summary>
    /// Thông tin để lọc nhân viên
    /// create by: TQHa (31/7/2021)
    /// </summary>
    public class EmployeeFilter
    {
        #region Properties
        /// <summary>
        /// Kích thước trang
        /// </summary>
        public int? PageSize { get; set; }
        //Số thứ tự trang
        public int? PageNumber { get; set; }
        //Nội dung lọc
        public string Filter { get; set; }
        //Khóa chính của phòng ban
        #endregion

        #region Constructor
        public EmployeeFilter(int? pageSize, int? pageNumber, string filter)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            Filter = filter;

        }
        #endregion
    }
}
