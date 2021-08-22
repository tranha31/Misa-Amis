using MISA.Amis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces
{
    /// <summary>
    /// Interface tổng quát cho các dịch vụ
    /// create by : TQHa (29/7/2021)
    /// </summary>
    /// <typeparam name="TEntity">Đối tượng</typeparam>
    public interface IBaseService<TEntity>
    {
        #region Methods
        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// create by: TQHa (29/7/2021)
        ServiceResult GetAll();

        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>số lượng được xóa</returns>
        /// create by: TQHa (29/7/2021)
        ServiceResult Delete(List<string> listId);

        /// <summary>
        /// Lấy đối tượng theo Id truyền 
        /// create by: TQHa (29/7/2021)
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns>đối tượng thỏa mãn</returns>
        ServiceResult GetById(string id);

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <param name="status">0: thêm, 1: sửa</param>
        /// <returns>Trạng thái thành công hay thất bại</returns>
        /// create by: TQHa (29/7/2021)
        ServiceResult AddNew(TEntity entity, string status);

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns>Trạng thái</returns>
        /// create by: TQHa (29/7/2021)
        ServiceResult Update(TEntity entity, string status);

        #endregion
    }
}
