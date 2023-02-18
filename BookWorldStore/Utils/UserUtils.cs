using BookWorldStore.Models;
using System.Security.Claims;
using System.Security.Principal;

namespace BookWorldStore.Utils
{
    public class UserUtils
    {
        private static UserUtils _instance;
        public static UserUtils Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserUtils();
                }
                return _instance;
            }
        }
        private UserUtils() { }

        public User GetUser(HttpContext context)
        {
            var identity = context.User.Identity as ClaimsIdentity;
            IList<Claim> claim = identity.Claims.ToList();
            if(claim.Count > 0)
            {
                User user = new User();
                user.email = claim[0].Value;
                user.role = claim[1].Value;
                return user;
            }
            return null;
        }
    }
}
