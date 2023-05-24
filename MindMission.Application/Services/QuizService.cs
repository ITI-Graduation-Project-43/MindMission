﻿using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Services_Classes
{
    public class QuizService : IQuizService
    {
        private readonly IQuizRepository _context;

        public QuizService(IQuizRepository context)
        {
            _context = context;
        }

        public Task<IEnumerable<Quiz>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Quiz> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Quiz> AddAsync(Quiz entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Quiz entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
    }
}
