//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EsemkaRestaurant
{
    using System;
    using System.Collections.Generic;
    
    public partial class Detailorder
    {
        public int DetailID { get; set; }
        public string OrderID { get; set; }
        public int MenuID { get; set; }
        public int Qty { get; set; }
        public int Price { get; set; }
        public string status { get; set; }
    
        public virtual Msmenu Msmenu { get; set; }
        public virtual Msmenu Msmenu1 { get; set; }
    }
}
