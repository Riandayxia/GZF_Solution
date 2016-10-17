using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace SuHui.Framework.Server
{
    /// <summary>
    /// 服务主机信息
    /// </summary>
    public class SuHuiWCFHost
    {
        #region Properties

        /// <summary>
        /// 服务主机Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 服务主机名称
        /// </summary>
        public String Name { get; set; }
        /// <summary>
        /// 服务主机地址
        /// </summary>
        public String Path { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public Type ServiceType { get; set; }
        /// <summary>
        /// 服务接口类型
        /// </summary>
        public Type IServiceType { get; set; }
        /// <summary>
        /// 提供可靠服务主机
        /// </summary>
        public ServiceHost Host { get; set; }
        /// <summary>
        /// 服务主机描述
        /// </summary>
        public String Description { get; set; }

        #endregion
    }
}
