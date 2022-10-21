using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace WebApplication1.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDBContext(serviceProvider.GetRequiredService<DbContextOptions<AppDBContext>>()))
            {
                if (context.Personal.Any())
                {
                    return;
                }


                context.Personal.AddRange(
                    new PersonalInformations
                    {
                        Login = "ivan@user.com",
                        Password = "qwerty123",
                        FirstName = "Ivan",
                        LastName = "Ivanov",
                        Gender = "Male",
                        YearOfBirth = DateTime.Now
                    },
                     new PersonalInformations
                     {
                         Login = "nastya@user.com",
                         Password = "qwer123",
                         FirstName = "Nastya",
                         LastName = "Popova", 
                         Gender = "Female",
                         YearOfBirth = DateTime.Now
                     },
                      new PersonalInformations
                      {
                          Login = "abse@user.com",
                          Password = "qwerty123",
                          FirstName = "Kayrat",
                          LastName = "Erjanov",
                          Gender = "Male",
                          YearOfBirth = DateTime.Now
                      },
                       new PersonalInformations
                       {
                           Login = "user4@user.com",
                           Password = "qwerty123",
                           FirstName = "Mario",
                           LastName = "Mario",
                           Gender = "Female",
                           YearOfBirth = DateTime.Now
                       },
                        new PersonalInformations
                        {
                            Login = "user5@user.com",
                            Password = "qwerty123",
                            FirstName = "Luigy",
                            LastName = "Mario",
                            Gender = "Male",
                            YearOfBirth = DateTime.Now
                        }
                    );

                context.SaveChanges();
            }

        }

    }
}
