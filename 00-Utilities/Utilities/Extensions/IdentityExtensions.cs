using Microsoft.AspNetCore.Identity;
using Utilities.Extensions;
using Utilities;

namespace Utilities.Extensions
{
    public static class IdentityExtensions
    {

        public static string JoinErrors(this IEnumerable<string> strs) => String.Join(",", strs);

        public static string JoinIdentityErrors(this IdentityResult identityResult) =>
            identityResult.Errors.Select(x => x.Description).JoinErrors();

    }
}
