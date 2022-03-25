

using Core.Domain;

namespace Core.Application.Commands
{
    /// <summary>
    /// نتیجه انجام هر عملیات به کمک این کلاس بازگشت داده می‌شود.
    /// دلایل استفاده از این الگو و پیاده سازی کاملی از این الگو را در لینک زیر می‌توانید مشاهده کنید
    /// https://github.com/vkhorikov/CqrsInPractice
    /// </summary>
    public class CommandResult : ApiResult
    {
    }

    public class CommandResult<TData> : CommandResult
    {
        public TData Data;
    }
}
