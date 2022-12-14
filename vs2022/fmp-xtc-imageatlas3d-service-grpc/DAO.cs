
//*************************************************************************************
//   !!! Generated by the fmp-cli 1.59.0.  DO NOT EDIT!
//*************************************************************************************

using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace XTC.FMP.MOD.ImageAtlas3D.App.Service
{
    /// <summary>
    /// 泛型数据访问对象
    /// </summary>
    /// <typeparam name="T">Entity的派生类</typeparam>
    /// <example>
    /// public class YourDAO : DAO<YourEntity>
    /// {
    ///     public YourDAO(IOptions<DatabaseSettings> _settings) 
    ///         : base(_settings, "TableName")
    ///     {
    ///     }
    ///}
    /// </example>
    public class DAO<T> where T : Entity
    {
        protected readonly IMongoCollection<T> collection_;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_settings">自动注入的数据库设置</param>
        /// <param name="_collectionName">Entity对应的数据库集合名</param>
        public DAO(IOptions<DatabaseSettings> _settings, string _collectionName)
        {
            var mongoClient = new MongoClient(_settings.Value.ConnectionString);
            var db = mongoClient.GetDatabase(_settings.Value.DatabaseName);
            collection_ = db.GetCollection<T>(_collectionName);
        }

        /// <summary>
        /// 异步创建实体
        /// </summary>
        /// <param name="_entity">实体的实例</param>
        /// <returns></returns>
        public virtual async Task CreateAsync(T _entity) =>
           await collection_.InsertOneAsync(_entity);

        /// <summary>
        /// 异步计算数量
        /// </summary>
        /// <returns></returns>
        public virtual async Task<long> CountAsync() =>
            await collection_.CountDocumentsAsync(_=>true);

        /// <summary>
        /// 异步列举实体
        /// </summary>
        /// <param name="_offset">偏移量</param>
        /// <param name="_count">查询量</param>
        /// <returns></returns>
        public virtual async Task<List<T>> ListAsync(int _offset, int _count) =>
            await collection_.Find(_ => true).Skip(_offset).Limit(_count).ToListAsync();

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="_uuid">实体的uuid</param>
        /// <returns></returns>
        public virtual async Task<T?> GetAsync(string _uuid) =>
            await collection_.Find(x => x.Uuid.Equals(Guid.Parse(_uuid))).FirstOrDefaultAsync();

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="_uuid">实体的uuid</param>
        /// <param name="_entity">实体的实例</param>
        /// <returns></returns>
        public virtual async Task UpdateAsync(string _uuid, T _entity) =>
            await collection_.ReplaceOneAsync(x => x.Uuid.Equals(Guid.Parse(_uuid)), _entity);

        /// <summary>
        /// 异步移除实体
        /// </summary>
        /// <param name="_uuid">实体的uuid</param>
        /// <returns></returns>
        public virtual async Task RemoveAsync(string _uuid) =>
            await collection_.DeleteOneAsync(x => x.Uuid.Equals(Guid.Parse(_uuid)));
    }
}
