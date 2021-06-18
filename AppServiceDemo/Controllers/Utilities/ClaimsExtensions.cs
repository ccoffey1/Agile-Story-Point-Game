using System.Security.Claims;
using System.Security.Principal;

namespace AppServiceDemo.Controllers.Base
{
    public static class ClaimsExtensions
    {
        public static int GetPlayerId(this IIdentity identity)
        {
            return int.Parse(((ClaimsIdentity)identity).FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
