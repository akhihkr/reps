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
    
    public partial class REPS_GetAlerts_ByAlertID_Result
    {
        public long ID { get; set; }
        public Nullable<System.Guid> AlertsGUID { get; set; }
        public System.DateTime StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public string EventName { get; set; }
        public string Comment { get; set; }
        public string Location { get; set; }
    }
}
