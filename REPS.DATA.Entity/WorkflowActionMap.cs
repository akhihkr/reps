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
    
    public partial class WorkflowActionMap
    {
        public int ID { get; set; }
        public int WorkflowActionID { get; set; }
        public int WorkflowTaskID { get; set; }
        public bool AllowAttachments { get; set; }
        public Nullable<int> ParticipantTypeID { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<int> CreatedByUserID { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<bool> IsMandatory { get; set; }
    }
}