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
    
    public partial class VariableType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VariableType()
        {
            this.TransactionDetail = new HashSet<TransactionDetail>();
        }
    
        public int ID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public Nullable<int> DefaultSortOrder { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TransactionDetail> TransactionDetail { get; set; }
    }
}
