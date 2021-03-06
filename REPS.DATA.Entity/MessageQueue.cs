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
    
    public partial class MessageQueue
    {
        public long MessageID { get; set; }
        public string KeyName { get; set; }
        public int ForeignKey { get; set; }
        public int MimeTypeID { get; set; }
        public bool Inbound { get; set; }
        public string FileName { get; set; }
        public byte[] FileContent { get; set; }
        public string FileHash { get; set; }
        public System.DateTime DateCreated { get; set; }
        public System.DateTime DateModified { get; set; }
        public int MessageStatusID { get; set; }
        public bool Deleted { get; set; }
    
        public virtual MessageStatus MessageStatus { get; set; }
    }
}
