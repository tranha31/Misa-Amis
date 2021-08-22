using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    /// <summary>
    /// Tổng số bản ghi trả về từ phương thức lọc
    /// create by: TQHa (30/7/2021)
    /// </summary>
    public class TotalFilter
    {
        #region Properties
        /// <summary>
        /// Tổng số trang
        /// create by : TQHa (30/7/2021)
        /// </summary>
        public int TotalPage { get; set; }
        /// <summary>
        /// Tổng số bản ghi
        /// create by: TQHa (30/7/2021)
        /// </summary>
        public int TotalRecord { get; set; }
        #endregion

        #region Constructor
        public TotalFilter(int totalPage, int totalRecord)
        {
            TotalPage = totalPage;
            TotalRecord = totalRecord;
        }
        #endregion
    }
}
