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
    
    public partial class Reports
    {
        public int ReportsId { get; set; }
        public string Description { get; set; }
        public Nullable<bool> Deleted { get; set; }
        public string TableProcedure { get; set; }
        public string ChartProcedure { get; set; }
        public string TableParameter { get; set; }
        public string ChartParameter { get; set; }
    }
}
