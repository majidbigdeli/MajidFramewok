using System.Threading.Tasks;

namespace Majid.Threading
{
    public static class MajidTaskCache
    {
        public static Task CompletedTask { get; } = Task.FromResult(0);
    }
}
