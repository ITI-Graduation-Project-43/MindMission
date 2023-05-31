﻿using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;

namespace MindMission.Application.Mapping
{
    public class AdminMappingService : IMappingService<Admin, AdminDto>
    {
        private readonly IAdminService _adminService;

        public AdminMappingService(IAdminService adminService)
        {
            _adminService = adminService;
        }

        public async Task<AdminDto> MapEntityToDto(Admin admin)
        {
            return new AdminDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                ProfilePicture = admin.ProfilePicture,
                PasswordHash = admin.PasswordHash,
                IsDeactivated = admin.IsDeactivated,
                CreatedAt = admin.CreatedAt,
                UpdatedAt = admin.UpdatedAt
            };
        }

        public Admin MapDtoToEntity(AdminDto dto)
        {
            return new Admin
            {
                Id = dto.Id,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                ProfilePicture = dto.ProfilePicture,
                PasswordHash = dto.PasswordHash,
                IsDeactivated = dto.IsDeactivated,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt
            };
        }
    }
}