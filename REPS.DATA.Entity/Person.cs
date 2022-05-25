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
    
    public partial class Person
    {
        public int PersonID { get; set; }
        public Nullable<int> OrganizationID { get; set; }
        public Nullable<int> TitleID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public Nullable<int> IdentityTypeID { get; set; }
        public string IdentityNumber { get; set; }
        public string PassportNumber { get; set; }
        public Nullable<int> PassportCountryID { get; set; }
        public string TaxID { get; set; }
        public Nullable<System.DateTime> BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public decimal Telephone { get; set; }
        public Nullable<decimal> FaxNumber { get; set; }
        public Nullable<decimal> MobileNumber { get; set; }
        public string Email { get; set; }
        public Nullable<int> JobTitleID { get; set; }
        public bool Verified { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public bool Deleted { get; set; }
    
        public virtual Country Country { get; set; }
        public virtual IdentityType IdentityType { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual Organization Organization { get; set; }
        public virtual Title Title { get; set; }
    }
}