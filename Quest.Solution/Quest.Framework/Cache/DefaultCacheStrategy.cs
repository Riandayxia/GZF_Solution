using System;
using System.Web;
using System.Web.Caching;

namespace SuHui.Framework
{
    /// <summary>
    /// 默认缓存管理类
    /// </summary>
    public class DefaultCacheStrategy : ICacheStrategy
    {
        /// <summary>
        /// 默认缓存存活期为30分钟
        /// </summary>
        // ReSharper disable once InconsistentNaming
        protected int _timeOut = 30;


        /// <summary>
        /// 设置到期相对时间[单位: 秒] 
        /// </summary>
        public virtual int TimeOut
        {
            set { _timeOut = value > 0 ? value : 30; }
            get { return _timeOut > 0 ? _timeOut : 30; }
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        public virtual void AddObject(string objId, object o)
        {
            if (string.IsNullOrEmpty(objId) || o == null)
            {
                return;
            }

            if (TimeOut == 60)
            {
                HttpRuntime.Cache.Insert(objId, o, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, null);
            }
            else
            {
                HttpRuntime.Cache.Insert(objId, o, null, DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
        }

        /// <summary>
        /// 加入当前对象到缓存中
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="expire">到期时间,单位:分钟</param>
        public virtual void AddObject(string objId, object o, int expire)
        {
            if (string.IsNullOrEmpty(objId) || o == null)
            {
                return;
            }

            //表示永不过期
            if (expire == 0)
            {
                HttpRuntime.Cache.Insert(objId, o, null, DateTime.MaxValue, TimeSpan.Zero, CacheItemPriority.High, null);
            }
            else
            {
                HttpRuntime.Cache.Insert(objId, o, null, DateTime.Now.AddMinutes(expire), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, null);
            }
        }


        /// <summary>
        /// 加入当前对象到缓存中,并对相关文件建立依赖
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="files">监视的路径文件</param>
        public virtual void AddObjectWithFileChange(string objId, object o, string[] files)
        {
            if (string.IsNullOrEmpty(objId) || o == null)
            {
                return;
            }

            var callBack = new CacheItemRemovedCallback(OnRemove);

            var dep = new CacheDependency(files, DateTime.Now);

            HttpRuntime.Cache.Insert(objId, o, dep, DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, callBack);
        }



        /// <summary>
        /// 加入当前对象到缓存中,并使用依赖键
        /// </summary>
        /// <param name="objId">对象的键值</param>
        /// <param name="o">缓存的对象</param>
        /// <param name="dependKey">依赖关联的键值</param>
        public virtual void AddObjectWithDepend(string objId, object o, string[] dependKey)
        {
            if (string.IsNullOrEmpty(objId) || o == null)
            {
                return;
            }

            var callBack = new CacheItemRemovedCallback(OnRemove);

            var dep = new CacheDependency(null, dependKey, DateTime.Now);

            HttpRuntime.Cache.Insert(objId, o, dep, DateTime.Now.AddSeconds(TimeOut), System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.High, callBack);
        }

        /// <summary>
        /// 建立回调委托的一个实例
        /// </summary>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <param name="reason"></param>
        public void OnRemove(string key, object val, CacheItemRemovedReason reason)
        {
            switch (reason)
            {
                case CacheItemRemovedReason.DependencyChanged:
                    break;
                case CacheItemRemovedReason.Expired:
                    break;
                case CacheItemRemovedReason.Removed:
                    break;
                case CacheItemRemovedReason.Underused:
                    break;
            }
            System.Diagnostics.Debug.Write(val);
        }

        /// <summary>
        /// 删除缓存对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        public virtual void RemoveObject(string objId)
        {
            if (string.IsNullOrEmpty(objId))
            {
                return;
            }
            HttpRuntime.Cache.Remove(objId);
        }


        /// <summary>
        /// 返回一个指定的对象
        /// </summary>
        /// <param name="objId">对象的关键字</param>
        /// <returns>对象</returns>
        public virtual object RetrieveObject(string objId)
        {
            if (string.IsNullOrEmpty(objId))
            {
                return null;
            }
            return HttpRuntime.Cache.Get(objId);
        }

        /// <summary>
        /// 清空的有缓存数据
        /// </summary>
        public virtual int FlushAll()
        {
            var cacheEnum = HttpRuntime.Cache.GetEnumerator();
            int cacheCount = 0;
            while (cacheEnum.MoveNext())
            {
                cacheCount++;
                HttpRuntime.Cache.Remove(cacheEnum.Key.ToString());
            }
            return cacheCount;
        }
    }
}
