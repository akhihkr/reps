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
    
    public partial class UserAction
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserAction()
        {
            this.UserRoleAction = new HashSet<UserRoleAction>();
        }
    
        public int UserActionID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string AbsoluteUri { get; set; }
        public bool Deleted { get; set; }
        public Nullable<bool> Editable { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRoleAction> UserRoleAction { get; set; }
    }
}
