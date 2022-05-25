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
    
    public partial class UserDocument
    {
        public long DocumentID { get; set; }
        public int EntityID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> ParticipantID { get; set; }
        public int MimeTypeID { get; set; }
        public string DocumentName { get; set; }
        public byte[] FileContent { get; set; }
        public string FileHash { get; set; }
    
        public virtual MimeType MimeType { get; set; }
        public virtual Entity Entity { get; set; }
        public virtual User User { get; set; }
        public virtual Participant Participant { get; set; }
    }
}
