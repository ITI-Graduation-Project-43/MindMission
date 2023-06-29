﻿using MindMission.Application.DTOs;
using MindMission.Application.Mapping.Base;
using MindMission.Domain.Models;
using System;
using System.Linq;

namespace MindMission.Application.Mapping
{
    public class DiscussionMappingService : IMappingService<Discussion, DiscussionDto>
    {
        public Discussion MapDtoToEntity(DiscussionDto dto)
        {
            return new Discussion
            {
                Id = dto.Id,
                LessonId = dto.LessonId,
                UserId = dto.UserId,
                Content = dto.Content,
                Upvotes = dto.Upvotes,
                ParentDiscussionId = dto.ParentDiscussionId ?? null
            };
        }

        public async Task<DiscussionDto> MapEntityToDto(Discussion entity)
        {
            var discussionDTO = new DiscussionDto
            {
                Id = entity.Id,
                UserId = entity.UserId,
                LessonId = entity.LessonId,
                Content = entity.Content,
                Upvotes = entity.Upvotes,
                ParentDiscussionId = entity.ParentDiscussionId,
            };
            if (entity.ParentDiscussion != null)
            {
                discussionDTO.ParentContent = entity.ParentDiscussion.Content;
            }
            if (entity.User != null)
            {
                discussionDTO.Username = entity.User.Email;

            }

            return discussionDTO;
        }
    }
}
