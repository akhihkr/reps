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
    
    public partial class REPS_GetCompletedActionsByWorkflowID_Result
    {
        public int WorkflowTaskID { get; set; }
        public int TaskID { get; set; }
        public string TaskName { get; set; }
        public int WorkflowActionID { get; set; }
        public string DisplayName { get; set; }
        public Nullable<int> WorkflowTaskOrder { get; set; }
        public string TaskControl { get; set; }
        public string Tick { get; set; }
    }
}
