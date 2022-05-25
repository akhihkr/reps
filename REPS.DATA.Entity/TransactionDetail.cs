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
    
    public partial class TransactionDetail
    {
        public System.Guid TransactionDetailGUID { get; set; }
        public long TransactionDetailID { get; set; }
        public long TransactionID { get; set; }
        public Nullable<int> WorkflowActionVarID { get; set; }
        public int VariableTypeID { get; set; }
        public string Value { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Transaction Transaction { get; set; }
        public virtual VariableType VariableType { get; set; }
        public virtual WorkflowActionVariable WorkflowActionVariable { get; set; }
    }
}