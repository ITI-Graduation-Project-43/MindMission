﻿using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Services_Classes
{
    public class ChapterService : IChapterService
    {
        private readonly IChapterRepository _context;

        public ChapterService(IChapterRepository context)
        {
            _context = context;
        }

        public Task<IEnumerable<Chapter>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public Task<Chapter> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Chapter> AddAsync(Chapter entity)
        {
            return _context.AddAsync(entity);
        }

        public Task UpdateAsync(Chapter entity)
        {
            return _context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
    }
}
