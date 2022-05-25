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
    
    public partial class REPS_GetProperties_Result
    {
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
        public string PropertyAddress { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string PropertyDetails { get; set; }
        public int PropertyDetailID { get; set; }
        public string Geo { get; set; }
        public string Size { get; set; }
        public Nullable<System.Guid> PropertyGUID { get; set; }
        public Nullable<System.Guid> PropertyDetailGUID { get; set; }
        public Nullable<System.Guid> AddressGUID { get; set; }
        public Nullable<long> num { get; set; }
    }
}