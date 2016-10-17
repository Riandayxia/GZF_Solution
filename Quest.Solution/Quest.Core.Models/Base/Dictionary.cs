using Quest.Framework;
using Quest.Framework.T4;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Core.Models.Base
{

    /// <summary>
    /// 数据字典<see cref="Quest.Core.Models.s"/>对象
    ///     <remarks>
    ///         <list type="bullet">
    ///             <item><description>数据字典</description></item>
    ///             <item><description>直接访问数据库相关数据，不作业务处理</description></item> 
    ///         </list>
    ///     </remarks>
    ///     <example>
    ///        <para>演示如何创建类的实例</para>
    ///        <code>
    ///             using System;
    ///             using Netatomy.Learning;
    /// 
    ///             public class Program
    ///             {
    ///                 public static void Main(String[] args)
    ///                 {
    ///                     s index = new s();
    ///                 }
    ///             }
    ///     </code>
    ///     </example>
    /// </summary>   
    [DBTable("数据字典")]
    [DataContract]
    public class Dictionary : BaseEntity
    {
        #region Properties

        /// <summary>
        ///     <remarks>
        ///         <list type="bullet">
        ///             <item><description>获取或设置 父级 (ParentId)</description></item>
        ///             <item><description>直接访问数据库相关数据，不作业务处理</description></item> 
        ///         </list>
        ///     </remarks>
        ///     <example>
        ///        <para>演示如何调用属性</para>
        ///        <code>
        ///             using System;
        ///             using Netatomy.Learning;
        /// 
        ///             public class Program
        ///             {
        ///                 public static void Main(String[] args)
        ///                 {
        ///                     s item = new s();
        ///                     // 设置s的属性父级 ParentId值.
        ///                     item.ParentId=Guid.Empty;
        ///
        ///                     // 获取s的属性父级ParentId值.
        ///                     Console.WriteLine(item.ParentId.ToString());
        ///                 }
        ///             }
        ///     </code>
        ///     </example>
        /// </summary>        
        /// 
        [DataMember]
        [DBColumn("父级Id")]
        public Guid ParentId { get; set; }

        /// <summary>
        ///     <remarks>
        ///         <list type="bullet">
        ///             <item><description>获取或设置 关键字 (Name)</description></item>
        ///             <item><description>直接访问数据库相关数据，不作业务处理</description></item> 
        ///         </list>
        ///     </remarks>
        ///     <example>
        ///        <para>演示如何调用属性</para>
        ///        <code>
        ///             using System;
        ///             using Netatomy.Learning;
        /// 
        ///             public class Program
        ///             {
        ///                 public static void Main(String[] args)
        ///                 {
        ///                     s item = new s();
        ///                     // 设置s的属性名称 Name值.
        ///                     item.Name=String.Empty;
        ///
        ///                     // 获取s的属性名称Name值.
        ///                     Console.WriteLine(item.Name.ToString());
        ///                 }
        ///             }
        ///     </code>
        ///     </example>
        /// </summary>                
        [DataMember]
        [StringLength(64)]
        [DBColumn("关键字")]
        public String Keyword { get; set; }

        /// <summary>
        ///     <remarks>
        ///         <list type="bullet">
        ///             <item><description>获取或设置 建 (Key)</description></item>
        ///             <item><description>直接访问数据库相关数据，不作业务处理</description></item> 
        ///         </list>
        ///     </remarks>
        ///     <example>
        ///        <para>演示如何调用属性</para>
        ///        <code>
        ///             using System;
        ///             using Netatomy.Learning;
        /// 
        ///             public class Program
        ///             {
        ///                 public static void Main(String[] args)
        ///                 {
        ///                     s item = new s();
        ///                     // 设置s的属性建 Key值.
        ///                     item.Key=String.Empty;
        ///
        ///                     // 获取s的属性建Key值.
        ///                     Console.WriteLine(item.Key.ToString());
        ///                 }
        ///             }
        ///     </code>
        ///     </example>
        /// </summary>                
        [DataMember]
        [StringLength(128)]
        public String Key { get; set; }

        /// <summary>
        ///     <remarks>
        ///         <list type="bullet">
        ///             <item><description>获取或设置 值 (Value)</description></item>
        ///             <item><description>直接访问数据库相关数据，不作业务处理</description></item> 
        ///         </list>
        ///     </remarks>
        ///     <example>
        ///        <para>演示如何调用属性</para>
        ///        <code>
        ///             using System;
        ///             using Netatomy.Learning;
        /// 
        ///             public class Program
        ///             {
        ///                 public static void Main(String[] args)
        ///                 {
        ///                     s item = new s();
        ///                     // 设置s的属性值 Value值.
        ///                     item.Value=String.Empty;
        ///
        ///                     // 获取s的属性值Value值.
        ///                     Console.WriteLine(item.Value.ToString());
        ///                 }
        ///             }
        ///     </code>
        ///     </example>
        /// </summary>                
        [StringLength(256)]
        [DataMember]
        public String Value { get; set; }

        /// <summary>
        ///     <remarks>
        ///         <list type="bullet">
        ///             <item><description>获取或设置 顺序 (Sequence)</description></item>
        ///             <item><description>直接访问数据库相关数据，不作业务处理</description></item> 
        ///         </list>
        ///     </remarks>
        ///     <example>
        ///        <para>演示如何调用属性</para>
        ///        <code>
        ///             using System;
        ///             using Netatomy.Learning;
        /// 
        ///             public class Program
        ///             {
        ///                 public static void Main(String[] args)
        ///                 {
        ///                     s item = new s();
        ///                     // 设置s的属性顺序 Sequence值.
        ///                     item.Sequence=String.Empty;
        ///
        ///                     // 获取s的属性顺序Sequence值.
        ///                     Console.WriteLine(item.Sequence.ToString());
        ///                 }
        ///             }
        ///     </code>
        ///     </example>
        /// </summary>                
        [DataMember]
        [DBColumn("顺序")]
        public Int32 Sequence { get; set; }

        /// <summary>
        ///     <remarks>
        ///         <list type="bullet">
        ///             <item><description>获取或设置 描述 (Desc)</description></item>
        ///             <item><description>直接访问数据库相关数据，不作业务处理</description></item> 
        ///         </list>
        ///     </remarks>
        ///     <example>
        ///        <para>演示如何调用属性</para>
        ///        <code>
        ///             using System;
        ///             using Netatomy.Learning;
        /// 
        ///             public class Program
        ///             {
        ///                 public static void Main(String[] args)
        ///                 {
        ///                     s item = new s();
        ///                     // 设置s的属性描述 Desc值.
        ///                     item.Desc=String.Empty;
        ///
        ///                     // 获取s的属性描述Desc值.
        ///                     Console.WriteLine(item.Desc.ToString());
        ///                 }
        ///             }
        ///     </code>
        ///     </example>
        /// </summary>        
        [DataMember]
        [DBColumn("描述")]
        public String Desc { get; set; }

        #endregion
    }
}
