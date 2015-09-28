using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.ModelDB
{
    /// <summary>
    /// 模型父类
    /// </summary>
    public abstract class MyModelBase
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public Nullable<System.DateTime> CreateTime { get; set; } 
        /// <summary>
        /// 修改时间
        /// </summary>
        public Nullable<System.DateTime> UpTime { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        //public Guid Id { set; get; }

    }
}
