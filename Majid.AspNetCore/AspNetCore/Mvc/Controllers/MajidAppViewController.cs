using System;
using Majid.Auditing;
using Majid.Domain.Uow;
using Majid.Extensions;
using Majid.Runtime.Validation;
using Microsoft.AspNetCore.Mvc;

namespace Majid.AspNetCore.Mvc.Controllers
{
    public class MajidAppViewController : MajidController
    {
        [DisableAuditing]
        [DisableValidation]
        [UnitOfWork(IsDisabled = true)]
        public ActionResult Load(string viewUrl)
        {
            if (viewUrl.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(viewUrl));
            }

            return View(viewUrl.EnsureStartsWith('~'));
        }
    }
}
