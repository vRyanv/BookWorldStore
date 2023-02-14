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
            User user = new User();
            user.user_id = int.Parse(claim[0].Value);
            user.email = claim[1].Value;
            user.role = claim[2].Value;
            return user;
        }
    }
}
