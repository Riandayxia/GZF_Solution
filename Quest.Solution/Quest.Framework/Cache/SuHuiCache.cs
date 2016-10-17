using System;
using System.Linq;
using System.Xml;
#if NET4
using System.IO.MemoryMappedFiles;
#endif
using XmlElement = System.Xml.XmlElement;
using System.Collections;

namespace SuHui.Framework
{
    /// <summary>
    /// Discuz!NT缓存类
    /// 对Discuz!NT论坛缓存进行全局控制管理
    /// </summary>
    public class SuHuiCache
    {
        private static XmlElement _objectXmlMap;
        private static ICacheStrategy _cs;
        private static volatile SuHuiCache _instance;
        private static readonly object LockHelper = new object();
        private static readonly XmlDocument RootXml = new XmlDocument();

        /// <summary>
        /// 构造函数
        /// </summary>
        private SuHuiCache()
        {
            _cs = new DefaultCacheStrategy();
            if (RootXml.HasChildNodes)
                RootXml.RemoveAll();

            _objectXmlMap = RootXml.CreateElement("Cache");
            //建立内部XML文档.
            RootXml.AppendChild(_objectXmlMap);
        }

        /// <summary>
        /// 单体模式返回当前类的实例
        /// </summary>
        /// <returns></returns>
        public static SuHuiCache GetCacheService()
        {
            if (_instance == null)
            {
                lock (LockHelper)
                {
                    if (_instance == null)
                    {
                        _instance = new SuHuiCache();
                    }
                }
            }

            return _instance;
        }


        /// <summary>
        /// 在XML映射文档中的指定路径,加入当前对象信息
        /// </summary>
        /// <param name="xpath">分级对象的路径 </param>
        /// <param name="o">被缓存的对象</param>
        public virtual void AddObject(string xpath, object o)
        {
            lock (LockHelper)
            {
                {
                    //当缓存到期时间为0或负值,则不再放入缓存
                    if (_cs.TimeOut <= 0) return;

                    //整理XPATH表达式信息
                    var newXpath = PrepareXpath(xpath);
                    var separator = newXpath.LastIndexOf("/", StringComparison.Ordinal);
                    //找到相关的组名
                    var group = newXpath.Substring(0, separator);
                    //找到相关的对象
                    var element = newXpath.Substring(separator + 1);

                    var groupNode = _objectXmlMap.SelectSingleNode(@group);

                    //建立对象的唯一键值, 用以映射XML和缓存对象的键
                    var objectId = "";

                    var node = _objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                    if (node != null)
                    {
                        if (node.Attributes != null) objectId = node.Attributes["objectId"].Value;
                    }

                    if (objectId == "")
                    {
                        groupNode = CreateNode(@group);
                        objectId = Guid.NewGuid().ToString();
                        //建立新元素和一个属性 for this perticular object
                        if (_objectXmlMap.OwnerDocument != null)
                        {
                            var objectElement = _objectXmlMap.OwnerDocument.CreateElement(element);
                            var objectAttribute = _objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                            objectAttribute.Value = objectId;
                            objectElement.Attributes.Append(objectAttribute);
                            //为XML文档建立新元素
                            groupNode.AppendChild(objectElement);
                        }
                    }
                    else
                    {
                        //建立新元素和一个属性 for this perticular object
                        if (_objectXmlMap.OwnerDocument != null)
                        {
                            var objectElement = _objectXmlMap.OwnerDocument.CreateElement(element);
                            var objectAttribute = _objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                            objectAttribute.Value = objectId;
                            objectElement.Attributes.Append(objectAttribute);
                            //为XML文档建立新元素
                            if (groupNode != null) if (node != null) groupNode.ReplaceChild(objectElement, node);
                        }
                    }

                    //向缓存加入新的对象
                    _cs.AddObject(objectId, o);
                }
            }
        }

        /// <summary>
        /// 在XML映射文档中的指定路径,加入当前对象信息
        /// </summary>
        /// <param name="xpath">分级对象的路径 </param>
        /// <param name="o">
        ///     被缓存的对象
        ///     到期时间,单位:秒
        /// </param>
        /// <param name="expire"></param>
        public virtual void AddObject(string xpath, object o, int expire)
        {
            lock (LockHelper)
            {
                {
                    //当缓存到期时间为0或负值,则不再放入缓存
                    if (_cs.TimeOut <= 0) return;

                    //整理XPATH表达式信息
                    var newXpath = PrepareXpath(xpath);
                    var separator = newXpath.LastIndexOf("/", StringComparison.Ordinal);
                    //找到相关的组名
                    var group = newXpath.Substring(0, separator);
                    //找到相关的对象
                    var element = newXpath.Substring(separator + 1);

                    var groupNode = _objectXmlMap.SelectSingleNode(@group);

                    //建立对象的唯一键值, 用以映射XML和缓存对象的键
                    var objectId = "";

                    var node = _objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                    if (node != null)
                    {
                        if (node.Attributes != null) objectId = node.Attributes["objectId"].Value;
                    }

                    if (objectId == "")
                    {
                        groupNode = CreateNode(@group);
                        objectId = Guid.NewGuid().ToString();
                        //建立新元素和一个属性 for this perticular object
                        if (_objectXmlMap.OwnerDocument != null)
                        {
                            XmlElement objectElement = _objectXmlMap.OwnerDocument.CreateElement(element);
                            XmlAttribute objectAttribute = _objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                            objectAttribute.Value = objectId;
                            objectElement.Attributes.Append(objectAttribute);
                            //为XML文档建立新元素
                            groupNode.AppendChild(objectElement);
                        }
                    }
                    else
                    {
                        //建立新元素和一个属性 for this perticular object
                        if (_objectXmlMap.OwnerDocument != null)
                        {
                            XmlElement objectElement = _objectXmlMap.OwnerDocument.CreateElement(element);
                            XmlAttribute objectAttribute = _objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                            objectAttribute.Value = objectId;
                            objectElement.Attributes.Append(objectAttribute);
                            //为XML文档建立新元素
                            if (node != null) if (groupNode != null) groupNode.ReplaceChild(objectElement, node);
                        }
                    }

                    //向缓存加入新的对象
                    _cs.AddObject(objectId, o, expire);
                }
            }
        }

        /// <summary>
        /// 在XML映射文档中的指定路径,加入当前对象信息
        /// </summary>
        /// <param name="xpath">分级对象的路径 </param>
        /// <param name="o">被缓存的对象</param>
        /// <param name="files"></param>
        public virtual void AddObject(string xpath, object o, string[] files)
        {
            xpath = xpath.Replace(" ", "_SPACE_");    //如果xpath中出现空格，则将空格替换为_SPACE_
            lock (LockHelper)
            {
                {
                    //当缓存到期时间为0或负值,则不再放入缓存
                    if (_cs.TimeOut <= 0) return;

                    //整理XPATH表达式信息
                    var newXpath = PrepareXpath(xpath);
                    var separator = newXpath.LastIndexOf("/", StringComparison.Ordinal);
                    //找到相关的组名
                    var group = newXpath.Substring(0, separator);
                    //找到相关的对象
                    var element = newXpath.Substring(separator + 1);

                    var groupNode = _objectXmlMap.SelectSingleNode(@group);
                    //建立对象的唯一键值, 用以映射XML和缓存对象的键
                    var objectId = "";

                    var node = _objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                    if (node != null)
                    {
                        if (node.Attributes != null) objectId = node.Attributes["objectId"].Value;
                    }
                    if (objectId == "")
                    {
                        groupNode = CreateNode(@group);
                        objectId = Guid.NewGuid().ToString();
                        //建立新元素和一个属性 for this perticular object
                        if (_objectXmlMap.OwnerDocument != null)
                        {
                            var objectElement = _objectXmlMap.OwnerDocument.CreateElement(element);
                            var objectAttribute = _objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                            objectAttribute.Value = objectId;
                            objectElement.Attributes.Append(objectAttribute);
                            //为XML文档建立新元素
                            groupNode.AppendChild(objectElement);
                        }
                    }
                    else
                    {
                        //建立新元素和一个属性 for this perticular object
                        if (_objectXmlMap.OwnerDocument != null)
                        {
                            var objectElement = _objectXmlMap.OwnerDocument.CreateElement(element);
                            var objectAttribute = _objectXmlMap.OwnerDocument.CreateAttribute("objectId");
                            objectAttribute.Value = objectId;
                            objectElement.Attributes.Append(objectAttribute);
                            //为XML文档建立新元素
                            if (groupNode != null) if (node != null) groupNode.ReplaceChild(objectElement, node);
                        }
                    }

                    //向缓存加入新的对象
                    _cs.AddObjectWithFileChange(objectId, o, files);
                }
            }
        }

#if NET4 
        private static Hashtable htMapFile = new Hashtable();
#endif

        /// <summary>
        /// 取得指定XML路径下的数据项
        /// </summary>
        /// <param name="xpath">分级对象的路径</param>
        /// <returns></returns>
        public virtual object RetrieveObject(string xpath)
        {
            try
            {
#if NET4       
                if (GeneralConfigs.GetConfig().Webgarden > 1 && Environment.Version.Major >= 4)
                {
                    //.net4框架下基于mmap实现跨进程共享信息，来实现当前web园进程内缓存更新后，其它web园进程无法得到信息已修改的标记
                    //方法摘要：通过htMapFile表记录共享内存的文件信息，这样可以提升访问共享信息的命中率（之前直接声明的方式命中率非常低且容易过多申请共享内存造成内存紧张）
                    //通过在共享内存中保存进程ID的方式，如果当前进程ID未出现在共享内存中，则直接将进程ID放到内享内存中，同时返回NULL，这样前端就会从数据库或文件中再次载入数据。
                    //如当前进程ID出现在了共享内存中，则标识该进程中的当前键值的缓存数据已更新过，则直接从缓存中获取数据并返回该数据信息。
                    lock (lockHelper)
                    {
                        //强制移除缓存（将共享内存中数据清空）后，查看指定缓存键的共享内存数据变化
                        //if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["removecache"]))
                        //    RemoveObject("/Forum/TemplateIDList");

                        MemoryMappedFile file = htMapFile[xpath] as MemoryMappedFile;
                        if (file == null)
                        {
                            file = MemoryMappedFile.CreateOrOpen(xpath, 512, MemoryMappedFileAccess.ReadWrite);// MemoryMappedFileOptions.DelayAllocatePages, new MemoryMappedFileSecurity(), HandleInheritability.Inheritable);
                            htMapFile.Add(xpath, file);
                        }
                        int processId = System.Diagnostics.Process.GetCurrentProcess().Id;
                        using (BinaryReader br = new BinaryReader(file.CreateViewStream()))
                        {
                            string brstr = br.ReadString().Trim().Replace("none", "");
                            if (!brstr.Contains("_" + processId + "_"))
                            {
                                using (BinaryWriter bw = new BinaryWriter(file.CreateViewStream()))
                                {
                                    bw.Write(Utils.CutString("_" + processId + "_" + brstr, 0, 512));
                                }
                                if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["showdetail"]))
                                    System.Web.HttpContext.Current.Response.Write("<br/>write xpath: " + xpath + "  process :" + processId + ", old process: " + brstr);
                                return null;
                            }
                            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["showdetail"]))
                                System.Web.HttpContext.Current.Response.Write("<br/>output write xpath: " + xpath + "  process :" + processId + ", old process: " + brstr);
                        }
                    }
                }
#endif
                {
                    var node = _objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                    if (node == null) return null;
                    return node.Attributes != null ? _cs.RetrieveObject(node.Attributes["objectId"].Value) : null;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 通过指定的路径删除缓存中的对象
        /// </summary>
        /// <param name="xpath">分级对象的路径</param>
        public virtual void RemoveObject(string xpath)
        {
            lock (LockHelper)
            {
                try
                {
#if NET4      
                    if (GeneralConfigs.GetConfig().Webgarden > 1 && Environment.Version.Major >= 4)
                    {
                        //.net4框架下基于mmap实现跨进程共享信息，来实现当前web园进程内缓存更新后，其它web园进程无法得到信息已修改的标记
                        //方法摘要：通过htMapFile表记录共享内存的文件信息，这样可以提升访问共享信息的命中率（之前直接声明的方式命中率非常低且容易过多申请共享内存造成内存紧张）
                        //通过直接置空共享内存中数据(写入"none")，这样当别的进程再访问该共享内存时，发现共享内存中已为空（"即当前进程缓存数据要重新加载",详情参见上面的RetrieveObject(string xpath)）        
                        MemoryMappedFile file = htMapFile[xpath] as MemoryMappedFile;
                        if (file == null)
                        {
                            file = MemoryMappedFile.CreateOrOpen(xpath, 512, MemoryMappedFileAccess.ReadWrite);// MemoryMappedFileOptions.DelayAllocatePages, new MemoryMappedFileSecurity(), HandleInheritability.Inheritable);
                            htMapFile.Add(xpath, file);
                        }
                        using (BinaryWriter bw = new BinaryWriter(file.CreateViewStream()))
                        {
                            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.QueryString["showdetail"]))
                                System.Web.HttpContext.Current.Response.Write("<br/>xpath: " + xpath);
                            bw.Write("none");
                        }
                    }
#endif
                    {
                        var result = _objectXmlMap.SelectSingleNode(PrepareXpath(xpath));
                        //检查路径是否指向一个组或一个被缓存的实例元素
                        if (result != null && result.HasChildNodes)
                        {
                            //删除所有对象和子结点的信息
                            var objects = result.SelectNodes("*[@objectId]");
                            if (objects == null) return;
                            foreach (XmlNode node in objects)
                            {
                                if (node.Attributes == null) continue;
                                var objectId = node.Attributes["objectId"].Value;
                                if (node.ParentNode != null) node.ParentNode.RemoveChild(node);
                                //删除对象
                                _cs.RemoveObject(objectId);
                            }
                        }
                        else
                        {
                            //删除元素结点和相关的对象
                            if (result == null) return;
                            if (result.Attributes == null) return;
                            var objectId = result.Attributes["objectId"].Value;
                            if (result.ParentNode != null) result.ParentNode.RemoveChild(result);
                            _cs.RemoveObject(objectId);
                        }
                    }
                }
                catch (Exception exception)
                { throw new Exception(exception.Message); }
            }
        }

        /// <summary>
        /// 对象树形分级对象节点
        /// </summary>
        /// <param name="xpath">分级路径 location</param>
        /// <returns></returns>
        private XmlNode CreateNode(string xpath)
        {
            lock (LockHelper)
            {
                var xpathArray = xpath.Split('/');
                var root = "";
                XmlNode parentNode = _objectXmlMap;
                //建立相关节点
                for (var i = 1; i < xpathArray.Length; i++)
                {
                    var node = _objectXmlMap.SelectSingleNode(root + "/" + xpathArray[i]);
                    // 如果当前路径不存在则建立,否则设置当前路径到它的子路径上
                    if (node == null)
                    {
                        if (_objectXmlMap.OwnerDocument != null)
                        {
                            var newElement = _objectXmlMap.OwnerDocument.CreateElement(xpathArray[i]);
                            if (parentNode != null) parentNode.AppendChild(newElement);
                        }
                    }
                    //设置低一级的路径
                    root = root + "/" + xpathArray[i];
                    parentNode = _objectXmlMap.SelectSingleNode(root);
                }
                return parentNode;
            }
        }

        /// <summary>
        /// 整理 xpath 确保 '/'被删除 is removed
        /// </summary>
        /// <param name="xpath">分级地址</param>
        /// <returns></returns>
        private string PrepareXpath(string xpath)
        {
            lock (LockHelper)
            {
                var xpathArray = xpath.Split('/');
                return xpathArray.Where(s => s != "").Aggregate("/Cache", (current, s) => current + "/" + s);
            }
        }

        /// <summary>
        /// 加载指定的缓存策略
        /// </summary>
        /// <param name="ics"></param>
        public void LoadCacheStrategy(ICacheStrategy ics)
        {
            lock (LockHelper)
            {
                _cs = ics;
            }
        }

        /// <summary>
        /// 加载默认的缓存策略
        /// </summary>
        public void LoadDefaultCacheStrategy()
        {
            lock (LockHelper)
            {
                //当使用MemCached或redis时
                _cs = new DefaultCacheStrategy();
            }
        }

        /// <summary>
        /// 清空的有缓存数据, 注: 考虑效率问题，建议仅在需要时（如后台管理）使用.
        /// </summary>
        public int FlushAll()
        {
            return _cs.FlushAll();
        }
    }
}
