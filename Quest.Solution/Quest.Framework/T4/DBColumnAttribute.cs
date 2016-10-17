/*  作者：      RaindayXia
*  创建时间：   2014/7/23 19:01:58
*
*/
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Framework.T4
{
    /// <summary>
    /// 用于表对象
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class DBColumnAttribute : Attribute
    {
        #region Properties

        /// <summary>
        /// 获取或设置 字段描述
        /// </summary>
        public String Desc { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SQuest.Framework.T4.DBColumnAttribute"/> 类.
        /// </summary>
        /// <param name="desc">描述</param>
        public DBColumnAttribute(String desc)
            : base()
        {
            this.Desc = desc;
        }

        /// <summary>
        /// 初始化一个新的实例的 <see cref="SQuest.Framework.T4.DBColumnAttribute"/> 类.
        /// </summary>
        /// <param name="desc">描述</param>
        /// <param name="name">映射列的名称</param>
        public DBColumnAttribute(String desc, String name)
        {
            this.Desc = desc;
        }

        #endregion
    }
}
