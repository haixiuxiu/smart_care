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
    
    public partial class THUMBSUP_ACTIVITY
    {
        public int USER_ID { get; set; }
        public int ACTIVITY_ID { get; set; }
        public System.DateTime THUMBSUP_TIME { get; set; }
        public int THUMBSUP_NUMBER { get; set; }
    
        public virtual CARETAKER CARETAKER { get; set; }
        public virtual COMMUNITY_DOCTOR COMMUNITY_DOCTOR { get; set; }
        public virtual THEELDERLY THEELDERLY { get; set; }
        public virtual EVENT EVENT { get; set; }
    }
}
