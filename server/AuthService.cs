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
                    return $"SUCCESS|{user.id}|{db.security_roles.FirstOrDefault(x => x.id == user.securityLvl_id).id}";
                }
                return "WRONG_PASSWORD";
            }
        }

        public static async Task makeStatus(int id, bool online)
        {
            using (var db = new teacher_studentEntities())
            {
                var attentionet  = db.attentions.FirstOrDefault(x => x.user_id == id);
                if(!(attentionet is null))
                {
                    if (online)
                    {
                        attentionet.date = DateTime.Now;
                    }
                    else
                    {
                        attentionet.date = null;
                    }
                }
                else
                {
                    attention attent_new = new attention();
                    if(online)
                    {
                        attent_new.date = DateTime.Now;
                    }
                    else
                    {
                        attent_new.date = null;
                    }
                    attent_new.user_id = id;
                    db.attentions.Add(attent_new);
                }
                db.SaveChanges();
            }
        }
    }
}