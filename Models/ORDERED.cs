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
    
    public partial class ORDERED
    {
        public int ORDER_ID { get; set; }
        public int CARETAKER_ID { get; set; }
        public int ELDERLY_ID { get; set; }
        public System.DateTime ORDER_TIME { get; set; }
        public decimal ORDER_TYPE { get; set; }
        public string ORDER_STATE { get; set; }
        public decimal ORDER_PRICE { get; set; }
        public string ORDER_DURATION { get; set; }
        public string ORDER_GENDER { get; set; }
        public string CARETAKER_EXPERIENCE { get; set; }
    
        public virtual CARETAKER CARETAKER { get; set; }
        public virtual THEELDERLY THEELDERLY { get; set; }
    }
}
