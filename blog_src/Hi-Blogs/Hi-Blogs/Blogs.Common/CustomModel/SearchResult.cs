using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blogs.Common.CustomModel
{
    public class SearchResult
    {
        public SearchResult() { }

        public SearchResult(string title, string content, string url, string blogTag, int id, int clickQuantity, int flag)
        {
            this.blogTag = blogTag;
            this.clickQuantity = clickQuantity;
            this.content = content;
            this.id = id;
            this.url = url;
            this.title = title;
            this.flag = flag;
        }
        /// <summary>
        /// 标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 正文内容
        /// </summary>
        public string content { get; set; }
        /// <summary>
        /// url地址
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// tag标签
        /// </summary>
        public string blogTag { get; set; }
        /// <summary>
        /// 唯一id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 点击量
        /// </summary>
        public int clickQuantity { get; set; }
        /// <summary>
        /// 标记（用户） 
        /// </summary>
        public int flag { get; set; }
    }
}
