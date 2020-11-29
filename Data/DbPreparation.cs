using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PunsApi.Models;

namespace PunsApi.Data
{
    public static class DbPreparation
    {
        public static void Migrate(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope();
            var context = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                context.Database.Migrate();
                Console.WriteLine("Database migrated");
                SeedData(context);
                Console.WriteLine("Database seeded");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public static void SeedData(AppDbContext context)
        {

            //Populating Database
            if (!context.PasswordCategories.Any())
            {
                System.Console.WriteLine("Adding Users...");
                var item = new List<PasswordCategorie>
                {
                    new PasswordCategorie
                    {
                        CategoryName = "Film Polski",
                        Passwords = new List<Password>
                        {
                            new Password
                            {
                                PasswordContent = "Sami Swoi"
                            },
                            new Password
                            {
                                PasswordContent = "Boże Ciało"
                            },
                            new Password
                            {
                                PasswordContent = "365 Dni"
                            },
                            new Password
                            {
                                PasswordContent = "Poranek Kojota"
                            },
                            new Password
                            {
                                PasswordContent = "Chłopaki Nie Płaczą"
                            }
                        }
                    },
                    new PasswordCategorie
                    {
                        CategoryName = "Film Zagraniczny",
                        Passwords = new List<Password>
                        {
                            new Password
                            {
                                PasswordContent = "Zniewolony"
                            },
                            new Password
                            {
                                PasswordContent = "Matrix"
                            },
                            new Password
                            {
                                PasswordContent = "Uwolnić Orkę"
                            },
                            new Password
                            {
                                PasswordContent = "Terminator"
                            },
                            new Password
                            {
                                PasswordContent = "Rocky"
                            }
                        }
                    },
                    new PasswordCategorie
                    {
                        CategoryName = "Przysłowia",
                        Passwords = new List<Password>
                        {
                            new Password
                            {
                                PasswordContent = "Bez pracy nie ma kołaczy"
                            },
                            new Password
                            {
                                PasswordContent = "Darowanemu koniowi w zęby się nie zagląda"
                            },
                            new Password
                            {
                                PasswordContent = "Fortuna kołem się toczy"
                            },
                            new Password
                            {
                                PasswordContent = "Lepszy wróbel w garści niż gołąb na dachu"
                            },
                            new Password
                            {
                                PasswordContent = "Co ma wisieć, nie utonie"
                            }
                        }
                    },

                };
                context.AddRange(item);
                context.SaveChanges();
            }
        }
    }
}
