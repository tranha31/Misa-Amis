using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    /// <summary>
    /// Dữ liệu trả về cho client
    /// create by: TQHa (29/7/2021)
    /// </summary>
    public class ServiceResult
    {
        #region Properties
        /// <summary>
        /// Dữ liệu trả về từ database
        /// create by: TQHa (29/7/2021)
        /// </summary>
        public object Data { get; set; }
        /// <summary>
        /// Thông báo trả về cho client
        /// create by: TQHa (29/7/2021)
        /// </summary>
        public Message Message { get; set; }
        #endregion
    }
}
