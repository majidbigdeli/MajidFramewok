using System.Collections.Generic;
using Microsoft.AspNet.Identity;

namespace Majid.IdentityFramework
{
    public class MajidIdentityResult : IdentityResult
    {
        public MajidIdentityResult()
        {
            
        }

        public MajidIdentityResult(IEnumerable<string> errors)
            : base(errors)
        {
            
        }

        public MajidIdentityResult(params string[] errors)
            :base(errors)
        {
            
        }

        public static MajidIdentityResult Failed(params string[] errors)
        {
            return new MajidIdentityResult(errors);
        }
    }
}