using System;
using System.Collections.Generic;

namespace WebCarFist.Models
{
    public partial class Account
    {
        public Account()
        {
            Invoice = new HashSet<Invoice>();
            RoleAccount = new HashSet<RoleAccount>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Stautus { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<Invoice> Invoice { get; set; }
        public virtual ICollection<RoleAccount> RoleAccount { get; set; }
    }
}
