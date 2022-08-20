using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoThiHoangOanh.Context;

namespace VoThiHoangOanh.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}