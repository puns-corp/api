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
                Console.WriteLine("Failed to seed database.");
                Console.WriteLine(ex.Message);
            }
        }
        public static void SeedData(AppDbContext context)
        {
            if (context.PasswordCategories.Any())
                return;

            Console.WriteLine("Adding password categories...");
            var categories = new List<PasswordCategory>
            {
                new PasswordCategory
                {
                    CategoryName = "Film Polski",
                    Passwords = new List<Password>
                    {
                        new Password
                        {
                            Content = "Sami Swoi"
                        },
                        new Password
                        {
                            Content = "Boże Ciało"
                        },
                        new Password
                        {
                            Content = "365 Dni"
                        },
                        new Password
                        {
                            Content = "Poranek Kojota"
                        },
                        new Password
                        {
                            Content = "Chłopaki Nie Płaczą"
                        }
                    }
                },
                new PasswordCategory
                {
                    CategoryName = "Film Zagraniczny",
                    Passwords = new List<Password>
                    {
                        new Password
                        {
                            Content = "Zniewolony"
                        },
                        new Password
                        {
                            Content = "Matrix"
                        },
                        new Password
                        {
                            Content = "Uwolnić Orkę"
                        },
                        new Password
                        {
                            Content = "Terminator"
                        },
                        new Password
                        {
                            Content = "Rocky"
                        }
                    }
                },
                new PasswordCategory
                {
                    CategoryName = "Przysłowia",
                    Passwords = new List<Password>
                    {
                        new Password
                        {
                            Content = "Bez pracy nie ma kołaczy"
                        },
                        new Password
                        {
                            Content = "Darowanemu koniowi w zęby się nie zagląda"
                        },
                        new Password
                        {
                            Content = "Fortuna kołem się toczy"
                        },
                        new Password
                        {
                            Content = "Lepszy wróbel w garści niż gołąb na dachu"
                        },
                        new Password
                        {
                            Content = "Co ma wisieć, nie utonie"
                        }
                    }
                },

            };
            context.AddRange(categories);
            context.SaveChanges();
        }
    }
}
