using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace TradingPlatform.Infrastructure.Identity
{
    public class ApplicationUser: IdentityUser 
    {
        public string FullName { get; set; } = default!  ;
        public DateTime DateOfBirth { get; set; }   
        public bool IsKycVerified { get; set; }

    }

}
