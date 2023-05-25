﻿using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Services;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Mapping
{
    public class InstructorMappingService : IMappingService<Instructor, InstructorDto>
    {
        
        private readonly IUserAccountService _userAccountService;
        public InstructorMappingService(IUserAccountService userAccountContext) {
             _userAccountService = userAccountContext;
        }
        public Instructor MapDtoToEntity(InstructorDto instructorDto)
        {
            return new Instructor
            {
                Id = instructorDto.Id,
                FirstName = instructorDto.FirstName,
                LastName = instructorDto.LastName,
                Bio = instructorDto.Bio,
                Title = instructorDto.Title,
                Description = instructorDto.Description,
                NoOfCourses = instructorDto.NoOfCourses,
                NoOfRatings = instructorDto.NoOfStudents,
                NoOfStudents = instructorDto.NoOfStudents,
                AvgRating = instructorDto.AvgRating,
                CreatedAt = DateTime.Now,
                UpdatedAt = null
            };
        }

        public async Task<InstructorDto> MapEntityToDto(Instructor entity)
        {
            var InstructorDTO = new InstructorDto()
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Bio = entity.Bio,
                Title = entity.Title,
                Description = entity.Description,
                NoOfCources = entity.NoOfCourses,
                AvgRating = (double)entity.AvgRating,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                NoOfStudents = entity.NoOfStudents,
                ProfilePicture = entity.ProfilePicture,
                NoOfRating = entity.NoOfRatings
            };
            
            var UserAccounts =  _userAccountService.GetUserAccountsAsync(entity.Id);
            foreach (var account in UserAccounts)
            {
                InstructorDTO.accounts.Add(account.Account.AccountType, account.AccountLink);
            }
            return InstructorDTO;
        }
    }
}
