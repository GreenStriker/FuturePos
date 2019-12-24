using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using vms.entity.viewModels;

namespace vms.entity.models
{


    //[ModelMetadataType(typeof(UserMetadata))]
    public partial class Branch : URF.Core.EF.Trackable.Entity
    {
        //[NotMapped]
        ////public string EncryptedId { get; set; }
        ////public IEnumerable<SelectListItems> Roles;
        ////public IEnumerable<SelectListItems> UserTypes;
        //[NotMapped]
        //public string jsonobj { get; set; }

    }
    //public class UserMetadata 
    //{
    //    [Required]
    //    [StringLength(64, ErrorMessage = "UserName cannot be longer than 50 characters.")]
    //    public string UserName { get; set; }
    //    [Display(Name = "Name")]       
    //    public string FullName { get; set; }

    //    [Display(Name = "Email")]
       
    //    [DataType(DataType.EmailAddress)]
    //    [EmailAddress]
    //    public string EmailAddress { get; set; }

    //    [Display(Name = "UserType")]
    //    [Required]
    //    public int UserTypeId { get; set; }
       
    //    [Display(Name = "Role")]
    //     [Required]
    //    public int RoleId { get; set; }

    //    [Display(Name = "Organization")]
    //    public int OrganizationId { get; set; }

    //    [Required]
    //    [Display(Name = "Company Representative")]
    //    public bool IsCompanyRepresentative { get; set; }

    //    [RegularExpression("^[0-9]*$", ErrorMessage = "Entered Mobile is not valid.")]
    //    [StringLength(11, ErrorMessage = "Mobile cannot be longer than 11 characters.")]
    //    public string Mobile { get; set; }
    //}


}