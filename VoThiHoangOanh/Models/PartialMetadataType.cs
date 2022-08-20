using System;
using System.Collections.Generic;
using System.Linq;
using VoThiHoangOanh.Context;
using VoThiHoangOanh.Models;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VoThiHoangOanh.Context
{
    [MetadataType(typeof(UserMasterData))]
    public partial class User
    {
        
    }

    [MetadataType(typeof(ProductMasterData))]
    public partial class Product
    {
        [NotMapped]
        public System.Web.HttpPostedFileBase ImageUpLoad { get; set; }
    }

}