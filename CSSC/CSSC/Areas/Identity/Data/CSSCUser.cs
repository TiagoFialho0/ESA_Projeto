using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CSSC.Areas.Identity.Data;

// Add profile data for application users by adding properties to the CSSCUser class
public class CSSCUser : IdentityUser
{
    //[DataType(DataType.EmailAddress)]
   // public string Email { get; set; }
    //[DataType(DataType.Password)]
   // public string Password { get; set; }
    //[DataType(DataType.Date)]
    public string UtDataDeNascimento {  get; set; }
    public string UtNIF { get; set; }
    public string UtMorada {  get; set; }

    // public string UtContactoTelefonico { get; set; }

}

