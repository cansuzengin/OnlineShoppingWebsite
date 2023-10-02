using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcOnlineShopping.Models.Entity;
namespace MvcOnlineShopping.Models.Classes
{
    public class Class1
    {
        public IEnumerable<TBLProduct> value1 { get; set; }
        public IEnumerable<TBLBrand> value2 { get; set; }
        public IEnumerable<TBLBasket> value3 { get; set; }
        public IEnumerable<TBLCategory> value4 { get; set; }
        public IEnumerable<TBLLike> value5 { get; set; }
        public IEnumerable<TBLGender> value6 { get; set; }
        public IEnumerable<TBLComments> value7 { get; set; }
        public IEnumerable<TBLOrder> value8 { get; set; }
        public IEnumerable<TBLCargo> value9 { get; set; }
        public IEnumerable<TBLAddress> value10 { get; set; }
    }
}