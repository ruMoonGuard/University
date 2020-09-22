using System;
using System.Collections.Generic;
using System.Linq;
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

            if (!string.IsNullOrEmpty(uniqueName))
            {
                UniqueName = uniqueName.Length >= 6 && uniqueName.Length <= 16
                    ? uniqueName
                    : throw new ArgumentException($"{nameof(UniqueName)} field must be between 6 and 16 in length! Current: {uniqueName.Length}");
            }

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
            // Есть дублирование правил проверки, можно вынести в private set, но тогда необходимо будет вместо автопроперти определять обычные проперти
            // Часто при rich моделях выбирают вариант с дублированием в методах, которые изменяют само состояние модели
            if (!string.IsNullOrEmpty(name))
            {
                UniqueName = name.Length >= 6 && name.Length <= 16
                    ? name
                    : throw new ArgumentException($"{nameof(UniqueName)} field must be between 6 and 16 in length! Current: {name.Length}");
            }
        }

        public void AddGroup(Group group)
        {
            var studentGroup = new StudentGroup(this, group);

            if(StudentGroup.Any(m => m.GroupId == group.Id))
            {
                throw new ArgumentException($"The studentId {Id} is already a member of this groupId {group.Id}");
            }

            StudentGroup.Add(studentGroup);
        }

        public void RemoveFromGroup(Group group)
        {
            //StudentGroup.FirstOrDefault()

            //StudentGroup.Remove()
        }
    }
}
