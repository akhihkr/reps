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
    
    public partial class REPS_GetEntities_Result
    {
        public int EntityID { get; set; }
        public int OrganizationTypeID { get; set; }
        public string Name { get; set; }
        public string RegistrationNumber { get; set; }
        public string LegalName { get; set; }
        public string AlternateName { get; set; }
        public string VatID { get; set; }
        public decimal Telephone { get; set; }
        public Nullable<decimal> FaxNumber { get; set; }
        public string Email { get; set; }
        public bool Verified { get; set; }
        public int AddressTypeID { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public int CountryID { get; set; }
        public string PostalCode { get; set; }
        public int ParentEntityID { get; set; }
        public bool Deleted { get; set; }
        public Nullable<bool> DataVerification { get; set; }
        public Nullable<System.Guid> EntityGUID { get; set; }
        public Nullable<long> num { get; set; }
    }
}
