using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDevelopment_Assignment2.Models
{
    public class EntityBase
    {
        public DateTime? Create { get; set; }
        public DateTime? Updated { get; set; }
        public bool Deleted { get; set; }
        public EntityBase()
        {
            Deleted = false;
        }
        public virtual int IdentityID()
        {
            return 0;
        }
        public virtual object[] IdentityID(bool dummy = true)
        {
            return new List<object>().ToArray();
        }
    }
}
