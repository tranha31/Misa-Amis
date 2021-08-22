using MISA.Amis.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    /// <summary>
    /// Thông báo kết quả api
    /// create by: TQHa (27/7/2021)
    /// </summary>
    public class Message
    {
        #region Properties
        /// <summary>
        /// Thông báo lỗi cho dev
        /// create by: TQHa (27/7/2021)
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// Thông báo lỗi cho người dùng
        /// create by: TQHa (27/7/2021)
        /// </summary>
        public object UserMsg { get; set; }

        /// <summary>
        /// Mã lỗi
        /// create by: TQHa (27/7/2021)
        /// </summary>
        public MisaEnum Code { get; set; }

        /// <summary>
        /// Thông tin thêm cho người dùng hoặc dev hiểu
        /// create by: TQHa (27/7/2021)
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// Id lỗi
        /// create by : TQHa (27/7/2021)
        /// </summary>
        public Guid TraceId { get; set; }
        #endregion
    }
}
