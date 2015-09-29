using System;
using System.Collections.Generic;
using System.Text;

namespace WebChatSDK.PHPLibrary
{
    /// <summary>
    /// 表示键和值的集合。
    /// </summary>
    public class PHPArray : Dictionary<object, object>
    {
        /// <summary>
        /// 忽略大小写
        /// </summary>
        public bool IgnoreCase { get; private set; }
        private int maxKey;

        #region 构造方法
        /// <summary>
        /// 初始化 <see cref="PHPArray"/> 类的新实例，该实例为空且具有默认的初始容量，并使用键类型的默认相等比较器。
        /// </summary>
        public PHPArray() :
            base()
        {
            this.IgnoreCase = false;
        }

        /// <summary>
        /// 初始化 <see cref="PHPArray"/> 类的新实例，该实例为空且具有默认的初始容量，并设置忽略大小写。
        /// </summary>
        /// <param name="ignoreCase">忽略大小写</param>
        public PHPArray(bool ignoreCase) :
            base()
        {
            this.IgnoreCase = ignoreCase;
        }

        /// <summary>
        /// 初始化 <see cref="PHPArray"/> 类的新实例，该实例为空且具有指定的初始容量，并为键类型使用默认的相等比较器。
        /// </summary>
        /// <param name="capacity"><see cref="PHPArray"/> 可包含的初始元素数。</param>
        public PHPArray(int capacity) :
            base(capacity)
        {
            this.IgnoreCase = false;
        }

        /// <summary>
        /// 初始化 <see cref="PHPArray"/> 类的新实例，该实例为空且具有指定的初始容量，并设置忽略大小写。
        /// </summary>
        /// <param name="capacity"><see cref="PHPArray"/> 可包含的初始元素数。</param>
        /// <param name="ignoreCase">忽略大小写</param>
        public PHPArray(int capacity, bool ignoreCase) :
            base(capacity)
        {
            this.IgnoreCase = ignoreCase;
        }

        public PHPArray(IDictionary<object, object> dictionary) :
            base(dictionary)
        {
            this.IgnoreCase = false;
        }
        #endregion

        #region 将指定的键和值添加到字典中。
        /// <summary>
        /// 将指定的键和值添加到字典中。
        /// </summary>
        /// <param name="key">要添加的元素的键。</param>
        /// <param name="value">要添加的元素的值。对于引用类型，该值可以为 null。</param>
        private void AddObject(object key, object value)
        {
            if (key == null)
            {
                throw new ArgumentNullException("元素的键为null");
            }
            if (!(key is int) && !(key is string))
            {
                throw new ArgumentException("元素的键不是字符串类型或者不是整型");
            }
            if ((key is int && (int)key < 0) || (key is string && ((string)key).Length == 0))
            {
                throw new ArgumentNullException("元素的键不能为空");
            }
            if (this.IgnoreCase && key is string)
            {
                key = ((string)key).ToLower();
            }
            if (key is int && maxKey < (int)key)
            {
                maxKey = (int)key;
            }
            if (base.ContainsKey(key))
            {
                base[key] = value;
            }
            else
            {
                base.Add(key, value);
            }
        }

        /// <summary>
        /// 将指定的键和值添加到字典中。
        /// </summary>
        /// <param name="key">要添加的元素的键。</param>
        /// <param name="value">要添加的元素的值。对于引用类型，该值可以为 null。</param>
        [System.Obsolete("请使用int或string类型的key", true)]
        public new void Add(object key, object value)
        {
        }

        /// <summary>
        /// 将指定的键和值添加到字典中。
        /// </summary>
        /// <param name="value">要添加的元素的值。对于引用类型，该值可以为 null。</param>
        public void Add(object value)
        {
            this.AddObject(maxKey == 0 ? 0 : maxKey++, value);
            if (maxKey == 0)
            {
                maxKey = 1;
            }
        }

        /// <summary>
        /// 将指定的键和值添加到字典中。
        /// </summary>
        /// <param name="key">要添加的元素的键。</param>
        /// <param name="value">要添加的元素的值。对于引用类型，该值可以为 null。</param>
        public void Add(int key, object value)
        {
            this.AddObject(key, value);
        }

        /// <summary>
        /// 将指定的键和值添加到字典中。
        /// </summary>
        /// <param name="key">要添加的元素的键。</param>
        /// <param name="value">要添加的元素的值。对于引用类型，该值可以为 null。</param>
        public void Add(string key, object value)
        {
            this.AddObject(key, value);
        }
        #endregion

        #region 将指定的键和值更新到字典中。
        /// <summary>
        /// 将指定的键和值更新到字典中。
        /// </summary>
        /// <param name="key">要更新的元素的键。</param>
        /// <param name="value">要更新的元素的值。对于引用类型，该值可以为 null。</param>
        public void Updata(int key, object value)
        {
            this.AddObject(key, value);
        }

        /// <summary>
        /// 将指定的键和值更新到字典中。
        /// </summary>
        /// <param name="key">要更新的元素的键。</param>
        /// <param name="value">要更新的元素的值。对于引用类型，该值可以为 null。</param>
        public void Updata(string key, object value)
        {
            this.AddObject(key, value);
        }
        #endregion

        #region 确定 PHPArray 是否包含指定的键。
        /// <summary>
        /// 确定 <see cref="PHPArray"/> 是否包含指定的键。
        /// </summary>
        /// <param name="key">要在 <see cref="PHPArray"/> 中定位的键。</param>
        /// <returns>如果 <see cref="PHPArray"/> 包含具有指定键的元素，则为 true；否则为false。</returns>
        private bool ContainsKeyObject(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("元素的键为null");
            }
            if (!(key is int) && !(key is string))
            {
                throw new ArgumentException("元素的键不是字符串类型或者不是整型");
            }
            if ((key is int && (int)key < 0) || (key is string && ((string)key).Length == 0))
            {
                throw new ArgumentNullException("元素的键不能为空");
            }
            if (this.IgnoreCase && key is string)
            {
                key = ((string)key).ToLower();
            }
            return base.ContainsKey(key);
        }

        /// <summary>
        /// 确定 <see cref="PHPArray"/> 是否包含指定的键。
        /// </summary>
        /// <param name="key">要在 <see cref="PHPArray"/> 中定位的键。</param>
        /// <returns>如果 <see cref="PHPArray"/> 包含具有指定键的元素，则为 true；否则为false。</returns>
        [System.Obsolete("请使用int或string类型的key", true)]
        public new bool ContainsKey(object key)
        {
            return false;
        }

        /// <summary>
        /// 确定 <see cref="PHPArray"/> 是否包含指定的键。
        /// </summary>
        /// <param name="key">要在 <see cref="PHPArray"/> 中定位的键。</param>
        /// <returns>如果 <see cref="PHPArray"/> 包含具有指定键的元素，则为 true；否则为false。</returns>
        public bool ContainsKey(int key)
        {
            return ContainsKeyObject(key);
        }

        /// <summary>
        /// 确定 <see cref="PHPArray"/> 是否包含指定的键。
        /// </summary>
        /// <param name="key">要在 <see cref="PHPArray"/> 中定位的键。</param>
        /// <returns>如果 <see cref="PHPArray"/> 包含具有指定键的元素，则为 true；否则为false。</returns>
        public bool ContainsKey(string key)
        {
            return ContainsKeyObject(key);
        }
        #endregion

        #region 从 PHPArray 中移除所指定的键的值。
        /// <summary>
        /// 从 <see cref="PHPArray"/> 中移除所指定的键的值。
        /// </summary>
        /// <param name="key">要移除的元素的键。</param>
        /// <returns>如果成功找到并移除该元素，则为 true；否则为 false。如果在 <see cref="PHPArray"/> 中没有找到 key，此方法则返回 false。</returns>
        private bool RemoveObject(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("元素的键为null");
            }
            if (!(key is int) && !(key is string))
            {
                throw new ArgumentException("元素的键不是字符串类型或者不是整型");
            }
            if ((key is int && (int)key < 0) || (key is string && ((string)key).Length == 0))
            {
                throw new ArgumentNullException("元素的键不能为空");
            }
            if (this.IgnoreCase && key is string)
            {
                key = ((string)key).ToLower();
            }
            return base.Remove(key);
        }

        /// <summary>
        /// 从 <see cref="PHPArray"/> 中移除所指定的键的值。
        /// </summary>
        /// <param name="key">要移除的元素的键。</param>
        /// <returns>如果成功找到并移除该元素，则为 true；否则为 false。如果在 <see cref="PHPArray"/> 中没有找到 key，此方法则返回 false。</returns>
        [System.Obsolete("请使用int或string类型的key", true)]
        public new bool Remove(object key)
        {
            return false;
        }

        /// <summary>
        /// 从 <see cref="PHPArray"/> 中移除所指定的键的值。
        /// </summary>
        /// <param name="key">要移除的元素的键。</param>
        /// <returns>如果成功找到并移除该元素，则为 true；否则为 false。如果在 <see cref="PHPArray"/> 中没有找到 key，此方法则返回 false。</returns>
        public bool Remove(int key)
        {
            return RemoveObject(key);
        }

        /// <summary>
        /// 从 <see cref="PHPArray"/> 中移除所指定的键的值。
        /// </summary>
        /// <param name="key">要移除的元素的键。</param>
        /// <returns>如果成功找到并移除该元素，则为 true；否则为 false。如果在 <see cref="PHPArray"/> 中没有找到 key，此方法则返回 false。</returns>
        public bool Remove(string key)
        {
            return RemoveObject(key);
        }
        #endregion

        #region 获取或设置与指定的键相关联的值。
        /// <summary>
        /// 获取或设置与指定的键相关联的值。
        /// </summary>
        /// <param name="key">要获取或设置的值的键。</param>
        /// <returns>与指定的键相关联的值。</returns>
        private ArrayObject Get(object key)
        {
            if (key == null)
            {
                throw new ArgumentNullException("元素的键为null");
            }
            if (!(key is int) && !(key is string))
            {
                throw new ArgumentException("元素的键不是字符串类型或者不是整型");
            }
            if ((key is int && (int)key < 0) || (key is string && ((string)key).Length == 0))
            {
                throw new ArgumentNullException("元素的键不能为空");
            }
            if (this.IgnoreCase && key is string)
            {
                key = ((string)key).ToLower();
            }
            if (base.ContainsKey(key))
            {
                return new ArrayObject(base[key]);
            }
            else
            {
                return new ArrayObject(null);
            }
        }

        /// <summary>
        /// 获取或设置与指定的键相关联的值。
        /// </summary>
        /// <param name="key">要获取或设置的值的键。</param>
        /// <returns>与指定的键相关联的值。如果找不到指定的键，get 操作便会引发 System.Collections.Generic.KeyNotFoundException，而 set 操作会创建一个具有指定键的新元素。</returns>
        [System.Obsolete("请使用int或string类型的key", true)]
        public new object this[object key]
        {
            get { return null; }
            set { }
        }

        /// <summary>
        /// 获取或设置与指定的键相关联的值。
        /// </summary>
        /// <param name="key">要获取或设置的值的键。</param>
        /// <returns>与指定的键相关联的值。如果找不到指定的键，get 操作便会引发 System.Collections.Generic.KeyNotFoundException，而 set 操作会创建一个具有指定键的新元素。</returns>
        public ArrayObject this[string key]
        {
            get { return Get(key); }
            set { Add(key, value.Value); }
        }

        /// <summary>
        /// 获取或设置与指定的键相关联的值。
        /// </summary>
        /// <param name="key">要获取或设置的值的键。</param>
        /// <returns>与指定的键相关联的值。如果找不到指定的键，get 操作便会引发 System.Collections.Generic.KeyNotFoundException，而 set 操作会创建一个具有指定键的新元素。</returns>
        public ArrayObject this[int key]
        {
            get { return Get(key); }
            set { Add(key, value.Value); }
        }
        #endregion

        public string Print()
        {
            return PHP.print_r(this, true);
        }

        public string Join(string sp)
        {
            StringBuilder sb = new StringBuilder();
            int i = 0;
            foreach (KeyValuePair<object, object> p in this)
            {
                i++;
                if (i > 1)
                {
                    sb.Append(sp);
                }
                sb.Append(p.Value);
            }
            return sb.ToString();
        }
    }
}
