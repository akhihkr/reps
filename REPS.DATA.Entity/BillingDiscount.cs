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
    
    public partial class BillingDiscount
    {
        public long BillingDiscountID { get; set; }
        public int UserID { get; set; }
        public int BillingRateID { get; set; }
        public string DiscountReference { get; set; }
        public int DiscountTypeID { get; set; }
        public decimal DiscountValue { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public bool Deleted { get; set; }
    
        public virtual DiscountType DiscountType { get; set; }
    }
}
