using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GCFinalProject.Models
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public virtual ICollection<Event>Events { get; set; }
    }
}