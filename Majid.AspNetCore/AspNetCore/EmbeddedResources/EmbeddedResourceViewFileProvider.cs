using Majid.Dependency;
using Majid.Resources.Embedded;

namespace Majid.AspNetCore.EmbeddedResources
{
    public class EmbeddedResourceViewFileProvider : EmbeddedResourceFileProvider
    {
        public EmbeddedResourceViewFileProvider(IIocResolver iocResolver) 
            : base(iocResolver)
        {
        }

        protected override bool IsIgnoredFile(EmbeddedResourceItem resource)
        {
            return resource.FileExtension != "cshtml";
        }
    }
}