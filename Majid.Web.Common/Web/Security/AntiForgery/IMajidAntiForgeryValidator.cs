namespace Majid.Web.Security.AntiForgery
{
    /// <summary>
    /// This interface is internally used by MAJID framework and normally should not be used by applications.
    /// If it's needed, use 
    /// <see cref="IMajidAntiForgeryManager"/> and cast to 
    /// <see cref="IMajidAntiForgeryValidator"/> to use 
    /// <see cref="IsValid"/> method.
    /// </summary>
    public interface IMajidAntiForgeryValidator
    {
        bool IsValid(string cookieValue, string tokenValue);
    }
}