using System.Collections.Generic;
using Majid.Application.Navigation;

namespace Majid.Web.Models.MajidUserConfiguration
{
    public class MajidUserNavConfigDto
    {
        public Dictionary<string, UserMenu> Menus { get; set; }
    }
}