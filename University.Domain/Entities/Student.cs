using System;
using System.Collections.Generic;
using University.Domain.Entities.Enums;

namespace University.Domain.Entities
{
    public class Student
    {
        public Student(Guid id, Gender gender, string firstName, string lastName, string middleName = null, string uniqueName = null)
        {
            Id = id;
            Gender = gender;
            FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException($"{nameof(FirstName)} cannot be empty"); 
            LastName = !string.IsNullOrEmpty(lastName) ? firstName : throw new ArgumentNullException($"{nameof(FirstName)} cannot be empty");
            MiddleName = middleName;
            UniqueName = uniqueName;

            StudentGroup = new List<StudentGroup>();
        }

        public Guid Id { get; private set; }
        public Gender Gender { get; private set; }
        public string FirstName { get; private set; }
        public string MiddleName { get; private set; }
        public string LastName { get; private set; }
        public string UniqueName { get; private set; }

        public List<StudentGroup> StudentGroup { get; private set; }

        public void ChangeFIO(string firstName, string middleName, string lastName)
        {
            FirstName = !string.IsNullOrEmpty(firstName) ? firstName : throw new ArgumentNullException($"{nameof(FirstName)} cannot be empty");
            LastName = !string.IsNullOrEmpty(lastName) ? firstName : throw new ArgumentNullException($"{nameof(FirstName)} cannot be empty");
            MiddleName = middleName;
        }

        public void ChangeUniqueName(string name)
        {
            UniqueName = name;
        }
    }
}
