﻿using MindMission.Application.Repository_Interfaces;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using System.Linq.Expressions;

namespace MindMission.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _context;

        public CategoryService(ICategoryRepository context)
        {
            _context = context;
        }

        public Task<IQueryable<Category>> GetAllAsync()
        {
            return _context.GetAllAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync(params Expression<Func<Category, object>>[] IncludeProperties)
        {
            return await _context.GetAllAsync(IncludeProperties);
        }

        public Task<Category> GetByIdAsync(int id)
        {
            return _context.GetByIdAsync(id);
        }

        public Task<Category> GetByIdAsync(int id, params Expression<Func<Category, object>>[] IncludeProperties)
        {
            return _context.GetByIdAsync(id, IncludeProperties);
        }

        public Task<Category> AddAsync(Category entity)
        {
            return _context.AddAsync(entity);
        }

        public Task<Category> UpdateAsync(Category entity)
        {
            return _context.UpdateAsync(entity);
        }
        public Task<Category> UpdatePartialAsync(int id, Category entity)
        {
            return _context.UpdatePartialAsync(id, entity);
        }

        public Task DeleteAsync(int id)
        {
            return _context.DeleteAsync(id);
        }
        public Task SoftDeleteAsync(int id)
        {
            return _context.SoftDeleteAsync(id);
        }
        public Task<IQueryable<Category>> GetByTypeAsync(CategoryType type)
        {
            return _context.GetByTypeAsync(type);
        }

        public Task<IQueryable<Category>> GetByParentIdAsync(int parentId)
        {
            return _context.GetByParentIdAsync(parentId);
        }

        public Task<Category> GetParentCategoryById(int parentId)
        {
            return _context.GetParentCategoryById(parentId);
        }
    }
}