using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Entities
{
    /// <summary>
    /// Đối tượng nhân viên
    /// Create by: TQHa(16/8/2021)
    /// </summary>
    public class Employee: BaseEntity
    {
        #region Properties
        /// <summary>
        /// Id nhân viên
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckExistId]
        [DisplayName("Khóa chính")]
        [Column] public Guid EmployeeId { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckDuplicate]
        [Required]
        [CheckEntityCode]
        [DisplayName("Mã nhân viên")]
        [MaxLength(20)]
        [Column] public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckName]
        [Required]
        [DisplayName("Tên")]
        [MaxLength(100)]
        [Column] public string EmployeeName { get; set; }
        /// <summary>
        /// Ngày sinh nhân viên
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckAge]
        [DisplayName("Ngày sinh")]
        [Column] public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckGender]
        [DisplayName("Giới tính")]
        [Column] public int? Gender { get; set; }
        /// <summary>
        /// Id phòng ban
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [Required]
        [CheckExist]
        [DisplayName("Id phòng ban")]
        [Column] public Guid DepartmentId { get; set; }
        /// <summary>
        /// Số cccd/cmnd
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckDuplicate]
        [CheckIdentityNumber]
        [DisplayName("CCCD/CMND")]
        [MaxLength(20)]
        [Column] public string IdentityNumber { get; set; }
        /// <summary>
        /// Ngày cấp cccd/cmnd
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckValidDate]
        [DisplayName("Ngày cấp CCCD/CMND")]
        [Column] public DateTime? IdentityDate { get; set; }
        /// <summary>
        /// Nơi cấp cccd/cmnd
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [MaxLength(255)]
        [DisplayName("Nơi cấp CCCD/CMND")]
        [Column] public string IdentityPlace { get; set; }
        /// <summary>
        /// Vị trí của nhân viên
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [MaxLength(255)]
        [DisplayName("Chức danh")]
        [Column] public string EmployeePosition { get; set; }
        /// <summary>
        /// Địa chỉ của nhân viên
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [MaxLength(255)]
        [DisplayName("Địa chỉ")]
        [Column] public string Address { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [MaxLength(20)]
        [CheckDuplicate]
        [CheckBankNumber]
        [DisplayName("Tài khoản ngân hàng")]
        [Column] public string BankAccountNumber { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [MaxLength(255)]
        [DisplayName("Ngân hàng")]
        [Column] public string BankName { get; set; }
        /// <summary>
        /// Tên chi nhánh
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [MaxLength(255)]
        [DisplayName("Chi nhánh")]
        [Column] public string BankBranchName { get; set; }
        /// <summary>
        /// Tên xã ngân hàng
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [MaxLength(255)]
        [DisplayName("Chi nhánh huyện")]
        [Column] public string BankProvinceName { get; set; }
        /// <summary>
        /// Số điện thoại cá nhân
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckDuplicate]
        [CheckPhone]
        [DisplayName("Số điện thoại")]
        [MaxLength(20)]
        [Column] public string PhoneNumber { get; set; }
        /// <summary>
        /// Số điện thoại bàn
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        
        [CheckTelephone]
        [DisplayName("Số điện thoại cố định")]
        [MaxLength(20)]
        [Column] public string TelephoneNumber { get; set; }
        /// <summary>
        /// Địa chỉ email
        /// create by: TQHA (16/8/2021)
        /// </summary>
        /// 
        [CheckDuplicate]
        [CheckEmail]
        [DisplayName("Email")]
        [MaxLength(50)]
        [Column] public string Email { get; set; }

        /// <summary>
        /// Tên giới tính
        /// create by: TQHa (5/8/2021)
        /// </summary>
        public string GenderName
        {
            get
            {
                switch (Gender)
                {
                    case 1:
                        return Properties.Resources.Felmale;
                    case 0:
                        return Properties.Resources.Male;
                    default:
                        return Properties.Resources.Other;
                }
            }
            set { }
        }

        #endregion
    }
}
