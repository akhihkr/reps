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
    
    public partial class Property
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Property()
        {
            this.PropertyDetail = new HashSet<PropertyDetail>();
        }
    
        public int PropertyID { get; set; }
        public int DealID { get; set; }
        public string PropertyDescription { get; set; }
        public string LegalDescription { get; set; }
        public int AddressID { get; set; }
        public int PropertyTypeID { get; set; }
        public bool Verified { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public bool Deleted { get; set; }
        public Nullable<System.Guid> PropertyGUID { get; set; }
    
        public virtual Deal Deal { get; set; }
        public virtual PropertyType PropertyType { get; set; }
        public virtual Address Address { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PropertyDetail> PropertyDetail { get; set; }
    }
}
