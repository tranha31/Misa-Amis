using Microsoft.AspNetCore.Mvc;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Enums;
using MISA.Amis.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.Amis.Api.Controllers
{
    /// <summary>
    /// Khai báo đường dẫn
    /// Create by: TQHa (22/7/2021)
    /// </summary>
    [Route("v1/[controller]")]
    [ApiController]
    public class BaseController<TEntity> : Controller
    {
        #region fields
        IBaseService<TEntity> _baseService;
        #endregion

        #region Constructor
        public BaseController(IBaseService<TEntity> baseService)
        {
            _baseService = baseService;
        }
        #endregion

        #region GetAll
        /// <summary>
        /// Lấy toàn bộ danh sách đối tượng
        /// create by: TQHa (29/7/2021)
        /// </summary>
        /// <returns>Danh sách toàn bộ đối tượng, 500 nếu lỗi hệ thống</returns>

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _baseService.GetAll();

            if (result.Message.Code == MisaEnum.Success)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
        #endregion

        #region Id
        /// <summary>
        /// Lấy đối tượng theo khóa chính
        /// create by: TQHa (29/7/2021)
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>Đối tượng có id tương ứng, 404 nếu không tìm thấy, 500 nếu lỗi hệ thống</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            var result = _baseService.GetById(id);

            if (result.Message.Code == MisaEnum.Success)
            {
                return Ok(result);
            }
            else if (result.Message.Code == MisaEnum.NotValid)
            {
                return StatusCode(400, result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }

        #endregion

        #region Delete
        /// <summary>
        /// Xóa đối tượng theo khóa chính
        /// create by: TQHa (30/7/2021)
        /// </summary>
        /// <param name="id">Khóa chính</param>
        /// <returns>trạng thái</returns>


        [HttpDelete]
        public IActionResult Delete([FromBody] ListEntityId listEntityId)
        {
            var listId = listEntityId.ListId;
            var result = _baseService.Delete(listId);

            if (result.Message.Code == MisaEnum.Success)
            {
                return Ok(result);
            }
            else if (result.Message.Code == MisaEnum.NotValid )
            {
                return StatusCode(400, result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
        #endregion

        #region Post
        /// <summary>
        /// Thêm đối tượng mới
        /// create by: TQHa (31/7/2021)
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult Post([FromBody] TEntity entity)
        {
            var result = _baseService.AddNew(entity, Properties.Resources.MisaPost);

            if (result.Message.Code == MisaEnum.Success)
            {
                return StatusCode(201, result);
            }
            else if (result.Message.Code == MisaEnum.NotValid || result.Message.Code == MisaEnum.Duplicate)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
        #endregion

        #region Put
        /// <summary>
        /// Sửa đối tượng
        /// create by: TQHa (31/7/2021)
        /// </summary>
        /// <param name="entity">Đối tượng cần sửa</param>
        /// <returns></returns>

        [HttpPut]
        public IActionResult Put([FromBody] TEntity entity)
        {
            var result = _baseService.Update(entity, Properties.Resources.MisaPut);

            if (result.Message.Code == MisaEnum.Success)
            {
                return Ok(result);
            }
            else if (result.Message.Code == MisaEnum.NotValid || result.Message.Code == MisaEnum.Duplicate)
            {
                return StatusCode(200, result);
            }
            else
            {
                return StatusCode(500, result);
            }
        }
        #endregion

    }
}
