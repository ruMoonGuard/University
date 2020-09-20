using Microsoft.AspNetCore.Builder;
using System;
using University.Infrastructure.Data;

namespace University.API.Code.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void SeedDb(this IApplicationBuilder app, UniversityContext context)
        {
            var student1 = new Domain.Entities.Student(Guid.NewGuid(), Domain.Entities.Enums.Gender.Female, "Алиса", "Панкер", "Петровна");
            var student2 = new Domain.Entities.Student(Guid.NewGuid(), Domain.Entities.Enums.Gender.Male, "Джонни", "Депп", uniqueName: "Актёр");
            var student3 = new Domain.Entities.Student(Guid.NewGuid(), Domain.Entities.Enums.Gender.Male, "Андрей", "Шемнов", "Андреевич", "Путник");
            var student4 = new Domain.Entities.Student(Guid.NewGuid(), Domain.Entities.Enums.Gender.Female, "Марина", "Вторая");

            context.Students.Add(student1);
            context.Students.Add(student2);
            context.Students.Add(student3);
            context.Students.Add(student4);

            var group1 = new Domain.Entities.Group(Guid.NewGuid(), "ФЭС-1");
            var group2 = new Domain.Entities.Group(Guid.NewGuid(), "АД-101");

            context.Groups.Add(group1);
            context.Groups.Add(group2);

            context.SaveChanges();
        }
    }
}
