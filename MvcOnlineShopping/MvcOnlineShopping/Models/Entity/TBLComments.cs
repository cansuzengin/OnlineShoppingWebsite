//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcOnlineShopping.Models.Entity
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBLComments
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TBLComments()
        {
            this.Status = false;
        }
    
        public int Id { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<int> MemberId { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> Datetime { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<bool> Status { get; set; }
    
        public virtual TBLMember TBLMember { get; set; }
        public virtual TBLProduct TBLProduct { get; set; }
    }
}
