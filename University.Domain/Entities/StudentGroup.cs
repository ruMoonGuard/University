using System;

namespace University.Domain.Entities
{
    public class StudentGroup
    {
        public StudentGroup(Student student, Group group)
        {
            if(student == null)
            {
                throw new ArgumentNullException($"{nameof(Student)} cannot null in {nameof(StudentGroup)}");
            }

            if(group == null)
            {
                throw new ArgumentNullException($"{nameof(Group)} cannot null in {nameof(StudentGroup)}");
            }

            StudentId = student.Id;
            Student = student;

            GroupId = group.Id;
            Group = group;
        }

        public Guid StudentId { get; private set; }
        public Student Student { get; private set; }

        public Guid GroupId { get; private set; }
        public Group Group { get; private set; }
    }
}
