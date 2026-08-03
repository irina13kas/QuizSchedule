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
            AppDbContext context, IPasswordHasher hasher)
        {
            var passwordHash_1 = hasher.Hash("12345");
            var adminUser_1 = new User(
                "mainAdmin",
                passwordHash_1,
                "Юлия Зеленская",
                UserRole.Admin,
                "@zjul673",
                null,
                new DateTime(2004, 07, 21)
                );
            await context.Users.AddAsync(adminUser_1);

            var passwordHash_2 = hasher.Hash("asdf");
            var adminUser_2 = new User(
                "admin2",
                passwordHash_2,
                "Мария Виноградова",
                UserRole.Admin,
                "@user1"
                );
            await context.Users.AddAsync(adminUser_2);

            var admin1 = await GenerateAdmin(context, adminUser_1, "сб, вс");
            var admin2 = await GenerateAdmin(context, adminUser_2, "пн, ср");

            var passwordHash_3 = hasher.Hash("4444");
            var quizmanUser_1 = new User(
                "quizman1",
                passwordHash_3,
                "Светлана Розина",
                UserRole.Quizman,
                "@sv_ros34"
                );
            await context.Users.AddAsync(quizmanUser_1);

            var passwordHash_4 = hasher.Hash("47563");
            var quizmanUser_2 = new User(
                "quizman2",
                passwordHash_4,
                "Виктория Шевелева",
                UserRole.Quizman,
                "@frindly_fire3",
                "user364",
                new DateTime(2001, 5, 11)
                );
            await context.Users.AddAsync(quizmanUser_2);

            var passwordHash_5 = hasher.Hash("htrf23");
            var quizmanUser_3 = new User(
                "quizman3",
                passwordHash_5,
                "Варвара Крохина",
                UserRole.Quizman,
                "@var56y",
                "yejg23",
                new DateTime(2003, 9, 22)
                );
            await context.Users.AddAsync(quizmanUser_3);

            var passwordHash_6 = hasher.Hash("iedtf0367");
            var quizmanUser_4 = new User(
                "quizman4",
                passwordHash_6,
                "Екатерина Лямкина",
                UserRole.Quizman,
                "@blood_mary45"
                );
            await context.Users.AddAsync(quizmanUser_4);

            var passwordHash_7 = hasher.Hash("quiz23");
            var quizmanUser_5 = new User(
                "quizman5",
                passwordHash_7,
                "Михаил Пономарев",
                UserRole.Quizman,
                "@hoockey77"
                );
            await context.Users.AddAsync(quizmanUser_5);

            var passwordHash_8 = hasher.Hash("reka879");
            var quizmanUser_6 = new User(
                "quizman6",
                passwordHash_8,
                "Владислав Поляков",
                UserRole.Quizman,
                "sunny_today"
                );
            await context.Users.AddAsync(quizmanUser_6);

            var quizman1 = await GenerateQuizman(context, quizmanUser_1);
            var quizman2 = await GenerateQuizman(context, quizmanUser_2);
            var quizman3 = await GenerateQuizman(context, quizmanUser_3);
            var quizman4 = await GenerateQuizman(context, quizmanUser_4);
            var quizman5 = await GenerateQuizman(context, quizmanUser_5);
            var quizman = await GenerateQuizman(context, quizmanUser_6);

            var puberty = new Bar("Puberty", "Выборгская наб. 47Д", 120);
            var palma = new Bar("Palma", "ул. Салова, 61", 230);
            var amra = new Bar("Amra", "Москательный пер. 1", 190);
            await context.Bars.AddAsync(puberty);
            await context.Bars.AddAsync(palma);
            await context.Bars.AddAsync(amra);

            var dj1 = new Dj("Никита Никитин");
            var dj2 = new Dj("Илья Куртич");
            var dj3 = new Dj("Василий Кошечкин");
            var dj4 = new Dj("Лев Яшин");
            await context.Djs.AddAsync(dj1);
            await context.Djs.AddAsync(dj2);
            await context.Djs.AddAsync(dj3);
            await context.Djs.AddAsync(dj4);

            var master1 = new Master("Даниил Исаев");
            var master2 = new Master("Матвей Сафонов");
            var master3 = new Master("Артем Дзюба");
            var master4 = new Master("Никита Серебряков");
            var master5 = new Master("Александр Овечкин");
            await context.Masters.AddAsync(master1);
            await context.Masters.AddAsync(master2);
            await context.Masters.AddAsync(master3);
            await context.Masters.AddAsync(master4);
            await context.Masters.AddAsync(master5);

            var photo1 = new Photographer("Ева Польна");
            var photo2 = new Photographer("Виктория Дайнеко");
            var photo3 = new Photographer("Дмитрий Маликов");
            await context.Photographers.AddAsync(photo1);
            await context.Photographers.AddAsync(photo2);
            await context.Photographers.AddAsync(photo3);

            var date1 = DateOnly.FromDateTime(new DateTime(2026, 08, 01));
            var date2 = DateOnly.FromDateTime(new DateTime(2026, 08, 02));
            var date3 = DateOnly.FromDateTime(new DateTime(2026, 08, 03));
            var date4 = DateOnly.FromDateTime(new DateTime(2026, 08, 04));
            var date5 = DateOnly.FromDateTime(new DateTime(2026, 08, 05));
            var smart1 = new Smart(date1, SmartStatus.Available,
                quizman1.Id, null);
            var smart2 = new Smart(date2, SmartStatus.Available,
                quizman1.Id, "Только Паберти");
            var smart3 = new Smart(date3, SmartStatus.Busy,
                quizman1.Id, null);
            var smart4 = new Smart(date4, SmartStatus.Busy,
                quizman1.Id, null);
            var smart5 = new Smart(date5, SmartStatus.Available,
                quizman1.Id, "За 1000");

            var smart6 = new Smart(date1, SmartStatus.Available,
                 quizman2.Id, "За 1000");
            var smart7 = new Smart(date2, SmartStatus.Available,
                quizman2.Id, "За 1000");
            var smart8 = new Smart(date3, SmartStatus.Available,
                quizman2.Id, "За 1000");

            var smart9 = new Smart(date1, SmartStatus.LeaveTown,
                quizman3.Id, null);
            var smart10 = new Smart(date2, SmartStatus.LeaveTown,
                quizman3.Id, null);
            var smart11 = new Smart(date3, SmartStatus.LeaveTown,
                quizman3.Id, null);
            var smart12 = new Smart(date4, SmartStatus.LeaveTown,
                quizman3.Id, null);

            var smart13 = new Smart(date1, SmartStatus.Available,
                quizman4.Id, "За 1000");
            var smart14 = new Smart(date2, SmartStatus.Available,
                quizman4.Id, "Пальма");
            await context.Smarts.AddAsync(smart1);
            await context.Smarts.AddAsync(smart2);
            await context.Smarts.AddAsync(smart3);
            await context.Smarts.AddAsync(smart4);
            await context.Smarts.AddAsync(smart5);
            await context.Smarts.AddAsync(smart6);
            await context.Smarts.AddAsync(smart7);
            await context.Smarts.AddAsync(smart8);
            await context.Smarts.AddAsync(smart9);
            await context.Smarts.AddAsync(smart10);
            await context.Smarts.AddAsync(smart11);
            await context.Smarts.AddAsync(smart12);
            await context.Smarts.AddAsync(smart13);
            await context.Smarts.AddAsync(smart14);


            var game1 = new Game("1932.2", 
                admin1.Id,
                puberty.Id,
                null,
                admin1.Id,
                new DateTime(2026, 08, 03, 19, 30, 00),
                new DateTime(2026, 08, 03, 17, 30, 00),
                master3.Id, dj2.Id, photo2.Id
                );

            var game2 = new Game("изи 160.1",
                admin2.Id,
                palma.Id,
                "Балтика",
                admin1.Id,
                new DateTime(2026, 08, 03, 19, 30, 00),
                new DateTime(2026, 08, 03, 17, 30, 00),
                master4.Id, dj1.Id, photo3.Id
                );

            var game3 = new Game("МП 23.6",
                admin2.Id,
                amra.Id,
                "Самбери серт на 1000",
                admin1.Id,
                new DateTime(2026, 08, 04, 19, 30, 00),
                new DateTime(2026, 08, 04, 17, 30, 00),
                master3.Id, dj2.Id, photo1.Id
                );

            var game4 = new Game("Про футбол 1.2",
                admin1.Id,
                amra.Id,
                @"Спа ""Сибирская сосна""",
                admin1.Id,
                new DateTime(2026, 08, 04, 19, 30, 00),
                new DateTime(2026, 08, 04, 17, 30, 00),
                master3.Id, dj2.Id, photo2.Id
                );

            var game5 = new Game("Королевская битва 1.1",
                admin2.Id,
                puberty.Id,
                null,
                admin1.Id,
                new DateTime(2026, 08, 05, 19, 30, 00),
                new DateTime(2026, 08, 05, 17, 30, 00),
                master3.Id, dj2.Id, photo2.Id
                );
            await context.Games.AddAsync( game1 );
            await context.Games.AddAsync( game2 );
            await context.Games.AddAsync( game3 );
            await context.Games.AddAsync( game4 );
            await context.Games.AddAsync( game5 );

            var part1 = new GameParticipant(game1.Id, 
                quizman1.Id, true, false);
            var part2 = new GameParticipant(game1.Id,
                quizman2.Id, true, true);
            var part3 = new GameParticipant(game1.Id,
                quizman3.Id, true, true);
            var part4 = new GameParticipant(game1.Id,
                quizman4.Id, false, false);

            await context.Participants.AddAsync( part1 );
            await context.Participants.AddAsync( part2 );
            await context.Participants.AddAsync( part3 );
            await context.Participants.AddAsync( part4 );

            await context.SaveChangesAsync();
        }

        public static async Task<Admin> GenerateAdmin(
            AppDbContext context, 
            User user,
            string weekend)
        {
            var admin = new Admin(user.Id, weekend);
            await context.Admins.AddAsync(admin);
            await context.SaveChangesAsync();
            return admin;
        }

        public static async Task<Quizman> GenerateQuizman(
            AppDbContext context,
            User user)
        {
            var quizman = new Quizman(user.Id);
            await context.Quizmen.AddAsync(quizman);
            await context.SaveChangesAsync();
            return quizman;
        }
    }
}
