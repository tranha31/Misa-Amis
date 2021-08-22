using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.Amis.Core.Entities;
using MISA.Amis.Core.Enums;
using MISA.Amis.Core.Interfaces;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Amis.Repository
{
    public class BaseDepot<TEntity> : IBaseDepot<TEntity>

    {
        #region Declare
        /// <summary>
        /// Thông tin kết nối
        /// create by : TQHa (27/7/2021)
        /// </summary>
        IConfiguration _configuration;
        string _connectionString;

        /// <summary>
        /// Biến khởi tạo kết nối
        /// create by: TQHa (27/7/2021)
        /// </summary>
        IDbConnection _dbConnection;
        string _tableName;
        #endregion

        #region Constructor
        public BaseDepot(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("MISACukCukConnectionString");
            _dbConnection = new MySqlConnection(_connectionString);
            _tableName = typeof(TEntity).Name;
        }

        /// <summary>
        /// Thêm mới đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Trạng thái thành công hay thất bại</returns>
        /// create by: TQHa (29/7/2021)
        public ServiceResult AddNew(TEntity entity)
        {
            var tableName = entity.GetType().Name;
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(entity);

            var result = new ServiceResult();
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    _dbConnection.Execute($"Proc_Insert{tableName}", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    var message = new Message();
                    message.Code = MisaEnum.Success;
                    result.Message = message;
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    var message = new Message();
                    message.Code = MisaEnum.ErrorSever;
                    message.DevMsg = e.Message;
                    message.UserMsg = Properties.Resources.API_ErrorSystem;
                    result.Message = message;

                    transaction.Rollback();

                }
                finally
                {
                    transaction.Dispose();
                    _dbConnection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Xóa đối tượng
        /// </summary>
        /// <param name="id">Id của đối tượng</param>
        /// <returns>số lượng được xóa</returns>
        /// create by: TQHa (29/7/2021)
        public ServiceResult Delete(List<string> listId)
        {
            var message = new Message();
            var result = new ServiceResult();
            DynamicParameters parameters = new DynamicParameters();
            var sql = "";

            for (var i = 0; i < listId.Count; i++)
            {
                parameters.Add($"@Id{i}", listId[i]);
                sql += $"Delete From {_tableName} Where {_tableName}Id = @Id{i}; ";
            }



            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var entity = _dbConnection.Execute(sql, parameters, commandType: CommandType.Text, transaction: transaction);
                    if (entity != 0)
                    {
                        message.Code = MisaEnum.Success;
                        result.Data = entity;
                        result.Message = message;
                    }
                    else
                    {
                        message.Code = MisaEnum.Success;
                        message.UserMsg = Properties.Resources.API_NotFound;
                        result.Message = message;
                    }
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    message.Code = MisaEnum.ErrorSever;
                    message.UserMsg = Properties.Resources.API_ErrorSystem;
                    message.DevMsg = e.Message;
                    result.Message = message;

                    transaction.Rollback();

                }
                finally
                {
                    transaction.Dispose();
                    _dbConnection.Close();
                }
            }
            return result;
        }

        /// <summary>
        /// Lọc đối tượng
        /// </summary>
        /// <param name="pageSize">Kích thước trang</param>
        /// <param name="pageNumber">Trang số mấy</param>
        /// <param name="filter">Dữ liệu lọc</param>
        /// <returns>Danh sách đối tượng thỏa mãn</returns>
        /// create by: TQHa (29/7/2021)
        public IEnumerable<TEntity> Filter(int pageSize, int pageNumber, string filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Lấy tất cả
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// create by: TQHa (29/7/2021)
        public ServiceResult GetAll()
        {
            var message = new Message();
            var result = new ServiceResult();
            var sql = $"Select * from {_tableName} Order By CreatedDate DESC";
            
            try
            {
                var entity = _dbConnection.Query<TEntity>(sql, commandType: CommandType.Text);
                message.Code = MisaEnum.Success;
                result.Data = entity;
                result.Message = message;

            }
            catch (Exception ex)
            {
                //Nếu thất bại
                message.Code = MisaEnum.ErrorSever;
                message.DevMsg = ex.Message;
                message.UserMsg = Properties.Resources.API_ErrorSystem;

                result.Message = message;     

            }
            
            
            return result;

        }

        /// <summary>
        /// Lấy đối tượng theo id
        /// </summary>
        /// <param name="id">id đối tượng</param>
        /// <returns>Đối tượng nếu tồn tại</returns>
        /// create by: TQHa (29/7/2021)
        public ServiceResult GetById(string id)
        {
            var message = new Message();
            var result = new ServiceResult();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            var sql = $"Select * from {_tableName} where {_tableName}Id = @Id";
           
            try
            {
                var entity = _dbConnection.QueryFirstOrDefault<TEntity>(sql, parameters, commandType: CommandType.Text);
                if (entity != null)
                {
                    message.Code = MisaEnum.Success;
                    result.Data = entity;
                    result.Message = message;
                }
                else
                {
                    message.Code = MisaEnum.Success;
                    message.UserMsg = Properties.Resources.API_NotFound;
                    result.Message = message;
                }

                   

            }
            catch (Exception ex)
            {
                //Nếu thất bại
                message.Code = MisaEnum.ErrorSever;
                message.DevMsg = ex.Message;
                message.UserMsg = Properties.Resources.API_ErrorSystem;

                result.Message = message;

                    
            }
            
            
            return result;
        }

        /// <summary>
        /// Lấy đối tượng theo cột
        /// create by: TQHa (30/7/2021)
        /// </summary>
        /// <param name="tableName">Tên bảng</param>
        /// <param name="tableColumn">Tên cột</param>
        /// <param name="value">Dữ liệu cột</param>
        /// <param name="id">id của đối tượng</param>
        /// <returns>số bản ghi</returns>
        public Message GetEnitity(string tableName, string tableColumn, object value)
        {
            var result = new Message();
            DynamicParameters parameters = new DynamicParameters();
            if (value.GetType() == typeof(Guid) || value.GetType() == typeof(Guid?))
            {
                parameters.Add("@Value", value.ToString());
            }
            else
            {
                parameters.Add("@Value", value);
            }



            var sql = $"Select t.{tableName}Id from {tableName} t where {tableColumn} = @Value";
           
           
            try
            {
                var entity = _dbConnection.QueryFirstOrDefault<Guid?>(sql, parameters, commandType: CommandType.Text);
                if (entity != null)
                {
                    result.Code = MisaEnum.Success;
                    result.DevMsg = Properties.Resources.Exist;
                    result.TraceId = (Guid)entity;

                        

                }
                else
                {
                    result.Code = MisaEnum.Success;
                    result.DevMsg = Properties.Resources.NotExist;

                       

                }
            }
            catch (Exception ex)
            {
                result.Code = MisaEnum.ErrorSever;
                result.DevMsg = ex.Message;
                result.UserMsg = Properties.Resources.API_ErrorSystem;



            }
                
            
            return result;
        }

        /// <summary>
        /// Cập nhật đối tượng
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns>Trạng thái</returns>
        /// create by: TQHa (29/7/2021)
        public ServiceResult Update(TEntity entity)
        {
            var tableName = entity.GetType().Name;
            DynamicParameters parameters = new DynamicParameters();
            parameters.AddDynamicParams(entity);

            var result = new ServiceResult();
            _dbConnection.Open();
            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    _dbConnection.Execute($"Proc_Update{tableName}", parameters, commandType: CommandType.StoredProcedure, transaction: transaction);
                    var message = new Message();
                    message.Code = MisaEnum.Success;
                    result.Message = message;
                    transaction.Commit();

                }
                catch (Exception e)
                {
                    var message = new Message();
                    message.Code = MisaEnum.ErrorSever;
                    message.DevMsg = e.Message;
                    message.UserMsg = Properties.Resources.API_ErrorSystem;
                    result.Message = message;
                    transaction.Rollback();


                }
                finally
                {
                    transaction.Dispose();
                    _dbConnection.Close();
                }
            }
            return result;
        }
        #endregion


    }
}
