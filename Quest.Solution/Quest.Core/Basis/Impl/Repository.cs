using SuHui.Framework;
using SuHui.Framework.ExtJs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SuHui.Core.Basis
{
    public class Repository<TEntity>
    {

        #region 私有方法

        /// <summary>
        /// TEntity 装换为 ExtTreeNode<Diction>
        /// </summary>
        /// <param name="TEntity">TEntity 对象</param>
        /// <returns>ExtTreeNode<Diction></retur
        private ExtTreeNode<TEntity> ConverterTree(TEntity entity)
        {
            if (entity != null)
            {
                return new ExtTreeNode<TEntity>()
                {
                    id = entity.Id.ToString(),
                    text = entity.Name,
                    Tobject = entity
                };
            }
            else
                return null;
        }

        #endregion

        #region 公共方法

        /// <summary>
        /// IList<TEntity> 装换为 List<ExtTreeNode<TEntity>><TEntity>
        /// </summary>
        /// <param name="TEntity">IList<TEntity> 对象</param>
        /// <returns>List<ExtTreeNode<TEntity>></TEntity>
        public OperationResult ConverterTree(IList<TEntity> TEntitys)
        {
            #region 参数验证
            try
            {
                PublicHelper.CheckArgument(TEntitys, "TEntitys");
            }
            catch (ComponentException e)
            {
                return new OperationResult(OperationResultType.ParamError, e.Message, null);
            }
            #endregion

            List<ExtTreeNode<TEntity>> nodes = new List<ExtTreeNode<TEntity>>();
            try
            {
                foreach (var dic in TEntitys)
                {
                    nodes.Add(ConverterTree(dic));
                }
                return new OperationResult(OperationResultType.Success, "操作成功", nodes);
            }
            catch (Exception e)
            {
                return new OperationResult(OperationResultType.Error, e.Message, null);
            }
        }

        #endregion
    }
}
