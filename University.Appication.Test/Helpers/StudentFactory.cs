using AutoFixture;
using System;
using System.Linq;
using University.Domain.Entities;
using University.Domain.Entities.Enums;

namespace University.Appication.Test.Helpers
{
    public static class StudentFactory
    {
        private static int firstNameMaxLength = 40;
        private static int lastNameMaxLength = 40;
        private static int middleNameMaxLength = 60;
        private static int uniqueNameMaxLength = 16;

        public static Student CorrectStudent(bool useMiddleName = true, bool useUniqueName = true, Group group = null)
        {
            var fixture = new Fixture();

            var id = Guid.NewGuid();
            var gender = fixture.Create<Gender>();

            // По умолчанию Create<string>() создает стркоу размером 36.
            var firstName = string.Join("", fixture.CreateMany<string>(2)).Substring(0, firstNameMaxLength);
            var lastName = string.Join("", fixture.CreateMany<string>(2)).Substring(0, lastNameMaxLength);

            string middleName = null;
            string uniqueName = null;

            if(useMiddleName)
            {
                middleName = string.Join("", fixture.CreateMany<string>(2)).Substring(0, middleNameMaxLength);
            }

            if(useUniqueName)
            {
                uniqueName = string.Join("", fixture.CreateMany<string>(2)).Substring(0, uniqueNameMaxLength);
            }

            var student = new Student(id, gender, firstName, lastName, middleName, uniqueName);

            if(group != null)
            {
                student.AddGroup(group);
            }

            return student;
        }
    }
}
