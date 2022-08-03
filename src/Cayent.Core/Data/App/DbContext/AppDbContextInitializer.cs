//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Data.App.Models.Patients;
//using Data.App.Models.Users;
//using Data.Constants;
//using Data.Providers;

//namespace Data.App.DbContext
//{
//    public static class AppDbContextInitializer
//    {
//        static Random _rnd = new Random((int)DateTime.UtcNow.Ticks);

//        public static void Initialize(AppDbContext ctx, IEnumerable<ProvisionUserRole> provisionUserRoles)
//        {
//            var parent1 = new Parent
//            {
//                User = new User
//                {
//                    UserId = Guid.NewGuid().ToString(),
//                    Email = "parent1@gmail.com",
//                    PhoneNumber = "12345",
//                    FirstName = "Parent1",
//                    MiddleName = "Parent1",
//                    LastName = "Parent1",
//                    UserRoles = new List<UserRole>(new[]
//                    {
//                        new UserRole {
//                            RoleId = ApplicationRoles.Parent.Id
//                        }
//                    })
//                },
//                Patients = new List<Patient>(new[]
//                {
//                    new Patient
//                    {
//                        User = new User
//                        {
//                            UserId = Guid.NewGuid().ToString(),
//                            Email = "",
//                            PhoneNumber = "",
//                            FirstName = "Patient1",
//                            MiddleName = "Patient1",
//                            LastName = "Patient1",
//                        }
//                    },
//                    new Patient
//                    {
//                        User = new User
//                        {
//                            UserId = Guid.NewGuid().ToString(),
//                            Email = "",
//                            PhoneNumber = "",
//                            FirstName = "Patient2",
//                            MiddleName = "Patient2",
//                            LastName = "Patient2",
//                        }
//                    }
//                })
//            };

//            var parent2 = new Parent
//            {
//                User = new User
//                {
//                    UserId = Guid.NewGuid().ToString(),
//                    Email = "parent2@gmail.com",
//                    PhoneNumber = "12345",
//                    FirstName = "Parent2",
//                    MiddleName = "Parent2",
//                    LastName = "Parent2",
//                    UserRoles = new List<UserRole>(new[]
//                    {
//                        new UserRole {
//                            RoleId = ApplicationRoles.Parent.Id
//                        }
//                    })
//                },
//                Patients = new List<Patient>(new[]
//                {
//                    new Patient
//                    {
//                        User = new User
//                        {
//                            UserId = Guid.NewGuid().ToString(),
//                            Email = "",
//                            PhoneNumber = "",
//                            FirstName = "Patient1",
//                            MiddleName = "Patient1",
//                            LastName = "Patient1",
//                        }
//                    },
//                    new Patient
//                    {
//                        User = new User
//                        {
//                            UserId = Guid.NewGuid().ToString(),
//                            Email = "",
//                            PhoneNumber = "",
//                            FirstName = "Patient2",
//                            MiddleName = "Patient2",
//                            LastName = "Patient2",
//                        }
//                    }
//                })
//            };

//            ctx.AddRange(parent1);
//            ctx.AddRange(parent2);

//            for (var i = 10; i < 99; i++)
//            {
//                var parent = new Parent
//                {
//                    User = new User
//                    {
//                        UserId = Guid.NewGuid().ToString(),
//                        Email = $"parent_0{i}@gmail.com",
//                        PhoneNumber = "12345",
//                        FirstName = $"Parent_0{i}",
//                        MiddleName = $"Parent_0{i}",
//                        LastName = $"Parent_0{i}",
//                        UserRoles = new List<UserRole>(new[]
//                    {
//                        new UserRole {
//                            RoleId = ApplicationRoles.Parent.Id
//                        }
//                    })
//                    },
//                };

//                ctx.AddRange(parent);

//            }

//            ctx.SaveChanges();
//        }

//        static List<Tuple<string, string, string, string>> GetNames()
//        {
//            var list = new List<Tuple<string, string, string, string>>();

//            //list.Add(new Tuple<string, string, string, string>("Juan", "Dela Cruz", "09191234567", "105 Paz Street, Barangay 11, Balayan, Batangas City"));
//            //list.Add(new Tuple<string, string, string, string>("Pening", "Garcia", "09191234567", "101 Subdivision 202 Street, Barangay, Town, City, Philippines"));
//            //list.Add(new Tuple<string, string, string, string>("Nadia", "Cole", "09191234567", "301 Main Street, Barangay 3, Balayan, Batangas City"));
//            //list.Add(new Tuple<string, string, string, string>("Chino", "Pacia", "09191234567", "202 Subdivision 303 Street, Barangay, Town, City, Philippines"));
//            //list.Add(new Tuple<string, string, string, string>("Vina", "Ruruth", "09191234567", "501 Main Street, Barangay 5, Balayan, Batangas City"));
//            //list.Add(new Tuple<string, string, string, string>("Lina", "Mutac", "09191234567", "601 Main Street, Barangay 6, Balayan, Batangas City"));

//            list.Add(new Tuple<string, string, string, string>("Penny", "Garcia", "09191234567", "101 Subdivision 202 Street, Barangay, Town, City, Philippines"));
//            list.Add(new Tuple<string, string, string, string>("Gino", "Pacia", "09191234567", "202 Subdivision 303 Street, Barangay, Town, City, Philippines"));

//            return list;
//        }

//        static string NewId()
//        {
//            return Guid.NewGuid().ToString().ToLower();
//        }

//        static string NewCouponCode()
//        {
//            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//            var random = _rnd;
//            var result = new string(
//                Enumerable.Repeat(chars, 8)
//                          .Select(s => s[random.Next(s.Length)])
//                          .ToArray());

//            return result;
//        }
//    }
//}
