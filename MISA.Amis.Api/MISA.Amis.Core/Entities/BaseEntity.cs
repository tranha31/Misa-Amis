using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    #region Attribute
    /// <summary>
    /// Bắt buộc nhập
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra trùng
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckDuplicate : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra tồn tại
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckExist : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra tuổi
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckAge : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra hợp lệ ngày tháng
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckValidDate : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra tên có đúng cú pháp
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckName : Attribute
    {

    }
    /// <summary>
    /// Kiểm tra giới tính
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckGender : Attribute
    {

    }
    /// <summary>
    /// Kiểm tra số điện thoại
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckPhone : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra số điện thoại cố định
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckTelephone : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra email
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckEmail : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra cccd/cmnd
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckIdentityNumber : Attribute
    {

    }
    /// <summary>
    /// Kiểm tra cccd/cmnd
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckBankNumber : Attribute
    {

    }

    /// <summary>
    /// Kiểm tra mẫ số đối tượng có hợp lệ
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckEntityCode : Attribute
    {

    }

    /// <summary>
    /// Kiêm tra id
    /// create by: TQHa (30/7/2021)
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CheckExistId : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        public int MaxPropertyLength { get; set; }
        public MaxLength(int maxLength)
        {
            this.MaxPropertyLength = maxLength;
        }
    }

    #endregion
    /// <summary>
    /// Các thuộc tính chung của đối tượng
    /// create by: TQHa (29/7/2021)
    /// </summary>
    public class BaseEntity
    {
        #region Properties
        /// <summary>
        /// Ngày tạo bản ghi
        /// create by: TQHa (22/7/2021)
        /// </summary>
        [Column] public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo bản ghi
        /// create by: TQHa (22/7/2021)
        /// </summary>
        [Column] public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa đổi
        /// create by: TQHa (22/7/2021)
        /// </summary>
        [Column] public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa đổi
        /// create by: TQHa (22/7/2021)
        /// </summary>
        [Column] public string ModifiedBy { get; set; }

        #endregion
    }
}
