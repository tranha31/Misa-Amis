using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    /// <summary>
    /// Class cho validate data
    /// create by: TQHa (31/7/2021)
    /// </summary>
    public class CheckClass<TEntity>
    {
        #region Properties
        /// <summary>
        /// Đối tượng cần xét
        /// create by: TQHa (31/7/2021)
        /// </summary>
        public TEntity Entity { get; set; }
        /// <summary>
        /// Thuộc tính cần kiểm tra
        /// create by: TQHa (31/7/2021)
        /// </summary>
        public PropertyInfo Property { get; set; }
        /// <summary>
        /// Tên bảng
        /// create by: TQHa (31/7/2021)
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// Tên thuộc tính
        /// create by: TQHa (31/7/2021)
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// Trạng thái: 0-Post, 1-Put
        /// create by: TQHa (31/7/2021)
        /// </summary>
        public string Status { get; set; }
        /// <summary>
        /// Khóa chính của đối tượng
        /// create by: TQHa (31/7/2021)
        /// </summary>
        public Guid EntityId { get; set; }
        #endregion

        #region Constructor
        public CheckClass(TEntity entity, PropertyInfo property, string tableName, string propertyName, string status, Guid entityId)
        {
            Entity = entity;
            Property = property;
            TableName = tableName;
            PropertyName = propertyName;
            Status = status;
            EntityId = entityId;
        }

        #endregion
    }
}
