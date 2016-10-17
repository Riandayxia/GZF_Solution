
using Quest.Framework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Quest.Core.Data
{
    /// <summary>
    ///     定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public interface IRepository<TEntity, TKey> where TEntity : BaseEntity
    {
        #region 属性

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        IQueryable<TEntity> Entities { get; }

        /// <summary>
        ///     获取 EntityFramework的数据仓储上下文
        /// </summary>
        Database DB { get; }

        #endregion

        #region 公共方法

        void Dispose();
        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Insert(TEntity entity, Boolean isSave = true);

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Insert(IEnumerable<TEntity> entities, Boolean isSave = true);

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Delete(TKey id, Boolean isSave = true);

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Delete(TEntity entity, Boolean isSave = true);

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Delete(IEnumerable<TEntity> entities, Boolean isSave = true);

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Delete(Expression<Func<TEntity, Boolean>> predicate, Boolean isSave = true);

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        OperationResult Update(TEntity entity, Boolean isSave = true);

        /// <summary>
        /// 使用附带新值的实体信息更新指定实体属性的值
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="isSave">是否执行保存</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        /// <returns>操作影响的行数</returns>
        OperationResult Update(Expression<Func<TEntity, object>> propertyExpression, TEntity entity, Boolean isSave = true);

        /// <summary>
        /// 根据表达式，批量添加或修改
        /// </summary>
        /// <param name="propertyExpression">制定需要添加或更新的表达式</param>
        /// <param name="entities">需要添加或更新地数据对象</param>
        /// <returns>返回操作结果</returns>
        OperationResult AddOrUpdate(Expression<Func<TEntity, object>> propertyExpression, IEnumerable<TEntity> entities, Boolean isSave = true);

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        OperationResult GetByKey(TKey key);

        /// <summary>
        /// 执行原始SQL语句获取对象.
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        OperationResult SqlQuery(String query, params object[] parameters);

        #endregion
    }
}
