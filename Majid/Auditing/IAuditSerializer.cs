namespace Majid.Auditing
{
    public interface IAuditSerializer
    {
        string Serialize(object obj);
    }
}