
//using Data.Identity.Models;
//using Data.Identity.Models.Security;
//using Data.Identity.Models.Users;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Data.Identity.DbContext
//{
//    internal static class IdentityWebContextInitializer
//    {
//        public static void Initialize(IdentityWebContext ctx, string connectionString)
//        {
//            if (ctx.Users.Any())
//                return;

//            CreateRoles(ctx);

//            CreateAdministrator(ctx, connectionString);

//            ctx.SaveChanges();
//        }

//        static void CreateRoles(IdentityWebContext ctx)
//        {
//            var roles = ApplicationRoles.Items.Select(e => new IdentityRole
//            {
//                Id = e.Id,
//                Name = e.Name,
//                NormalizedName = e.Name.ToUpper()
//            });

//            ctx.AddRange(roles);
//        }

//        static void CreateAdministrator(IdentityWebContext ctx, string connectionString)
//        {
//            var tenant = new Tenant
//            {
//                TenantId = "administrator",
//                Name = "Administrator",
//                Host = "Administrators Host",
//                DatabaseConnectionString = connectionString,// @"Data Source=App_Data\TenantDB-DV.db;",
//                PhoneNumber = "+639198262335",
//                Email = "caydev2010@gmail.com",
//                Address = "",
//                SellerId = "PH7S3PV6UR",
//            };

//            var token1 = Guid.NewGuid().ToString();

//            var email1 = "system1@gmail.com";
//            var user1 = new IdentityWebUser
//            {
//                Id = email1,
//                UserName = email1,
//                NormalizedUserName = email1.ToUpper(),

//                Email = email1,
//                NormalizedEmail = email1.ToUpper(),
//                EmailConfirmed = true,
//                PhoneNumber = "+639198262335",
//                PhoneNumberConfirmed = true,

//                LockoutEnabled = false,
//                LockoutEnd = null,
//                PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
//                SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
//                TwoFactorEnabled = false,
//                AccessFailedCount = 0,
//                TenantId = tenant.TenantId,
//                ConcurrencyStamp = token1,
//                UserInformation = new UserInformation
//                {
//                    FirstName = "System1",
//                    LastName = "System1",
//                    ConcurrencyToken = token1,
//                    Theme = "https://bootswatch.com/4/darkly/bootstrap.min.css"
//                }
//            };

//            var userRole1 = new IdentityUserRole<string>
//            {
//                UserId = user1.Id,
//                RoleId = ApplicationRoles.System.Id
//            };

//            var email2 = "admin1@gmail.com";
//            var user2 = new IdentityWebUser
//            {
//                Id = email2,
//                UserName = email2,
//                NormalizedUserName = email2.ToUpper(),

//                Email = email2,
//                NormalizedEmail = email2.ToUpper(),
//                EmailConfirmed = true,
//                PhoneNumber = "+639198262335",
//                PhoneNumberConfirmed = true,

//                LockoutEnabled = false,
//                LockoutEnd = null,
//                PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
//                SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
//                TwoFactorEnabled = false,
//                AccessFailedCount = 0,
//                TenantId = tenant.TenantId,
//                ConcurrencyStamp = token1,
//                UserInformation = new UserInformation
//                {
//                    FirstName = "Admin1",
//                    LastName = "Admin1",
//                    ConcurrencyToken = token1,
//                    Theme = "https://bootswatch.com/4/spacelab/bootstrap.min.css"
//                }
//            };
//            var userRole2 = new IdentityUserRole<string>
//            {
//                UserId = user2.Id,
//                RoleId = ApplicationRoles.Administrator.Id
//            };

//            var email3 = "admin2@gmail.com";
//            var user3 = new IdentityWebUser
//            {
//                Id = email3,
//                UserName = email3,
//                NormalizedUserName = email3.ToUpper(),

//                Email = email3,
//                NormalizedEmail = email3.ToUpper(),
//                EmailConfirmed = true,
//                PhoneNumber = "+639198262335",
//                PhoneNumberConfirmed = true,

//                LockoutEnabled = false,
//                LockoutEnd = null,
//                PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
//                SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
//                TwoFactorEnabled = false,
//                AccessFailedCount = 0,
//                TenantId = tenant.TenantId,
//                ConcurrencyStamp = token1,
//                UserInformation = new UserInformation
//                {
//                    FirstName = "admin2",
//                    LastName = "admin2",
//                    ConcurrencyToken = token1,
//                    Theme = "https://bootswatch.com/4/spacelab/bootstrap.min.css"
//                }
//            };
//            var userRole3 = new IdentityUserRole<string>
//            {
//                UserId = user3.Id,
//                RoleId = ApplicationRoles.Administrator.Id
//            };

//            ctx.AddRange(tenant, user1, userRole1, user2, userRole2, user3, userRole3);

//            //  sample doctor
//            var doctor1Email = "doctor1@gmail.com";
//            var doctor1 = new IdentityWebUser
//            {
//                Id = doctor1Email,
//                UserName = doctor1Email,
//                NormalizedUserName = doctor1Email.ToUpper(),

//                Email = doctor1Email,
//                NormalizedEmail = doctor1Email.ToUpper(),
//                EmailConfirmed = true,
//                PhoneNumber = "+639198262335",
//                PhoneNumberConfirmed = true,

//                LockoutEnabled = false,
//                LockoutEnd = null,
//                PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
//                SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
//                TwoFactorEnabled = false,
//                AccessFailedCount = 0,
//                TenantId = tenant.TenantId,
//                ConcurrencyStamp = token1,
//                UserInformation = new UserInformation
//                {
//                    FirstName = "Chino",
//                    LastName = "Pacia",
//                    ConcurrencyToken = token1,
//                    Theme = "https://bootswatch.com/4/spacelab/bootstrap.min.css"
//                }
//            };
//            var doctor1Role1 = new IdentityUserRole<string>
//            {
//                UserId = doctor1.Id,
//                RoleId = ApplicationRoles.Doctor.Id
//            };
//            ctx.AddRange(doctor1, doctor1Role1);

//            //  sample nurse1
//            var nurse1Email = "nurse1@gmail.com";
//            var nurse1 = new IdentityWebUser
//            {
//                Id = nurse1Email,
//                UserName = nurse1Email,
//                NormalizedUserName = nurse1Email.ToUpper(),

//                Email = nurse1Email,
//                NormalizedEmail = nurse1Email.ToUpper(),
//                EmailConfirmed = true,
//                PhoneNumber = "+639198262335",
//                PhoneNumberConfirmed = true,

//                LockoutEnabled = false,
//                LockoutEnd = null,
//                PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
//                SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
//                TwoFactorEnabled = false,
//                AccessFailedCount = 0,
//                TenantId = tenant.TenantId,
//                ConcurrencyStamp = token1,
//                UserInformation = new UserInformation
//                {
//                    FirstName = "Pening",
//                    LastName = "Garcia",
//                    ConcurrencyToken = token1,
//                    Theme = "https://bootswatch.com/4/spacelab/bootstrap.min.css"
//                }
//            };
//            var nurse1Role1 = new IdentityUserRole<string>
//            {
//                UserId = nurse1.Id,
//                RoleId = ApplicationRoles.Nurse.Id
//            };
//            ctx.AddRange(nurse1, nurse1Role1);

//            //  sample nurse2
//            var nurse2Email = "nurse2@gmail.com";
//            var nurse2 = new IdentityWebUser
//            {
//                Id = nurse2Email,
//                UserName = nurse2Email,
//                NormalizedUserName = nurse2Email.ToUpper(),

//                Email = nurse2Email,
//                NormalizedEmail = nurse2Email.ToUpper(),
//                EmailConfirmed = true,
//                PhoneNumber = "+639198262335",
//                PhoneNumberConfirmed = true,

//                LockoutEnabled = false,
//                LockoutEnd = null,
//                PasswordHash = "AQAAAAEAACcQAAAAEKGIieH17t5bYXa5tUfxRwN9UIEwApTKbQBRaUtIHplIUG2OfYxvBS8uvKy5E2Stsg==",
//                SecurityStamp = "6SADCY3NMMLOHA2S26ZJCEWGHWSQUYRM",
//                TwoFactorEnabled = false,
//                AccessFailedCount = 0,
//                TenantId = tenant.TenantId,
//                ConcurrencyStamp = token1,
//                UserInformation = new UserInformation
//                {
//                    FirstName = "Tina",
//                    LastName = "Moran",
//                    ConcurrencyToken = token1,
//                    Theme = "https://bootswatch.com/4/spacelab/bootstrap.min.css"
//                }
//            };
//            var nurse2Role1 = new IdentityUserRole<string>
//            {
//                UserId = nurse2.Id,
//                RoleId = ApplicationRoles.Nurse.Id
//            };
//            ctx.AddRange(nurse2, nurse2Role1);
//        }
//    }
//}
