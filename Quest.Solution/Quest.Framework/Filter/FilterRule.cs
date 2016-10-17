using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Quest.Framework
{
    /// <summary>
    /// 过滤规则
    /// </summary>
    [DataContract]
    public class FilterRule
    {
        public FilterRule()
        {
        }
        public FilterRule(string field, object value)
            : this(field, value, "equal")
        {
        }

        public FilterRule(string field, object value, string op)
        {
            this.Field = field;
            this.Value = value;
            this.Op = op;
        }

        [DataMember]
        public String Field { get; set; }
        [DataMember]
        public Object Value { get; set; }
        [DataMember]
        public String Op { get; set; }
        [DataMember]
        public String Type { get; set; }
    }

    /// <summary>
    /// 对应前台 ligerFilter 的检索规则数据
    /// </summary>
    [DataContract]
    public class FilterGroup
    {
        [DataMember]
        public IEnumerable<FilterRule> Rules { get; set; }
        [DataMember]
        public String Op { get; set; }
        [DataMember]
        public IEnumerable<FilterGroup> Groups { get; set; }
    }
}
