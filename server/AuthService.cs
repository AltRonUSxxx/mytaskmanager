using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class AuthService
    {
        public static async Task<string> loginAsync(string username, string password)
        {
            using (var db = new teacher_studentEntities())
            {
                var user = db.users.FirstOrDefault(x => x.username == username);

                if (user is null)
                {
                    return "NOT FOUND";
                }

                if (passwordHasher.verifyPassword(password, user.hashed_password))
                {
                    return "SUCCESS";
                }
                return "WRONG_PASSWORD";
            }
        }
    }
}