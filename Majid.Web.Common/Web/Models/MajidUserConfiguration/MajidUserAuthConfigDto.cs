using System.Collections.Generic;

namespace Majid.Web.Models.MajidUserConfiguration
{
    public class MajidUserAuthConfigDto
    {
        public Dictionary<string,string> AllPermissions { get; set; }

        public Dictionary<string, string> GrantedPermissions { get; set; }
        
    }
}