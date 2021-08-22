using MISA.Amis.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Core.Interfaces
{
    /// <summary>
    /// Interface chung cho các phương thức gửi từ client
    /// </summary>
    /// <typeparam name="TEntity">Loại đối tượng</typeparam>
    /// create by: TQHa (29/7/2021)
    public interface IBaseDepot<TEntity>
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
        /// Lấy đối tượng theo id
        /// </summary>
        /// <param name="id">id đối tượng</param>
        /// <returns>Đối tượng nếu tồn tại</returns>
        /// create by: TQHa (29/7/2021)
        ServiceResult GetById(string id);

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Trạng thái thành công hay thất bại</returns>
        /// create by: TQHa (29/7/2021)
        ServiceResult AddNew(TEntity entity);

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns>Trạng thái</returns>
        /// create by: TQHa (29/7/2021)
        ServiceResult Update(TEntity entity);

        /// <summary>
        /// Lọc đối tượng
        /// </summary>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="pageNumber">Trang số mấy</param>
        /// <param name="filter">Dữ liệu lọc</param>
        /// <returns>Danh sách đối tượng thỏa mãn</returns>
        /// create by: TQHa (29/7/2021)
        IEnumerable<TEntity> Filter(int pageSize, int pageNumber, string filter);

        /// <summary>
        /// Lấy đối tượng theo cột
        /// create by: TQHa (30/7/2021)
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="tableColumn">Tên cột</param>
        /// <param name="value">Dữ liệu cột</param>
        /// <param name="id">id của đối tượng</param>
        /// <returns>số bản ghi</returns>
        Message GetEnitity(string tableName, string tableColumn, object value);
        #endregion
    }
}
