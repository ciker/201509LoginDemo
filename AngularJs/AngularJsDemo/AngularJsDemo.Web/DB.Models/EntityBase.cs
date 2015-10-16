using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AngularJsDemo.Web.DB.Models
{
    [Serializable]
    public abstract class EntityBase<TKey>
    {
        protected EntityBase()
        {
            IsDeleted = false;
            AddDate = DateTime.Now;
        }

        public TKey ID { get; set; }

        public bool IsDeleted { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddDate { get; set; }
    }
}