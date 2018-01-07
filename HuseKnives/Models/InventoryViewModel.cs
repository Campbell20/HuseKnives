using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HuseKnives.Models
{
    public class InventoryViewModel
    {
        public Inventory Inventories { get; set; }
        public Image Images { get; set; }

        //[Required, FileExtensions(Extensions = "jpg,gif,png",
        //    ErrorMessage = "Specify a file")]
        [Required]
        public HttpPostedFileBase File { get; set; }
    }
}