using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using ZTunnel.Pmms.Core.Paged;

namespace ZTunnel.Pmms.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        #region 同步操作

        #region T操作
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        T FindBy(string primaryKey);
        /// <summary>
        /// 返回全部集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> FindAll();
        /// <summary>
        /// 查询符合条件集合
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <returns></returns>
        IEnumerable<T> FindAll(string where);
        /// <summary>
        /// 查询符合条件集合
        /// </summary>
        /// <param name="sql">筛选条件</param>
        /// <param name="param">参数集合</param>
        /// <returns></returns>
        List<T> Query(string sql, object param = null);
        /// <summary>
        /// 查询获取第一个
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        T FirstOrDefault(string sql, object param = null);
        /// <summary>
        /// 异步查询
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<T> FirstOrDefaultAsync(string sql, object param = null);
        /// <summary>
        /// 查询符合条件集合并且排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        IEnumerable<T> FindAll(string where, string order);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        PagedList<T> FindPage(int pageIndex, int pageSize, string where, string order);
        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="pageIndex">索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="tablename">表名</param>
        /// <param name="cols">列名</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <param name="count">总条数</param>
        /// <returns></returns>
        DataTable FindPage(int pageIndex, int pageSize, string tablename, string cols, string where, string order, out int count);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        bool Add(T entity);
        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Add(List<T> entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        bool Delete(T entity);
        /// <summary>
        ///删除符合条件
        /// </summary>
        /// <param name="where">删除</param>
        bool DeleteAll(string where);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        bool Update(T entity);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        bool Exists(string primaryKey);
        #endregion

        #region Sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        int ExecuteSql(string sql, object param = null);
        /// <summary>
        /// 执行sql 语句带事务
        /// </summary>
        /// <param name="dic"></param>
        /// <returns></returns>
        bool ExecuteTransactionSql(Dictionary<string, object> dic);
        /// <summary>
        /// 执行sql 语句带事务
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        bool ExecuteTransactionSql(List<string> sql);
        /// <summary>
        /// 获取收影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        object ExecuteScalar(string sql, object param = null);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        System.Data.DataTable ExecuteDataTable(string sql, object param = null);


        /// <summary>
        /// 分页获取数据
        /// </summary>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="total">总条数</param>
        /// <param name="returnFields">返回字段</param>
        /// <param name="where">表名与查询条件</param>
        /// <param name="param">参数</param>
        /// <param name="orderBy">排序</param>
        /// <returns></returns>
        IEnumerable<TEntity> GetByPage<TEntity>(int pageIndex, int pageSize, out long total, string returnFields = null, string where = null, object param = null, string orderBy = null) where TEntity : new();
        #endregion
        #endregion

        dynamic FirstOrDefaultEx(string sql, object param = null);
        #region 异步操作
        #region T操作
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="primaryKey"></param>
        /// <returns></returns>
        Task<T> FindByAsync(string primaryKey);
        /// <summary>
        /// 返回全部集合
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync();
        /// <summary>
        /// 查询符合条件集合
        /// </summary>
        /// <param name="where">筛选条件</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync(string where);
        /// <summary>
        /// 查询符合条件集合并且排序
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        Task<IEnumerable<T>> FindAllAsync(string where, string order);
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">索引</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="where">查询条件</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        Task<PagedList<T>> FindPageAsync(int pageIndex, int pageSize, string where, string order);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        Task AddAsync(T entity);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        Task DeleteAsync(T entity);
        /// <summary>
        ///删除符合条件
        /// </summary>
        /// <param name="where">删除</param>
        Task DeleteAllAsync(string where);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        Task UpdateAsync(T entity);
        /// <summary>
        /// 是否存在
        /// </summary>
        /// <param name="primaryKey">主键</param>
        /// <returns></returns>
        Task<bool> ExistsAsync(string primaryKey);
        #endregion

        #region Sql语句
        /// <summary>
        /// 执行sql语句
        /// </summary>
        /// <param name="sql">sql</param>
        /// <param name="param">参数</param>
        /// <returns></returns>
        Task<int> ExecuteSqlAsync(string sql, object param = null);
        /// <summary>
        /// 获取收影响的行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<object> ExecuteScalarAsync(string sql, object param = null);
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<System.Data.DataTable> ExecuteDataTableAsync(string sql, object param = null);
        #endregion
        #endregion

    }
}
