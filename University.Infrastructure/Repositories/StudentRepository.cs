﻿using System;
using System.Threading.Tasks;
using University.Domain.Entities;
using University.Domain.Interfaces;

namespace University.Infrastructure.Data.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        public Task<Guid> AddAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistByUniqueNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }
}