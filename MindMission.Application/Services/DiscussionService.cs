﻿using MindMission.Application.DTOs;
using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class DiscussionService : IDiscussionService
    {
        private readonly IDiscussionRepository Context;

        public DiscussionService(IDiscussionRepository _Context)
        {
            Context = _Context;
        }

        public Task<IQueryable<Discussion>> GetAllAsync()
        {
            return Context.GetAllAsync();
        }

        public Task<IQueryable<Discussion>> GetAllDiscussionByLessonIdAsync(int lessonId)
        {
            return Context.GetAllDiscussionByLessonIdAsync(lessonId);
        }

        public Task<Discussion> GetByIdAsync(int id)
        {
            return Context.GetByIdAsync(id);
        }

        public Task<Discussion> AddAsync(Discussion entity)
        {
            return Context.AddAsync(entity);
        }

        public Task UpdateAsync(Discussion entity)
        {
            return Context.UpdateAsync(entity);
        }

        public Task DeleteAsync(int id)
        {
            return Context.DeleteAsync(id);
        }

        public async Task<IEnumerable<Discussion>> GetAllAsync(params Expression<Func<Discussion, object>>[] IncludeProperties)
        {
            return await Context.GetAllAsync(IncludeProperties);
        }

        public Task<Discussion> GetByIdAsync(int id, params Expression<Func<Discussion, object>>[] IncludeProperties)
        {
            return Context.GetByIdAsync(id, IncludeProperties);
        }

        public async Task<IQueryable<Discussion>> GetAllDiscussionByParentIdAsync(int parentId)
        {
            return await Context.GetAllDiscussionByParentIdAsync(parentId);
        }

        
    }
}