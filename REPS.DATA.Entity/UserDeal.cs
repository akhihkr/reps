//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace REPS.DATA.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserDeal
    {
        public int UserRoleID { get; set; }
        public int UserID { get; set; }
        public int DealID { get; set; }
        public bool IsActive { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public bool Deleted { get; set; }
        public Nullable<int> Order { get; set; }
        public string LastViewName { get; set; }
    
        public virtual Deal Deal { get; set; }
        public virtual User User { get; set; }
    }
}
