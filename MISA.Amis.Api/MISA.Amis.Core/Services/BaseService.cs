using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Enums;
using MISA.Amis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Services
{
    /// <summary>
    /// Lớp tổng quát cho các dịch vụ
    /// create by : TQHa (29/7/2021)
    /// </summary>
    /// <typeparam name="TEntity">Đối tượng</typeparam>
    public class BaseService<TEntity> : IBaseService<TEntity>
    {
        IBaseDepot<TEntity> _baseDepot;

        #region Constructor
        public BaseService(IBaseDepot<TEntity> baseDepot)
        {
            _baseDepot = baseDepot;
        }

        public ServiceResult AddNew(TEntity entity, string status)
        {
            var baseValid = new BaseValidate<TEntity>(_baseDepot);
            var valid = baseValid.Validate(entity, status);
            var result = new ServiceResult();
            if (valid.Code != MisaEnum.Success)
            {

                result.Message = valid;
                return result;
            }
            if(entity.GetType() == typeof(Employee))
            {
                (entity as Employee).EmployeeName = ChangeName((entity as Employee).EmployeeName);
            }
            result = _baseDepot.AddNew(entity);
            return result;
        }

        public ServiceResult Delete(List<string> listId)
        {
            //Kiểm tra id có phải guid
            var baseValid = new BaseValidate<TEntity>(_baseDepot);

            var result = new ServiceResult();

            for (var i = 0; i < listId.Count; i++)
            {
                if (!baseValid.CheckValidId(listId[i]))
                {
                    //Nếu không trả về lỗi
                    var message = new Message();
                    message.Code = MisaEnum.NotValid;
                    message.UserMsg = Properties.Resources.API_InValidData;
                    result.Message = message;
                    return result;
                }
            }

            result = _baseDepot.Delete(listId);
            return result;
        }

        public ServiceResult GetAll()
        {
            var result = _baseDepot.GetAll();
            return result;
        }

        public ServiceResult GetById(string id)
        {
            //Kiểm tra id có phải guid
            var baseValid = new BaseValidate<TEntity>(_baseDepot);
            var valid = baseValid.CheckValidId(id);

            if (valid)
            {
                var result = _baseDepot.GetById(id);
                return result;
            }
            else
            {
                //Nếu không trả về lỗi
                var result = new ServiceResult();
                var message = new Message();
                message.Code = MisaEnum.NotValid;
                message.UserMsg = Properties.Resources.API_InValidData;
                result.Message = message;

                return result;
            }
        }

        public ServiceResult Update(TEntity entity, string status)
        {
            var baseValid = new BaseValidate<TEntity>(_baseDepot);
            var valid = baseValid.Validate(entity, status);
            var result = new ServiceResult();
            if (valid.Code != MisaEnum.Success)
            {
                result.Message = valid;
                return result;
            }
            if (entity.GetType() == typeof(Employee))
            {
                (entity as Employee).EmployeeName = ChangeName((entity as Employee).EmployeeName);
            }
            result = _baseDepot.Update(entity);
            return result;
        }
        #endregion

        #region ChangeName
        /// <summary>
        /// Chỉnh lại định dạng tên 
        /// create by: TQHa(17/8/2021)
        /// </summary>
        /// <param name="name">Tên</param>
        /// <returns></returns>
        public string ChangeName(string name)
        {
            string result = "";
            //Xóa dấu cách thừa ở 2 đầu
            name = name.Trim();
            //Chuyển thành chữ thường
            name = name.ToLower();
            //Chia cách bằng dấu cách
            string[] nameTmp = name.Split(' ');
            for (int i = 0; i < nameTmp.Length; i++)
            {
                //Chia cách bằng dấu chấm
                string[] nameTmp2 = nameTmp[i].Split('.');
                if (nameTmp2.Length > 1)
                {
                    for (int j = 0; j < nameTmp2.Length; j++)
                    {
                        //Viết hoa chữ đầu tiên
                        if (nameTmp2[j].Length > 1)
                        {
                            result += " " + char.ToUpper(nameTmp2[j][0]) + nameTmp2[j].Substring(1);
                        }
                        else
                        {
                            result += " " + char.ToUpper(nameTmp2[j][0]);
                        }

                    }
                }
                else
                {
                    //Viết hoa chữ đầu tiên
                    if (nameTmp2[0].Length > 1)
                    {
                        result += " "+ char.ToUpper(nameTmp2[0][0]) + nameTmp2[0].Substring(1);
                    }
                    else
                    {
                        result += " "+ char.ToUpper(nameTmp2[0][0]);
                    }
                }
            }
            return result.Trim();
        }

        #endregion

    }
}
