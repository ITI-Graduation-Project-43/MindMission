﻿using Microsoft.EntityFrameworkCore;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;
using System.ComponentModel.DataAnnotations;
using System.Formats.Asn1;

namespace MindMission.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        private readonly MindMissionDbContext _context;

        public CategoryRepository(MindMissionDbContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Category>> GetByTypeAsync(CategoryType type)
        {
            return await _context.Categories
                .Include(category => category.Parent)
                .ThenInclude(parentCategory => parentCategory.Parent)
                .Where(category => category.Type == type)
                .ToListAsync();
        }



        public async Task<IEnumerable<Category>> GetByParentIdAsync(int parentId)
        {
            return await _context.Categories
                .Include(category => category.Parent)
                .ThenInclude(parentCategory => parentCategory.Parent)
                .Where(category => category.ParentId == parentId)
                .ToListAsync();
        }

    }
}