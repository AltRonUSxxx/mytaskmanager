using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Text.RegularExpressions;
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
                    return "NOT FOUND|";
                }

                if (passwordHasher.verifyPassword(password, user.hashed_password))
                {
                    return $"SUCCESS|{user.id}|{db.security_roles.FirstOrDefault(x => x.id == user.securityLvl_id).id}";
                }
                return "WRONG_PASSWORD|";
            }
        }

        private static string getLenTwo(string line)
        {
            if(line.Length == 1)
            {
                return $"0{line}";
            }
            return line;
        }

        public static async Task<string> register_lesson(string day, string month, string year, 
            string startHour, string startMinute, string endHour, string endMinute, 
            string theme, string group, int user_id)
        {
            try
            {
                using (var db = new teacher_studentEntities())
                {
                    DateTime startTime = DateTime.ParseExact($"{getLenTwo(day)}.{getLenTwo(month)}.{year} {getLenTwo(startHour)}:{getLenTwo(startMinute)}", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                    DateTime endTime = DateTime.ParseExact($"{getLenTwo(day)}.{getLenTwo(month)}.{year} {getLenTwo(endHour)}:{getLenTwo(endMinute)}", "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
                    lesson new_lesson = new lesson();
                    new_lesson.theme = theme;
                    new_lesson.user_id = user_id;
                    new_lesson.group_id = Convert.ToInt32(await getGroup_id(group));
                    new_lesson.start_time = startTime;
                    new_lesson.end_time = endTime;
                    new_lesson.status_id = 4;
                    db.lessons.Add(new_lesson);
                    db.SaveChanges();
                    return "SUCCESS";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return "UNEXPECTED ERROR";
            }

        }

        private static async Task<int> getGroup_id(string student_group)
        {
            using (var db = new teacher_studentEntities())
            {
                group checkGroup = db.groups.FirstOrDefault(x => x.name.ToLower() == student_group.ToLower());
                if(checkGroup == null)
                {
                    return 1;
                }
                else
                {
                    return checkGroup.id;
                }
            }
        }

        private static async Task<string> getGroup_name(int student_group_id)
        {
            using (var db = new teacher_studentEntities())
            {
                group checkGroup = db.groups.FirstOrDefault(x => x.id == student_group_id);
                if (checkGroup == null)
                {
                    return "-";
                }
                else
                {
                    return checkGroup.name;
                }
            }
        }

        public static async Task<string> registerAsync(string username, string password, string email, string firstname = "-", string lastname = "-", string middlename = "-", string student_group = "-")
        {
            using (var db = new teacher_studentEntities())
            {
                var userCheck = db.users.FirstOrDefault(x => x.username.Equals(username));
                if(userCheck != null)
                {
                    return "USERNAME_ALREADY_TAKEN";
                }
                var emailCheck = db.users.FirstOrDefault(x => x.email.Equals(email));
                if (emailCheck != null)
                {
                    return "EMAIL_ALREADY_TAKEN";
                }
                else
                {
                    try
                    {
                        user newUser = new user();
                        newUser.username = username;
                        newUser.hashed_password = passwordHasher.hashPassword(password);
                        newUser.securityLvl_id = 2;
                        newUser.email = email;
                        newUser.group_id = await getGroup_id(student_group);
                        db.users.Add(newUser);
                        fullname newFullname = new fullname();
                        newFullname.lastname = lastname;
                        newFullname.name = firstname;
                        newFullname.middlename = middlename;
                        newFullname.user_id = newUser.id;
                        db.fullnames.Add(newFullname);
                        attention newAttention = new attention();
                        newAttention.date = DateTime.Now;
                        newAttention.isOnline = false;
                        newAttention.user_id = newUser.id;
                        db.attentions.Add(newAttention);
                        await db.SaveChangesAsync();
                        return "SUCCESS";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        return "UNEXPECTED_ERROR";
                    }
                }
            }
        }

        private static string getFullName(int student_id)
        {
            using (var db = new teacher_studentEntities())
            {
                fullname student_fullname = db.fullnames.FirstOrDefault(x => x.user_id == student_id);
                string name = student_fullname.name == "" ? "-" : student_fullname.name;
                string lastname = student_fullname.lastname == "" ? "-" : student_fullname.lastname;
                string middlename = student_fullname.middlename == "" ? "-" : student_fullname.middlename;

                return $"{lastname} {name} {middlename}";
            }
        }

        private static string getStatus(int student_id)
        {
            using (var db = new teacher_studentEntities())
            {
                attention status = db.attentions.FirstOrDefault(x => x.user_id == student_id);
                if (status != null)
                {
                    return status.isOnline == true ? "ONLINE" : "OFFLINE";
                }
                return "OFFLINE";
            }
        }
        private static string getStatus_time(int student_id)
        {
            using (var db = new teacher_studentEntities())
            {
                attention status = db.attentions.FirstOrDefault(x => x.user_id == student_id);
                if(status == null)
                {
                    status = new attention();
                    status.user_id = student_id;
                    status.date = DateTime.Now;
                    status.isOnline = false;
                    db.attentions.Add(status);
                    db.SaveChanges();
                }
                DateTime time = status.date.Value;
                if(status.isOnline == true)
                {
                    return $"ONLINE [{time.Day}.{time.Month}.{time.Year}][{time.Hour}:{time.Minute}]";
                }
                else
                {
                    return $"OFFLINE [{time.Day}.{time.Month}.{time.Year}][{time.Hour}:{time.Minute}]";
                }
            }
        }

        public static async Task<string[]> getStudents()
        {
            using (var db = new teacher_studentEntities())
            {
                string[] answer = { };
                user[] allStudents = db.users.Where(x => x.securityLvl_id == 2).Select(x => x).ToArray();
                foreach(user student in allStudents)
                {
                    string temp = $"{getFullName(student.id)}|{student.username}|{getStatus(student.id)}|{ await getGroup_name(Convert.ToInt32(student.group_id))}";
                    answer = answer.Append(temp).ToArray();
                }
                return answer;
            }
        }

        private static int getGroupPopularity(int group_id)
        {
            using (var db = new teacher_studentEntities())
            {
                return db.users.Where(x => x.group_id == group_id).ToArray().Length;
            }
        }

        public static async Task<string> remove_group(string group_name)
        {
            using (var db = new teacher_studentEntities())
            {
                var this_group = db.groups.FirstOrDefault(x => x.name.ToLower() == group_name.ToLower());
                if(this_group != null)
                {
                    try
                    {
                        var students = db.users.Where(u => u.group_id == this_group.id).ToList();
                        foreach (user student in students)
                        {
                            student.group_id = 1;
                        }
                        lesson this_lesson = db.lessons.FirstOrDefault(x => x.group_id == this_group.id);
                        if(this_lesson != null)
                        {
                            db.lessons.Remove(this_lesson);
                        }
                        db.groups.Remove(this_group);
                        db.SaveChanges();
                        return "SUCCESS";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        return "UNEXPECTED_ERROR";
                    }
                }
                else
                {
                    return "NOT_FOUND";
                }
            }
        }

        public static async Task<string[]> getGroups()
        {
            using (var db = new teacher_studentEntities())
            {
                string[] answer = { };
                group[] allGroups = db.groups.Select(x => x).ToArray();
                foreach (group this_group in allGroups)
                {
                    string temp = $"{this_group.name}|{getGroupPopularity(Convert.ToInt32(this_group.id))}|";
                    answer = answer.Append(temp).ToArray();
                }
                return answer;
            }
        }

        public static async Task<string> add_group(string group_name)
        {
            using (var db = new teacher_studentEntities())
            {
                var groupCheck = db.groups.FirstOrDefault(x => x.name.ToLower() == group_name.ToLower());
                if(groupCheck != null)
                {
                    return "NAME_ALREADY_TAKEN";
                }
                else
                {
                    group new_group = new group();
                    new_group.name = group_name;
                    db.groups.Add(new_group);
                    db.SaveChanges();
                    return "SUCCESS";
                }
            }
        }

        public static async Task<string> get_groups_name()
        {
            using (var db = new teacher_studentEntities())
            {
                string[] groups = db.groups.Select(x => x.name).ToArray();
                return string.Join("|", groups);
            }
        }

        private static string get_lesson_status_name(int status_id)
        {
            using (var db = new teacher_studentEntities())
            {
                status this_status = db.status.FirstOrDefault(x => x.id == status_id);
                if(this_status == null)
                {
                    return "-";
                }
                else
                {
                    return this_status.name;
                }
            }
        }

        public static async Task<string> get_lessons()
        {
            using (var db = new teacher_studentEntities())
            {
                string[] lessons = { };
                foreach (lesson this_lesson in db.lessons)
                {
                    lessons = lessons.Append($"{this_lesson.id}/" +
                        $"{this_lesson.theme}/" +
                        $"{await getGroup_name(this_lesson.group_id)}/" +
                        $"{get_lesson_status_name(this_lesson.status_id)}/" +
                        $"{this_lesson.start_time.ToString("dd.MM.yyyy/HH:mm")}").ToArray();
                }
                return string.Join("|", lessons);
            }
        }

        public static async Task<string> remove_lesson(int lesson_id)
        {
            using (var db = new teacher_studentEntities())
            {
                lesson this_lesson = db.lessons.FirstOrDefault(x => x.id == lesson_id);
                if(this_lesson != null)
                {
                    try
                    {
                        db.lessons.Remove(this_lesson);
                        db.SaveChanges();
                        return "SUCCESS";
                    }
                    catch (Exception ex)
                    {
                        return "UNEXCEPTED_ERROR";
                    }
                }
                else
                {
                    return "NOT_FOUND";
                }
            }
        }

        public static async Task<string> remove(string student_username)
        {
            using (var db = new teacher_studentEntities())
            {
                var student = db.users.FirstOrDefault(x => x.username == student_username);
                if (student != null)
                {
                    try
                    {
                        attention student_attention = db.attentions.FirstOrDefault(x => x.user_id == student.id);
                        if(student_attention != null)
                        {
                            db.attentions.Remove(student_attention);
                        }

                        fullname student_fullname = db.fullnames.FirstOrDefault(x => x.user_id == student.id);
                        if(student_fullname != null)
                        {
                            db.fullnames.Remove(db.fullnames.FirstOrDefault(x => x.user_id == student.id));
                        }
                        db.users.Remove(student);
                        db.SaveChanges();
                        return "SUCCESS";
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.ToString());
                        return "UNEXPECTED_ERROR";
                    }
                }
                else
                {
                    return "NOT_FOUND";
                }
            }
        }

        public static async Task makeStatus(int id, bool online)
        {
            using (var db = new teacher_studentEntities())
            {
                var attentionet = db.attentions.FirstOrDefault(x => x.user_id == id);
                if(attentionet == null)
                {
                    attention attent_new = new attention();
                    attent_new.date = DateTime.Now;
                    attent_new.isOnline = online;
                    attent_new.user_id = id;
                    db.attentions.Add(attent_new);
                }
                else
                {
                    attentionet.date = DateTime.Now;
                    attentionet.isOnline = online;
                }
                db.SaveChanges();
            }
        }


    }
}   