using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HuseKnives.Models
{
    public class Inventory
    {
        //Weapons ID in the SQL database
        public int Id { get; set; }

        #region Image, Name and Description
       // public HttpPostedFileBase WeaponImage { get; set; }

        [Required]
        [DisplayName("Knife Name")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The weapon name can only be 25 characters long.")]
        public string WeaponName { get; set; }

        [DisplayName("Knife's Description")]
        [DataType(DataType.MultilineText)]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "The weapon description can only be 255 characters long.")]
        public string WeaponDescription { get; set; }
        #endregion 

        //What type of steel was used to make the blade?
        [Required]
        [DisplayName("Blade Steel Type")]
        [DataType(DataType.Text)]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "The blade steel type can only be 25 characters long.")]
        public string BladeSteel { get; set; }

        //What type of handle was used?
        [Required]
        [DisplayName("Handle Material Type")]
        [DataType(DataType.Text)]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "The handle material can only be 50 characters long.")]
        public string HandleMaterial { get; set; }

        [Required]
        [DisplayName("Rockwell C Hardness Rating")]
        public int RCHardness { get; set; }

        [DisplayName("Knife Weight")]
        public int Weight { get; set; }

        [Required]
        [DisplayName("Price")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [Required]
        [DisplayName("Add Item to Website?")]
        //The weapon will never be deleted from the database, but instead moved to "inactive" status. This way the customer can reactivate the weapon's sale item if needed.
        public bool IsActive { get; set; }
    }
}