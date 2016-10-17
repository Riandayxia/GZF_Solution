using Quest.Framework;
using Quest.Framework.EF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Data.Entity;

using System.Data.Entity.Migrations;
using System.Collections;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Quest.Core.Data
{
    /// <summary>
    /// EntityFramework仓储操作基类
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    /// <typeparam name="TKey">实体主键类型</typeparam>
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : BaseEntity
    {
        #region 属性

        /// <summary>
        ///     获取 仓储上下文的实例
        /// </summary>
        [Import]
        public IUnitOfWork UnitOfWork { get; set; }

        /// <summary>
        ///     获取 EntityFramework的数据仓储上下文
        /// </summary>
        public virtual UnitOfWorkContextBase Context
        {
            get
            {
                if (UnitOfWork is UnitOfWorkContextBase)
                {
                    return UnitOfWork as UnitOfWorkContextBase;
                }
                throw new DataAccessException(string.Format("数据仓储上下文对象类型不正确，应为UnitOfWorkContextBase，实际为 {0}", UnitOfWork.GetType().Name));
            }
        }

        /// <summary>
        ///     获取 EntityFramework的数据仓储上下文
        /// </summary>
        public virtual Database DB
        {
            get { return Context.DbContext.Database; }
        }

        /// <summary>
        ///     获取 当前实体的查询数据集
        /// </summary>
        public virtual IQueryable<TEntity> Entities
        {
            get { return Context.Set<TEntity, TKey>(); }
        }

        #endregion

        #region 公共方法

        public void Dispose()
        {
            Context.Dispose();
        }


        /// <summary>
        /// 执行原始SQL语句获取对象.
        /// </summary>
        /// <param name="query">SQL语句</param>
        /// <param name="parameters">参数</param>
        /// <returns>查询结果</returns>
        public virtual OperationResult SqlQuery(String query, params object[] parameters)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(query, "query");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            try
            {
                IEnumerable<TEntity> items = Context.DbContext.Database.SqlQuery<TEntity>(query, parameters);
                or = new OperationResult(OperationResultType.Success, "执行成功", items);
            }
            catch (Exception e)
            {
                return new OperationResult(OperationResultType.Success, e.Message);
            }
            return or;
        }

        /// <summary>
        ///     插入实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Insert(TEntity entity, bool isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(entity, "entity");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            Context.RegisterNew<TEntity, TKey>(entity);
            Int32 count =  isSave ? Context.Commit(isSave) : 0;
            if (count > 0)
            {
                or = new OperationResult(OperationResultType.Success, "添加成功", false);
            }
            else
            {
                or = new OperationResult(OperationResultType.QueryNull, "添加失败", true);
            }
            return or;
        }

        /// <summary>
        ///     批量插入实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Insert(IEnumerable<TEntity> entities, bool isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(entities, "entities");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            Context.RegisterNew<TEntity, TKey>(entities);
            Int32 count =  isSave ? Context.Commit(isSave) : 0;
            if (count > 0)
            {
                or = new OperationResult(OperationResultType.Success, "添加成功", true);
            }
            else
            {
                or = new OperationResult(OperationResultType.QueryNull, "添加失败", false);
            }
            return or;
        }

        /// <summary>
        /// 根据表达式，批量添加或修改
        /// </summary>
        /// <param name="propertyExpression">制定需要添加或更新的表达式</param>
        /// <param name="entities">需要添加或更新地数据对象</param>
        /// <returns>返回操作结果</returns>
        public OperationResult AddOrUpdate(Expression<Func<TEntity, object>> propertyExpression, IEnumerable<TEntity> entities, Boolean isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(propertyExpression, "propertyExpression");
                PublicHelper.CheckArgument(entities, "entities");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or = new OperationResult(OperationResultType.QueryNull, "保存失败", false); ; ;
            try
            {
                DbSet<TEntity> context = Context.Set<TEntity, TKey>();
                context.AddOrUpdate(propertyExpression, entities.ToArray());
                Int32 count =  isSave ? Context.Commit(isSave) : 0;
                if (count > 0)
                {
                    or = new OperationResult(OperationResultType.Success, "保存成功", true);
                }
                else
                {
                    or = new OperationResult(OperationResultType.QueryNull, "保存失败", false);
                }
            }
            catch (Exception e)
            {
                or = new OperationResult(OperationResultType.QueryNull, e.Message, false);
            }
            return or;
        }

        /// <summary>
        ///     删除指定编号的记录
        /// </summary>
        /// <param name="id"> 实体记录编号 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Delete(TKey id, bool isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(id, "id");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            TEntity entity = Context.Set<TEntity, TKey>().Find(id);
            if (entity.IsNullOrEmpty())
            {
                or = new OperationResult(OperationResultType.QueryNull, "删除失败", false);
            }
            else
            {
                or = Delete(entity, isSave);
            }
            return or;
        }

        /// <summary>
        ///     删除实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Delete(TEntity entity, bool isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(entity, "entity");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            Context.RegisterDeleted<TEntity, TKey>(entity);
            Int32 count =  isSave ? Context.Commit(isSave) : 0;
            if (count > 0)
            {
                or = new OperationResult(OperationResultType.QueryNull, "删除成功", true);
            }
            else
            {
                or = new OperationResult(OperationResultType.QueryNull, "删除失败", false);
            }
            return or;
        }

        /// <summary>
        ///     删除实体记录集合
        /// </summary>
        /// <param name="entities"> 实体记录集合 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Delete(IEnumerable<TEntity> entities, bool isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(entities, "entities");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            Context.RegisterDeleted<TEntity, TKey>(entities);
            Int32 count =  isSave ? Context.Commit(isSave) : 0;
            if (count > 0)
            {
                or = new OperationResult(OperationResultType.Success, "删除成功", true);
            }
            else
            {
                or = new OperationResult(OperationResultType.QueryNull, "删除失败", false);
            }
            return or;
        }

        /// <summary>
        ///     删除所有符合特定表达式的数据
        /// </summary>
        /// <param name="predicate"> 查询条件谓语表达式 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Delete(Expression<Func<TEntity, bool>> predicate, bool isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(predicate, "predicate");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            List<TEntity> entities = Context.Set<TEntity, TKey>().Where(predicate).ToList();
            if (entities.Count > 0)
            {
                or = Delete(entities, isSave);
            }
            else
            {
                or = new OperationResult(OperationResultType.QueryNull, "删除失败", false);
            }
            return or;
        }

        /// <summary>
        ///     更新实体记录
        /// </summary>
        /// <param name="entity"> 实体对象 </param>
        /// <param name="isSave"> 是否执行保存 </param>
        /// <returns> 操作影响的行数 </returns>
        public virtual OperationResult Update(TEntity entity, bool isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(entity, "entity");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            entity.LastUpdatedTime = DateTime.Now;
            Context.RegisterModified<TEntity, TKey>(entity);
            Int32 count =  isSave ? Context.Commit(isSave) : 0;
            if (count > 0)
            {
                or = new OperationResult(OperationResultType.Success, "修改成功", true);
            }
            else
            {
                or = new OperationResult(OperationResultType.QueryNull, "修改失败", false);
            }
            return or;
        }

        /// <summary>
        /// 使用附带新值的实体信息更新指定实体属性的值
        /// </summary>
        /// <param name="propertyExpression">属性表达式</param>
        /// <param name="isSave">是否执行保存</param>
        /// <param name="entity">附带新值的实体信息，必须包含主键</param>
        /// <returns>操作影响的行数</returns>
        public OperationResult Update(Expression<Func<TEntity, object>> propertyExpression, TEntity entity, bool isSave = true)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(propertyExpression, "propertyExpression");
                PublicHelper.CheckArgument(entity, "entity");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or = new OperationResult(OperationResultType.QueryNull, "修改失败", false);
            Context.RegisterModified<TEntity, TKey>(propertyExpression, entity);
            if (isSave)
            {
                var dbSet = Context.Set<TEntity, TKey>();
                dbSet.Local.Clear();
                entity.LastUpdatedTime = DateTime.Now;
                Context.DbContext.Entry(entity);
                Int32 count =  isSave ? Context.Commit(isSave) : 0;
                if (count > 0)
                {
                    or = new OperationResult(OperationResultType.Success, "修改成功", true);
                }
                else
                {
                    or = new OperationResult(OperationResultType.QueryNull, "修改失败", false);
                }
            }
            return or;
        }

        /// <summary>
        ///     查找指定主键的实体记录
        /// </summary>
        /// <param name="key"> 指定主键 </param>
        /// <returns> 符合编号的记录，不存在返回null </returns>
        public virtual OperationResult GetByKey(TKey key)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(key, "key");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message);
            }
            #endregion
            OperationResult or;
            try
            {
                TEntity entity = Context.Set<TEntity, TKey>().Find(key);
                or = new OperationResult(OperationResultType.Success, "操作成功", entity);
            }
            catch (Exception e)
            {
                or = new OperationResult(OperationResultType.QueryNull, e.Message, false);
            }
            return or;
        }

        #endregion
    }
}
