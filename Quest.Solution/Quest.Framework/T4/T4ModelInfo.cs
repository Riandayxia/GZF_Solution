using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Quest.Framework.T4
{
    /// <summary>
    /// T4实体模型信息类
    /// </summary>
    public class T4ModelInfo
    {
        /// <summary>
        /// 获取 是否使用模块文件夹
        /// </summary>
        public bool UseModuleDir { get; private set; }

        /// <summary>
        /// 获取 模型所在模块名称
        /// </summary>
        public string ModuleName { get; private set; }

        /// <summary>
        /// 获取 模型名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取 模型描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 主键类型
        /// </summary>
        public Type KeyType { get; set; }

        /// <summary>
        /// 主键名称
        /// </summary>
        public String KeyName { get; set; }

        /// <summary>
        /// 主键类型名称
        /// </summary>
        public string KeyTypeName { get; set; }

        public IEnumerable<PropertyInfo> Properties { get; private set; }

        public T4ModelInfo(Type modelType, bool isTable, bool useModuleDir = false)
        {
            var @namespace = modelType.Namespace;
            if (@namespace == null)
            {
                return;
            }
            UseModuleDir = useModuleDir;
            if (UseModuleDir)
            {
                var index = @namespace.LastIndexOf('.') + 1;
                ModuleName = @namespace.Substring(index, @namespace.Length - index);
            }

            Name = modelType.Name;
            PropertyInfo keyProp = modelType.GetProperty("Id");
            KeyName = keyProp.Name;
            KeyType = keyProp.PropertyType;
            KeyTypeName = KeyType.Name;
            Properties = modelType.GetProperties();
            if (isTable)
            {
                Object[] descAttributes = modelType.GetCustomAttributes(typeof(DBTableAttribute), true);
                var desc = (DBTableAttribute)descAttributes[0];
                Description = descAttributes.Length == 1 ? desc.Title : Name;
            }
            else
            {
                Object[] dbViewDesc = modelType.GetCustomAttributes(typeof(DBViewAttribute), true);
                var descV = (DBViewAttribute)dbViewDesc[0];
                this.Description = dbViewDesc.Length == 1 ? descV.Title : Name;
            }
        }

    }
}
