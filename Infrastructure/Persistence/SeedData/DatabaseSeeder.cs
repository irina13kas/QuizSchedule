using Application.Common.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.SeedData
{
    public class DatabaseSeeder
    {
        public static async Task SeedAsync(
            IRepository<User> userRepository,
            IAdminRepository adminRepository,
            IPasswordHasher hasher,
            IQuizmanRepository quizmanRepository,
            IUnitOfWork unitOfWork)
        {
            var passwordHash_1 = hasher.Hash("12345");
            var adminUser_1 = new User(
                "mainAdmin",
                passwordHash_1,
                "Юлия Зеленская",
                UserRole.Admin,
                null,
                new DateTime(2004, 07, 21),
                "@zjul673"
                );

            var passwordHash_2 = hasher.Hash("asdf");
            var adminUser_2 = new User(
                "admin2",
                passwordHash_2,
                "Мария Виноградова",
                UserRole.Admin
                );

            await GenerateAdmin(adminRepository, adminUser_1, "сб, вс", unitOfWork);
            await GenerateAdmin(adminRepository, adminUser_2, "пн, ср", unitOfWork);

            var passwordHash_3 = hasher.Hash("4444");
            var quizmanUser_1 = new User(
                "quizman1",
                passwordHash_3,
                "Светлана Розина",
                UserRole.Quizman
                );

            var passwordHash_4 = hasher.Hash("47563");
            var quizmanUser_2 = new User(
                "quizman2",
                passwordHash_4,
                "Виктория Шевелева",
                UserRole.Quizman,
                null,
                new DateTime(2001, 5, 11),
                "user364"
                );

            var passwordHash_5 = hasher.Hash("htrf23");
            var quizmanUser_3 = new User(
                "quizman3",
                passwordHash_5,
                "Варвара Крохина",
                UserRole.Quizman,
                null,
                new DateTime(2003, 9, 22),
                "yejg23"
                );

            var passwordHash_6 = hasher.Hash("iedtf0367");
            var quizmanUser_4 = new User(
                "quizman4",
                passwordHash_6,
                "Екатерина Лямкина",
                UserRole.Quizman
                );

            var passwordHash_7 = hasher.Hash("quiz23");
            var quizmanUser_5 = new User(
                "quizman5",
                passwordHash_7,
                "Михаил Пономарев",
                UserRole.Quizman
                );

            var passwordHash_8 = hasher.Hash("reka879");
            var quizmanUser_6 = new User(
                "quizman6",
                passwordHash_8,
                "Владислав Поляков",
                UserRole.Quizman
                );

            await GenerateQuizman(quizmanRepository, quizmanUser_1, unitOfWork);
            await GenerateQuizman(quizmanRepository, quizmanUser_2, unitOfWork);
            await GenerateQuizman(quizmanRepository, quizmanUser_3, unitOfWork);
            await GenerateQuizman(quizmanRepository, quizmanUser_4, unitOfWork);
            await GenerateQuizman(quizmanRepository, quizmanUser_5, unitOfWork);
            await GenerateQuizman(quizmanRepository, quizmanUser_6, unitOfWork);
        }

        public static async Task GenerateAdmin(
            IAdminRepository adminRepository, 
            User user,
            string weekend,
            IUnitOfWork unitOfWork)
        {
            var admin = new Admin(user.Id, weekend);
            await adminRepository.AddAsync(admin);
            await unitOfWork.SaveChangesAsync();
        }

        public static async Task GenerateQuizman(
            IQuizmanRepository quizmanRepository,
            User user,
            IUnitOfWork unitOfWork)
        {
            var quizman = new Quizman(user.Id);
            await quizmanRepository.AddAsync(quizman);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
