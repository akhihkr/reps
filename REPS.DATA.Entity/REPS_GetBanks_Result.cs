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
    
    public partial class REPS_GetBanks_Result
    {
        public int BankID { get; set; }
        public int EntityID { get; set; }
        public string BankName { get; set; }
        public string UniversalSortCode { get; set; }
        public string SwiftCode { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public bool Deleted { get; set; }
        public Nullable<long> num { get; set; }
    }
}
