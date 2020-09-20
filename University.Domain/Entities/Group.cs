using System;
using System.Collections.Generic;

namespace University.Domain.Entities
{
    public class Group
    {
        public Group(Guid id, string name)
        {
            Id = id != Guid.Empty 
                ? id 
                : throw new ArgumentException($"{nameof(Id)} for group cannot empty");

            Name = !string.IsNullOrEmpty(name) 
                ? name 
                : throw new ArgumentNullException($"{nameof(Name)} for group cannot null or empty");

            StudentGroup = new List<StudentGroup>();
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public List<StudentGroup> StudentGroup { get; private set; }

        public void ChangeName(string name)
        {
            Name = !string.IsNullOrEmpty(name) 
                ? name 
                : throw new ArgumentNullException($"{nameof(Name)} for group cannot null or empty");
        }
    }
}
