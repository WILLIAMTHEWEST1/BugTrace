using BugTrace.Models;
using BugTrace.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugTrace.Data
{
    public static class DataUtility
    {
        private static int company1Id;
        private static int company2Id;
        private static int company3Id;
        private static int company4Id;
        private static int company5Id;

        public static string GetConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");

            return string.IsNullOrEmpty(databaseUrl) ? connectionString : BuildConnectionString(databaseUrl);
        }

        private static string BuildConnectionString(string databaseUrl)
        {
            var databaseUri = new Uri(databaseUrl);

            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Prefer,
                TrustServerCertificate = true
            };

            return builder.ToString();
        }

        public static async Task ManageDataAsync(IHost host)
        {
            using var svcScope = host.Services.CreateScope();

            var svcProvider = svcScope.ServiceProvider;

            var dbContextSvc = svcProvider.GetRequiredService<ApplicationDbContext>();

            var roleManagerSvc = svcProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var userManagerSvc = svcProvider.GetRequiredService<UserManager<BTUser>>();

            await dbContextSvc.Database.MigrateAsync();



            //Roles
            await SeedRolesAsync(roleManagerSvc);

            //Companies
            await SeedCompaniesAsync(dbContextSvc);

            //Users
            await SeedUsersAsync(userManagerSvc);

            //Demo Users
            //await SeedDemoUsersAsync(userManagerSvc);

            //project Priority
            //await SeedProjectPrioritiesAsync(dbContextSvc);

            //Ticket Statuses
            //await SeedTicketStatusAsync(dbContextSvc);

            //Ticket Priorities
            //await SeedTicketPrioritiesAsync(dbContextSvc);

            //Ticket Types
            await SeedTicketTypesAsync(dbContextSvc);

            //Notification Types
            //await SeedNotificationTypesAsync(dbContextSvc);

            //Projects
            //await SeedProjectsAsync(dbContextSvc);

            //Tickets
            //await SeedTicketsAsync(dbContextSvc);
        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManagerSvc)
        {
            await roleManagerSvc.CreateAsync(new IdentityRole(nameof(BTRoles.Admin)));
            await roleManagerSvc.CreateAsync(new IdentityRole(nameof(BTRoles.ProjectManager)));
            await roleManagerSvc.CreateAsync(new IdentityRole(nameof(BTRoles.Developer)));
            await roleManagerSvc.CreateAsync(new IdentityRole(nameof(BTRoles.Submitter)));
            await roleManagerSvc.CreateAsync(new IdentityRole(nameof(BTRoles.DemoUser)));
        }

        private static async Task SeedCompaniesAsync(ApplicationDbContext dbContextSvc)
        {
            try
            {
                IList<Company> defaultCompanies = new List<Company>()
                {
                    new Company() { Name = "Company 1", Description = "This is default Company 1" },
                    new Company() { Name = "Company 2", Description = "This is default Company 2" },
                    new Company() { Name = "Company 3", Description = "This is default Company 3" },
                    new Company() { Name = "Company 4", Description = "This is default Company 4" },
                    new Company() { Name = "Company 5", Description = "This is default Company 5" }
                };

                var dbCompanies = dbContextSvc.Companies.Select(c => c.Name).ToList();

                await dbContextSvc.Companies.AddRangeAsync(defaultCompanies.Where(c => !dbCompanies.Contains(c.Name)));

                await dbContextSvc.SaveChangesAsync();

                company1Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 1").Id;
                company2Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 2").Id;
                company3Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 3").Id;
                company4Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 4").Id;
                company5Id = dbContextSvc.Companies.FirstOrDefault(c => c.Name == "Company 5").Id;

            }

            catch (Exception ex)
            {
                Console.WriteLine("******* ERROR *******");
                Console.WriteLine("ERROR Seeding Default Admin User");
                Console.WriteLine(ex.Message);
                Console.WriteLine("*******");
            }
        }

        private static async Task SeedUsersAsync(UserManager<BTUser> userManagerSvc)
        {

            var defaultUser = new BTUser
            {
                UserName = "btadmin1@bugtracer.com",
                Email = "btadmin1@bugtracer.com",
                FirstName = "Bill",
                LastName = "AppUser",
                EmailConfirmed = true,
                CompanyId = company1Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    //When seeding users you MUST meet password complexity reqs
                    //6 char min, 1 upper, 1 lower, 1 num, 1 special Char
                    //If you dont, the user won't be created while NO ERROR GETS THROWN!!
                    await userManagerSvc.CreateAsync(defaultUser, "Abc!123");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Admin.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******* ERROR *******");
                Console.WriteLine("ERROR Seeding Default Admin User");
                Console.WriteLine(ex.Message);
                Console.WriteLine("*******");
            }
            defaultUser = new BTUser
            {
                UserName = "btadmin2@bugtracer.com",
                Email = "btadmin2@bugtracer.com",
                FirstName = "Jill",
                LastName = "AppUser",
                EmailConfirmed = true,
                CompanyId = company2Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Admin.ToString());
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.DemoUser.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Admin User 2");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }

            defaultUser = new BTUser
            {
                UserName = "btdev1@bugtracer.com",
                Email = "btdev1@bugtracer.com",
                FirstName = "James",
                LastName = "DevUser",
                EmailConfirmed = true,
                CompanyId = company1Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Developer.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Developer User 1");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }
            defaultUser = new BTUser
            {
                UserName = "btdev2@bugtracer.com",
                Email = "btdev2@bugtracer.com",
                FirstName = "Jessica",
                LastName = "DevUser",
                EmailConfirmed = true,
                CompanyId = company1Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Developer.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Developer User 2");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }

            defaultUser = new BTUser
            {
                UserName = "btdev3@bugtracer.com",
                Email = "btdev3@bugtracer.com",
                FirstName = "Terry",
                LastName = "DevUser",
                EmailConfirmed = true,
                CompanyId = company1Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Developer.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Developer User 3");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }

            defaultUser = new BTUser
            {
                UserName = "btdeveloper1@bugtracer.com",
                Email = "btdeveloper1@bugtracer.com",
                FirstName = "Shelly",
                LastName = "DevUser",
                EmailConfirmed = true,
                CompanyId = company2Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Developer.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Developer User 1");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }

            defaultUser = new BTUser
            {
                UserName = "btdevloper2@bugtracer.com",
                Email = "btdeveloper2@bugtracer.com",
                FirstName = "Thomas",
                LastName = "DevUser",
                EmailConfirmed = true,
                CompanyId = company2Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Developer.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Developer User 2");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }

            defaultUser = new BTUser
            {
                UserName = "btdeveloper3@bugtracer.com",
                Email = "btdeveloper3@bugtracer.com",
                FirstName = "Glenn",
                LastName = "DevUser",
                EmailConfirmed = true,
                CompanyId = company2Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Developer.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Developer User 3");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }
            defaultUser = new BTUser
            {
                UserName = "btsub1@bugtracer.com",
                Email = "btsub1@bugtracer.com",
                FirstName = "Carl",
                LastName = "SubmitterUser",
                EmailConfirmed = true,
                CompanyId = company1Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Submitter.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Submitter User 1");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }
            defaultUser = new BTUser
            {
                UserName = "btsub2@bugtracer.com",
                Email = "btsub2@bugtracer.com",
                FirstName = "Maggie",
                LastName = "SubmitterUser",
                EmailConfirmed = true,
                CompanyId = company1Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Submitter.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Submitter User 2");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }

            defaultUser = new BTUser
            {
                UserName = "btsub3@bugtracer.com",
                Email = "btsub3@bugtracer.com",
                FirstName = "Rick",
                LastName = "SubmitterUser",
                EmailConfirmed = true,
                CompanyId = company1Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Submitter.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Submitter User 3");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }
            defaultUser = new BTUser
            {
                UserName = "btsubbmitter1@bugtracer.com",
                Email = "btsubmitter1@bugtracer.com",
                FirstName = "Negan",
                LastName = "SubmitterUser",
                EmailConfirmed = true,
                CompanyId = company2Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Submitter.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Submitter User 1");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }
            defaultUser = new BTUser
            {
                UserName = "btsubmitter2@bugtracer.com",
                Email = "btsubbmitter2@bugtracer.com",
                FirstName = "Simon",
                LastName = "SubmitterUser",
                EmailConfirmed = true,
                CompanyId = company2Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Submitter.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Submitter User 2");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }
            defaultUser = new BTUser
            {
                UserName = "btsubbmitter3@bugtracer.com",
                Email = "btsubmitter3@bugtracer.com",
                FirstName = "Shane",
                LastName = "SubmitterUser",
                EmailConfirmed = true,
                CompanyId = company2Id
            };
            try
            {
                var user = await userManagerSvc.FindByEmailAsync(defaultUser.Email);
                if (user is null)
                {
                    await userManagerSvc.CreateAsync(defaultUser, "Abc&123!");
                    await userManagerSvc.AddToRoleAsync(defaultUser, BTRoles.Submitter.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******** ERROR ********");
                Console.WriteLine("Error Seeding Default Submitter User 3");
                Console.WriteLine(ex.Message);
                Console.WriteLine("***********************");
            }
        }

        private static async Task SeedDemoUsersAsync(UserManager<BTUser> userManagerSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedProjectPrioritiesAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedTicketStatusAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedTicketPrioritiesAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedTicketTypesAsync(ApplicationDbContext dbContextSvc)
        {
            try
            {           
            IList<TicketType> ticketTypes = new List<TicketType>()
            {
                new TicketType() { Name = BTTicketType.ChangeRequest.ToString() },
                new TicketType() { Name = BTTicketType.Defect.ToString() },
                new TicketType() { Name = BTTicketType.Enhancement.ToString() },
                new TicketType() { Name = BTTicketType.GeneralTask.ToString() },
                new TicketType() { Name = BTTicketType.NewDevelopment.ToString() },
                new TicketType() { Name = BTTicketType.WorkTask.ToString() },
             };
                var dbTicketTypes = dbContextSvc.TicketTypes.Select(c => c.Name).ToList();

                await dbContextSvc.TicketTypes.AddRangeAsync(ticketTypes.Where(c => !dbTicketTypes.Contains(c.Name)));

                await dbContextSvc.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("******* ERROR *******");
                Console.WriteLine("ERROR Seeding Default Ticket Types");
                Console.WriteLine(ex.Message);
                Console.WriteLine("*******");
            }

        }

        private static async Task SeedNotificationTypesAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedProjectsAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

        private static async Task SeedTicketsAsync(ApplicationDbContext dbContextSvc)
        {
            throw new NotImplementedException();
        }

    }
}
