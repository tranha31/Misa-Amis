using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    /// <summary>
    /// Lớp chung cho kiểm tra các trường của đối tượng
    /// create by: TQHa (29/7/2021)
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class BaseValidate<TEntity>
    {
        IBaseDepot<TEntity> _baseDepot;

        #region Constructor
        public BaseValidate(IBaseDepot<TEntity> baseDepot)
        {
            _baseDepot = baseDepot;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Kiểm tra xem Id truyền vào có hợp lệ
        /// create by: TQHa (29/7/2021)
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns>true: hợp lệ, false: không hợp lệ</returns>
        public bool CheckValidId(string id)
        {
            Guid guidOutput;
            bool isValid = Guid.TryParse(id, out guidOutput);
            return (isValid);
        }

        /// <summary>
        /// Kiểm tra email có đúng định dạng
        /// create by: TQHa (27/07/2021)
        /// </summary>
        /// <param name="email">Địa chỉ email nhận được</param>
        /// <returns>true: nếu email hợp lệ, false nếu email không hợp lệ</returns>
        public bool ChekEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra số điện thoại có hợp lệ
        /// create by: TQHa (27/07/2021)
        /// </summary>
        /// <param name="phoneNumber">Số điện thoại nhân viên</param>
        /// <returns>true: hợp lệ, false: không hợp lệ</returns>
        public bool CheckPhoneNumber(string phoneNumber)
        {
            return (Regex.Match(phoneNumber, @"^((09|03|07|08|05|84)+[0-9]{8})$").Success ||
                    Regex.Match(phoneNumber, @"^[+]+([0-9]{2})+([0-9]{9})$").Success ||
                    Regex.Match(phoneNumber, @"^((02)+[0-9]{8})$").Success);

        }

        /// <summary>
        /// Kiểm tra số điện thoại có hợp lệ
        /// create by: TQHa (27/07/2021)
        /// </summary>
        /// <param name="telephoneNumber">Số điện thoại cố định nhân viên</param>
        /// <returns>true: hợp lệ, false: không hợp lệ</returns>
        public bool CheckTelephoneNumber(string telephoneNumber)
        {
            return Regex.Match(telephoneNumber, @"^(\([0-9]{3}\)\s[0-9]{3}-[0-9]{4})$").Success;
                    
        }

        /// <summary>
        /// kiểm tra tên có bao gồm toàn chữ, số, dấu cách hoặc dấu chấm
        /// </summary>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public bool CheckName(string fullname)
        {
            var result = true;
            if (fullname.Length > 100)
            {
                result = false;
            }
            else
            {
                //kiểm tra tên chỉ được có chữ cái, chữ số, dấu cách và dấu chấm
                if (!Regex.Match(fullname, @"^[a-zA-Z_ÀÁÂÃÈÉÊÌÍÒÓÔÕÙÚĂĐĨŨƠàáâãèéêìíòóôõùúăđĩũơƯĂẠẢẤẦẨẪẬẮẰẲẴẶẸẺẼỀỀỂưăạảấầẩẫậắằẳẵặẹẻẽềềểếỄỆỈỊỌỎỐỒỔỖỘỚỜỞỠỢỤỦỨỪễệỉịọỏốồổỗộớờởỡợụủứừỬỮỰỲỴÝỶỸửữựỳỵỷỹý\s\\.]+$").Success)
                {
                    result = false;
                }
                else
                {
                    var indexdot = 0;
                    var indexspace = 0;
                    //kiểm tra xem tên có chứa 2 dấu cách hoặc 2 dấu chấm liên tiếp không
                    for (int i = 0; i < fullname.Length; i++)
                    {
                        //kiểm tra dấu chấm
                        if (fullname[i] == '.')
                        {
                            if (i == indexdot + 1)
                            {
                                result = false;
                                break;
                            }
                            else
                            {
                                indexdot = i;
                            }
                        }
                        //kiểm tra dấu cách
                        if (fullname[i] == ' ')
                        {
                            if (i == indexspace + 1)
                            {
                                result = false;
                                break;
                            }
                            else
                            {
                                indexspace = i;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Kiểm tra tuổi có đủ 18 tuổi
        /// create by: TQHa (27/7/2021)
        /// </summary>
        /// <param name="dateOfBirth">Ngày tháng năm sinh</param>
        /// <returns>true: hợp lệ, false: không hợp lệ</returns>
        public bool CheckAge(DateTime dateOfBirth)
        {

            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.Date)
            {
                age--;
            }

            return (age >= 18 && age<=60);

        }

        /// <summary>
        /// Kiểm tra xem số cccd/cmnd có hợp lệ
        /// create by: TQHa (27/7/2021)
        /// </summary>
        /// <param name="identityCode">Mã số cccd/cmnd</param>
        /// <returns>true: 9 hoặc 12 số, false: ngược lại</returns>
        public bool CheckIdentityCode(string identityCode)
        {
            return (Regex.IsMatch(identityCode, @"^\d+$")) ? (identityCode.Length == 9 || identityCode.Length == 12) : false;
        }

        /// <summary>
        /// Kiểm tra xem số cccd/cmnd có hợp lệ
        /// create by: TQHa (27/7/2021)
        /// </summary>
        /// <param name="bankNumber">Mã số cccd/cmnd</param>
        /// <returns>true: 9 hoặc 12 số, false: ngược lại</returns>
        public bool CheckBankNumber(string bankNumber)
        {
            return (Regex.IsMatch(bankNumber, @"^\d+$")) ;
        }

        /// <summary>
        /// Kiểm tra xem ngày cấp cccd/cmnd có đủ 15 tuổi
        /// create by : TQHa (27/7/2021)
        /// </summary>
        /// <param name="dateOfBirth">Ngày sinh</param>
        /// <param name="dateIdentity">Ngày cấp cccd/cmnd</param>
        /// <returns>true: hợp lệ, false: không hợp lệ</returns>
        public bool CheckValidateIdentityDate(DateTime dateOfBirth, DateTime dateIdentity)
        {
            var age = dateIdentity.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > dateIdentity.Date)
            {
                age--;
            }

            return (age >= 15);
        }

        /// <summary>
        /// Kiểm tra mã nhân viên có đúng định dạng NV-X
        /// create by: TQHa (28/7/2021)
        /// </summary>
        /// <param name="employeeCode">Mã số nhân viên</param>
        /// <returns>true: hợp lệ, false: không hợp lệ</returns>
        public bool CheckValidateEmployeeCode(string employeeCode)
        {
            if (employeeCode.Length >= 4)
            {
                var prefix = employeeCode.Substring(0, 3);
                var suffix = employeeCode.Substring(3);
                return (prefix.Equals("NV-") && Regex.Match(suffix, @"^[0-9]+$").Success);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra giới tính
        /// </summary>
        /// <param name="gender">1: Nữ, 0: Nam, 2: Khác</param>
        /// <returns>true: thỏa mãn, false: không thỏa mãn</returns>
        public bool CheckGender(int gender)
        {
            return (gender == (int)Enums.Gender.Felmale || gender == (int)Enums.Gender.Male || gender == (int)Enums.Gender.Other);
        }

        /// <summary>
        /// Kiểm tra dữ liệu
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm/sửa</param>
        /// <param name="status">0: Thêm mới, 1: Sửa</param>
        /// <returns>Thông báo</returns>
        public Message Validate(TEntity entity, string status)
        {
            var result = new Message();
            result.Code = Enums.MisaEnum.Success;

            var tableName = entity.GetType().Name;
            object dateOfBirth = null;
            object dateIdentity = null;
            Guid entityId = new Guid();
            //Đọc các property
            var properties = entity.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(entity);
                var check = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault();
                var displayName = "";
                if (check != null)
                {
                    displayName = check.DisplayName;
                }

                //Kiểm tra kích thước tối đa
                if (Attribute.IsDefined(property, typeof(MaxLength)))
                {
                    var maxLength = property.GetCustomAttributes(typeof(MaxLength), true).Cast<MaxLength>().FirstOrDefault().MaxPropertyLength;
                    if (propertyValue != null)
                    {
                        var tmp = CheckLength(maxLength, displayName, propertyValue.ToString());
                        //Nếu vượt quá độ dài cho phép thì trả thông báo cho người dùng
                        if (tmp.Code == Enums.MisaEnum.NotValid)
                        {
                            return tmp;
                        }
                    }

                }


                //Lấy id của đối tượng cho thao tác update
                if (Attribute.IsDefined(property, typeof(CheckExistId)))
                {
                    if (status.Equals(Properties.Resources.MisaPut))
                    {
                        var tmp = _baseDepot.GetEnitity(tableName, propertyName, propertyValue);
                        if (tmp.DevMsg.Equals(Properties.Resources.Exist))
                        {
                            entityId = (Guid)propertyValue;
                        }
                        else
                        {
                            result.Code = Enums.MisaEnum.NotValid;
                            result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                            return result;
                        }

                    }
                }

                //Kiểm tra bắt buộc nhập
                if (Attribute.IsDefined(property, typeof(Required)))
                {


                    //Nếu trống
                    if (propertyValue == null || propertyValue.Equals(""))
                    {
                        
                        result.Code = Enums.MisaEnum.NotValid;
                        result.UserMsg = Properties.Resources.API_IsNullData + " : " + displayName;
                        return result;
                    }
                    else
                    {

                        if (Attribute.IsDefined(property, typeof(CheckDuplicate)))
                        {
                            var tmp = _baseDepot.GetEnitity(tableName, propertyName, propertyValue);
                            if (tmp.DevMsg.Equals(Properties.Resources.Exist))
                            {
                                //Kiểm tra trùng cho post
                                if (status.Equals(Properties.Resources.MisaPost))
                                {
                                    result.DevMsg = property.ToString();
                                    result.Code = Enums.MisaEnum.Duplicate;
                                    result.UserMsg = Properties.Resources.API_IsExistData + " : " + displayName + " <"+ propertyValue +">";
                                    return result;
                                }
                                //Kiểm tra trùng cho put
                                else if (status.Equals(Properties.Resources.MisaPut))
                                {
                                    if (tmp.TraceId != entityId)
                                    {
                                        result.DevMsg = property.ToString();
                                        result.Code = Enums.MisaEnum.Duplicate;
                                        result.UserMsg = Properties.Resources.API_IsExistData + " : " + displayName +" <" + propertyValue + ">";
                                        return result;
                                    }
                                }

                            }
                            else if (tmp.Code == Enums.MisaEnum.ErrorSever)
                            {
                                result = tmp;
                                return result;
                            }
                        }
                        var checkClass = new CheckClass<TEntity>(entity, property, tableName, propertyName, status, entityId);
                        var resultCheck = CheckFormatData(checkClass);
                        if (resultCheck.Code != Enums.MisaEnum.Success)
                        {
                            result = resultCheck;
                            return result;
                        }

                    }

                }
                //Các trường hợp còn lại
                else
                {
                    if (propertyValue != null && !propertyValue.Equals("") && propertyValue.ToString() != "00000000-0000-0000-0000-000000000000")
                    {
                        //Lưu ngày sinh
                        if (Attribute.IsDefined(property, typeof(CheckAge)))
                        {
                            dateOfBirth = propertyValue;
                        }
                        //Kiểm tra ngày cấp cccd/cmnd
                        if (Attribute.IsDefined(property, typeof(CheckValidDate)))
                        {
                            //Nếu có ngày sinh
                            if (dateOfBirth != null)
                            {
                                dateIdentity = propertyValue;
                                //Kiểm tra kiểu ngày cấp cccd/cmnd
                                if (dateIdentity.GetType() != typeof(DateTime) && dateIdentity.GetType() != typeof(DateTime?))
                                {
                                    result.Code = Enums.MisaEnum.NotValid;
                                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                                    return result;
                                }
                                else
                                {
                                    //Kiểm tra ngày cấp có hợp lệ
                                    var c = CheckValidateIdentityDate((DateTime)dateOfBirth, (DateTime)dateIdentity);
                                    if (!c)
                                    {
                                        result.Code = Enums.MisaEnum.NotValid;
                                        result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                                        return result;
                                    }
                                }
                            }
                        }
                        var checkClass = new CheckClass<TEntity>(entity, property, tableName, propertyName, status, entityId);
                        var resultCheck = CheckFormatData(checkClass);
                        if (resultCheck.Code != Enums.MisaEnum.Success)
                        {
                            result = resultCheck;
                            return result;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Kiểm tra các trường
        /// create by: TQHa (30/7/2021)
        /// </summary>
        /// <param name="checkClass">Đối tượng kiểm tra</param>
        /// <returns>Thông điệp</returns>
        public Message CheckFormatData(CheckClass<TEntity> checkClass)
        {
            var entity = checkClass.Entity;
            var property = checkClass.Property;
            var tableName = checkClass.TableName;
            var propertyName = checkClass.PropertyName;
            var entityId = checkClass.EntityId;
            var status = checkClass.Status;

            var result = new Message();
            result.Code = Enums.MisaEnum.Success;

            var propertyValue = property.GetValue(entity);
            var check = property.GetCustomAttributes(typeof(DisplayNameAttribute), true).Cast<DisplayNameAttribute>().FirstOrDefault();
            var displayName = "";
            if (check != null)
            {
                displayName = check.DisplayName;
            }
            //Kiểm tra trùng
            if (Attribute.IsDefined(property, typeof(CheckDuplicate)))
            {
                var tmp = _baseDepot.GetEnitity(tableName, propertyName, propertyValue);
                if (tmp.DevMsg.Equals(Properties.Resources.Exist))
                {
                    //Kiểm tra trùng cho post
                    if (status.Equals(Properties.Resources.MisaPost))
                    {
                        result.DevMsg = property.ToString();
                        result.Code = Enums.MisaEnum.Duplicate;
                        result.UserMsg = Properties.Resources.API_IsExistData + " : " + displayName + " <" + propertyValue + ">";
                        return result;
                    }
                    //Kiểm tra trùng cho put
                    else if (status.Equals(Properties.Resources.MisaPut))
                    {
                        if (tmp.TraceId != entityId)
                        {
                            result.DevMsg = property.ToString();
                            result.Code = Enums.MisaEnum.Duplicate;
                            result.UserMsg = Properties.Resources.API_IsExistData + " : " + displayName + " <" + propertyValue + ">";
                            return result;
                        }
                    }

                }
                else if (tmp.Code == Enums.MisaEnum.ErrorSever)
                {
                    result = tmp;
                    return result;
                }
            }
            //Kiểm tra mã số
            if (Attribute.IsDefined(property, typeof(CheckEntityCode)))
            {
                if (entity.GetType().Name.Equals("Employee"))
                {

                    var checkValid = CheckValidateEmployeeCode(propertyValue.ToString());
                    if (!checkValid)
                    {
                        result.Code = Enums.MisaEnum.NotValid;
                        result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                        return result;
                    }
                }
            }
            //Kiểm tra tên
            if (Attribute.IsDefined(property, typeof(CheckName)))
            {
                var checkValid = CheckName(propertyValue.ToString());
                if (!checkValid)
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                    return result;
                }
            }
            //Kiểm tra số điện thoại
            if (Attribute.IsDefined(property, typeof(CheckPhone)))
            {
                var checkValid = CheckPhoneNumber(propertyValue.ToString());
                if (!checkValid)
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                    return result;
                }
            }

            //Kiểm tra số điện thoại cố định
            if (Attribute.IsDefined(property, typeof(CheckTelephone)))
            {
                var checkValid = CheckTelephoneNumber(propertyValue.ToString());
                if (!checkValid)
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                    return result;
                }
            }
            //Kiểm tra email
            if (Attribute.IsDefined(property, typeof(CheckEmail)))
            {
                var checkValid = ChekEmail(propertyValue.ToString());
                if (!checkValid)
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                    return result;
                }
            }
            //Kiểm tra tuổi
            if (Attribute.IsDefined(property, typeof(CheckAge)))
            {
                if (propertyValue.GetType() == typeof(DateTime) || propertyValue.GetType() == typeof(DateTime?))
                {
                    var checkValid = CheckAge((DateTime)propertyValue);
                    if (!checkValid)
                    {
                        result.Code = Enums.MisaEnum.NotValid;
                        result.UserMsg = Properties.Resources.API_NotEnoughAge;
                        return result;
                    }
                }
                else
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                    return result;
                }

            }
            
            //Kiểm tra số cccd/cmnd
            if (Attribute.IsDefined(property, typeof(CheckIdentityNumber)))
            {
                var checkValid = CheckIdentityCode(propertyValue.ToString());
                if (!checkValid)
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                    return result;
                }
            }

            //Kiểm tra tài khoản ngân hàng
            if (Attribute.IsDefined(property, typeof(CheckBankNumber)))
            {
                var checkValid = CheckBankNumber(propertyValue.ToString());
                if (!checkValid)
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                    return result;
                }
            }
            //Kiểm tra tồn tại
            if (Attribute.IsDefined(property, typeof(CheckExist)))
            {
                tableName = propertyName.Substring(0, propertyName.Length - 2);
                var tmp = _baseDepot.GetEnitity(tableName, propertyName, propertyValue);
                if (tmp.DevMsg == Properties.Resources.NotExist)
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_NotFound + " : " + displayName;
                    return result;
                }
                else if (tmp.Code == Enums.MisaEnum.ErrorSever)
                {
                    result = tmp;
                    return result;
                }
            }
            //Kiểm tra giới tính
            if (Attribute.IsDefined(property, typeof(CheckGender)))
            {
                var checkValid = CheckGender((int)propertyValue);
                if (!checkValid)
                {
                    result.Code = Enums.MisaEnum.NotValid;
                    result.UserMsg = Properties.Resources.API_InValidData + " : " + displayName;
                    return result;
                }
            }
            return result;
        }
        #endregion

        #region MaxLength
        /// <summary>
        /// Kiểm tra độ dài tối đa của các trường dữ liệu
        /// create by: TQHa (5/8/2021)
        /// </summary>
        /// <param name="maxLength">Kích thước tối đa của dữ liệu</param>
        /// <param name="displayName">Tên của thuộc tính</param>
        /// <param name="propertyValue">Giá trị của thuộc tính</param>
        /// <returns>Thông báo</returns>
        public Message CheckLength(int maxLength, string displayName, string propertyValue)
        {
            var message = new Message();
            message.Code = Enums.MisaEnum.Success;

            if (!string.IsNullOrEmpty(propertyValue) && propertyValue.Length > maxLength)
            {
                message.Code = Enums.MisaEnum.NotValid;
                message.UserMsg = displayName + " " + Properties.Resources.MaxLength + " : " + maxLength;
                return message;
            }
            else
            {
                return message;

            }
        }

        #endregion

        
    }
}
