using System.Threading.Tasks;
using Majid.Web.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Majid.AspNetCore.Mvc.Controllers
{
    public class MajidUserConfigurationController: MajidController
    {
        private readonly MajidUserConfigurationBuilder _majidUserConfigurationBuilder;

        public MajidUserConfigurationController(MajidUserConfigurationBuilder majidUserConfigurationBuilder)
        {
            _majidUserConfigurationBuilder = majidUserConfigurationBuilder;
        }

        public async Task<JsonResult> GetAll()
        {
            var userConfig = await _majidUserConfigurationBuilder.GetAll();
            return Json(userConfig);
        }
    }
}