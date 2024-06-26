﻿using System.Security.Claims;
using System.Security.Principal;

namespace Agenda.Web.Helpers
{
    public static class UserExtended
    {
        public static string GetProfile(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.Role);
            return claim?.Value;
        }

        public static string GetId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetGroupId(this IPrincipal user)
        {
            var claim = ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.GroupSid);
            return claim?.Value;
        }
    }
}
