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
    
    public partial class REPS_GetEditPaymentFields_Result
    {
        public System.Guid TransactionDetailGUID { get; set; }
        public int WorkflowTaskID { get; set; }
        public Nullable<int> WorkflowActionID { get; set; }
        public long TransactionID { get; set; }
        public long TransactionDetailID { get; set; }
        public Nullable<int> WorkflowActionVarID { get; set; }
        public Nullable<int> WorkflowVariableID { get; set; }
        public bool IsRequired { get; set; }
        public string Value { get; set; }
    }
}
