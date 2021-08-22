using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    /// <summary>
    /// Đối tượng phòng ban
    /// create by: TQHa (16/8/2021)
    /// </summary>
    public class Department: BaseEntity
    {
        /// <summary>
        /// Id phòng ban
        /// create by: TQHA (16/8/2021)
        /// </summary>
        [Column] public Guid DepartmentId { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// create by: TQHA (16/8/2021)
        /// </summary>
        [Column] public string DepartmentName { get; set; }
        /// <summary>
        /// Mô tả phòng ban
        /// create by: TQHA (16/8/2021)
        /// </summary>
        [Column] public string Description { get; set; }
    }
}
