//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EVENT
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EVENT()
        {
            this.COLLECTION_ACTIVITY = new HashSet<COLLECTION_ACTIVITY>();
            this.COMMENTTHUMBSUP = new HashSet<COMMENTTHUMBSUP>();
            this.JOIN_ACTIVITY = new HashSet<JOIN_ACTIVITY>();
            this.REPLYCOMMENT = new HashSet<REPLYCOMMENT>();
            this.THUMBSUP_ACTIVITY = new HashSet<THUMBSUP_ACTIVITY>();
        }
    
        public int EVENT_ID { get; set; }
        public string EVENT_NAME { get; set; }
        public decimal EVENT_TYPE { get; set; }
        public string EVENT_CONTENT { get; set; }
        public string EVENT_SITE { get; set; }
        public System.DateTime EVENT_DATE { get; set; }
        public int EA_ID { get; set; }
        public System.DateTime EVENT_START_TIME { get; set; }
        public System.DateTime EVENT_END_TIME { get; set; }
        public decimal STAR_NUMBER { get; set; }
        public decimal PARTICIPANT_NUMBER { get; set; }
        public long INITIATOR_ID { get; set; }
        public string EVENT_STATE { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COLLECTION_ACTIVITY> COLLECTION_ACTIVITY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<COMMENTTHUMBSUP> COMMENTTHUMBSUP { get; set; }
        public virtual EVENTCHECK EVENTCHECK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<JOIN_ACTIVITY> JOIN_ACTIVITY { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<REPLYCOMMENT> REPLYCOMMENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<THUMBSUP_ACTIVITY> THUMBSUP_ACTIVITY { get; set; }
    }
}
