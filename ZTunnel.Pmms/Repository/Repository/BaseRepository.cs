using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZTunnel.Pmms.Core;
using ZTunnel.Pmms.Core.Dapper;
using ZTunnel.Pmms.Core.Paged;
using ZTunnel.Pmms.Repository.IRepository;

namespace ZTunnel.Pmms.Repository.Repository
{
    /// <summary>
    /// 仓储
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial class BaseRepository<T> : IRepository<T> where T : class
    {
        internal IDbConnection db;
        public BaseRepository(IDbConnection db)
        {
            this.db = db;
        }

        public bool Add(T entity)
        {
            return db.Insert(entity) > 0;
        }
        public bool Add(List<T> entity)
        {
            if (db.State != ConnectionState.Open)
            {
                db.Open();
            }
            using (var tran = db.BeginTransaction())
            {
                try
                {
                    db.Insert<T>(entity, tran);
                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    tran.Rollback();
                    return false;
                }
            }
        }
        public async Task AddAsync(T entity)
        {
            await db.InsertAsync<Guid, T>(entity);
        }

        public bool Delete(T entity)
        {
            return db.Delete<T>(entity) > 0;
        }

        public bool DeleteAll(string where)
        {
            return db.DeleteList<T>(where) > 0;
        }

        public async Task DeleteAllAsync(string where)
        {
            await db.DeleteListAsync<T>(where);
        }

        public async Task DeleteAsync(T entity)
        {
            await db.DeleteAsync<T>(entity);
        }

        public DataTable ExecuteDataTable(string sql, object param = null)
        {
            var ds = db.ExecuteReader(sql, param);
            var dt = new DataTable();
            dt.Load(ds);
            return dt;
        }

        public async Task<DataTable> ExecuteDataTableAsync(string sql, object param = null)
        {
            var ds = await db.ExecuteReaderAsync(sql, param);
            var dt = new DataTable();
            dt.Load(ds);
            return dt;
        }

        public object ExecuteScalar(string sql, object param = null)
        {
            return db.ExecuteScalar(sql, param);
        }

        public async Task<object> ExecuteScalarAsync(string sql, object param = null)
        {
            return await db.ExecuteScalarAsync(sql, param);
        }

        public int ExecuteSql(string sql, object param = null)
        {
            return db.Execute(sql, param);
        }
        public bool ExecuteTransactionSql(Dictionary<string, object> dic)
        {
            try
            {
                if (db.State == ConnectionState.Closed) db.Open();
                var tran = db.BeginTransaction();
                foreach (var item in dic)
                {
                    db.Execute(item.Key, item.Value);
                }
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool ExecuteTransactionSql(List<string> sql)
        {
            try
            {
                if (db.State == ConnectionState.Closed) db.Open();
                var tran = db.BeginTransaction();
                sql.ForEach(x =>
                {
                    db.Execute(x);
                });
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> ExecuteSqlAsync(string sql, object param = null)
        {
            return await db.ExecuteAsync(sql, param);
        }

        public bool Exists(string primaryKey)
        {
            return db.QuerySingle<int>("select count(1) from @table where id=@id", new { @table = typeof(T).Name, @id = primaryKey }) > 0;
        }

        public async Task<bool> ExistsAsync(string primaryKey)
        {
            return await db.QuerySingleAsync<int>("select count(1) from @table where id=@id", new { @table = typeof(T).Name, @id = primaryKey }) > 0;
        }

        public IEnumerable<T> FindAll()
        {
            return db.GetList<T>();
        }

        public IEnumerable<T> FindAll(string where)
        {
            return db.GetList<T>(where);
        }
        public List<T> Query(string sql, object param = null)
        {
            return db.Query<T>(sql, param).ToList();
        }

        public T FirstOrDefault(string sql, object param = null)
        {
            return db.QueryFirstOrDefault<T>(sql, param);
        }
        public dynamic FirstOrDefaultEx(string sql, object param = null)
        {
            return db.QueryFirstOrDefault(sql, param);
        }

        public async Task<T> FirstOrDefaultAsync(string sql, object param = null)
        {
            return await db.QueryFirstOrDefaultAsync<T>(sql, param);
        }
        //order by
        public IEnumerable<T> FindAll(string where, string order)
        {
            return db.GetList<T>(where, order);
        }

        public PagedList<T> FindPage(int pageIndex, int pageSize, string where, string order)
        {
            var page = new PagedList<T>();
            page.Total = db.RecordCount<T>(where);
            page.Data = db.GetListPaged<T>(pageIndex, pageSize, where, order);
            return page;
        }
        public DataTable FindPage(int pageIndex, int pageSize, string tablename, string cols, string where, string order, out int count)
        {
            var sql = string.Format("select count(1) from {0} {1}", tablename, where);
            count = (int)db.ExecuteScalar(sql);
            return db.GetTablePaged(pageIndex, pageSize, tablename, cols, where, order);
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await db.GetListAsync<T>();
        }

        public async Task<IEnumerable<T>> FindAllAsync(string where)
        {
            return await db.GetListAsync<T>(where);
        }

        public async Task<IEnumerable<T>> FindAllAsync(string where, string order)
        {
            return await db.GetListAsync<T>(where, order);
        }

        public async Task<PagedList<T>> FindPageAsync(int pageIndex, int pageSize, string where, string order)
        {
            var page = new PagedList<T>();
            page.Total = await db.RecordCountAsync<T>(where);
            page.Data = await db.GetListPagedAsync<T>(pageIndex, pageIndex, where, order);
            return page;
        }


        public T FindBy(string primaryKey)
        {
            return db.Get<T>(primaryKey);
        }

        public async Task<T> FindByAsync(string primaryKey)
        {
            return await db.GetAsync<T>(primaryKey);
        }

        public bool Update(T entity)
        {
            return db.Update<T>(entity) > 0;
        }

        public async Task UpdateAsync(T entity)
        {
            await db.UpdateAsync<T>(entity);
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        public IEnumerable<TEntity> GetByPage<TEntity>(int pageIndex, int pageSize, out long total, string returnFields = null, string where = null, object param = null, string orderBy = null) where TEntity : new()
        {
            int skip = 0;
            if (pageIndex > 0)
            {
                skip = (pageIndex - 1) * pageSize;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(1) FROM {0};", where);
            sb.AppendFormat("SELECT {0} FROM {1} {2}  LIMIT {3},{4}", returnFields, where, orderBy, skip, pageSize);
            using (var reader = db.QueryMultiple(sb.ToString(), param))
            {
                total = reader.ReadFirst<long>();
                return reader.Read<TEntity>();
            }
        }
    }
}
