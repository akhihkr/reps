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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Alerts = new HashSet<Alerts>();
            this.Audit = new HashSet<Audit>();
            this.Deal = new HashSet<Deal>();
            this.UserDeal = new HashSet<UserDeal>();
            this.UserDocument = new HashSet<UserDocument>();
            this.UserRole = new HashSet<UserRole>();
        }
    
        public int UserID { get; set; }
        public Nullable<int> EntityID { get; set; }
        public int TitleID { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string IdentityNumber { get; set; }
        public string PassportNumber { get; set; }
        public Nullable<int> PassportCountryID { get; set; }
        public string TaxID { get; set; }
        public System.DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public Nullable<decimal> Telephone { get; set; }
        public Nullable<decimal> MobileNumber { get; set; }
        public Nullable<decimal> FaxNumber { get; set; }
        public string Email { get; set; }
        public Nullable<int> JobTitleID { get; set; }
        public bool Verified { get; set; }
        public System.DateTime DateCreated { get; set; }
        public Nullable<System.DateTime> DateModified { get; set; }
        public bool Deleted { get; set; }
        public Nullable<bool> HeaderTabToggle { get; set; }
        public string pwd { get; set; }
        public string TokenId { get; set; }
        public string AspNetUsersId { get; set; }
        public string ProfilePicture { get; set; }
        public Nullable<int> WorkflowID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Alerts> Alerts { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audit> Audit { get; set; }
        public virtual Country Country { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Deal> Deal { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDeal> UserDeal { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserDocument> UserDocument { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRole> UserRole { get; set; }
    }
}
