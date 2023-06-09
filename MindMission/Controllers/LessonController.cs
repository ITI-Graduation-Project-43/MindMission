﻿using Microsoft.AspNetCore.Mvc;
using MindMission.API.Controllers.Base;
using MindMission.Application.DTOs;
using MindMission.Application.Mapping;
using MindMission.Application.Service_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;

namespace MindMission.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonController : BaseController<Lesson, LessonDto, int>
    {
        private readonly ILessonService _lessonService;

        public LessonController(ILessonService lessonService, LessonMappingService lessonMappingService) :
            base(lessonMappingService)
        {
            _lessonService = lessonService ?? throw new ArgumentNullException(nameof(lessonService));
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetAllLesson([FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponseWithInclude(
                _lessonService.GetAllAsync,
                pagination,
                "Lessons",
                lesson => lesson.Chapter,
                lesson => lesson.Chapter.Course
            );
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LessonDto>> GetLessonById(int id)
        {
            return await GetEntityResponseWithInclude(
                   () => _lessonService.GetByIdAsync(id,
                       lesson => lesson.Chapter,
                       lesson => lesson.Chapter.Course
                   ),
                   "Lesson"
               );
        }


        [HttpGet("byCourse/{courseId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByCourseIdAsync(int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _lessonService.GetByCourseIdAsync(courseId), pagination, "Lessons");
        }

        [HttpGet("byChapter/{chapterId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByChapterIdAsync(int chapterId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _lessonService.GetByChapterIdAsync(chapterId), pagination, "Lessons");
        }

        [HttpGet("byCourseAndChapter/{courseId}/{chapterId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByCourseAndChapterIdAsync(int courseId, int chapterId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _lessonService.GetByCourseAndChapterIdAsync(courseId, chapterId), pagination, "Lessons");
        }

        [HttpGet("freeByCourse/{courseId}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetFreeByCourseIdAsync(int courseId, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _lessonService.GetFreeByCourseIdAsync(courseId), pagination, "Lessons");
        }

        [HttpGet("byType/{courseId}/{type}")]
        public async Task<ActionResult<IQueryable<LessonDto>>> GetByTypeAsync(int courseId, LessonType type, [FromQuery] PaginationDto pagination)
        {
            return await GetEntitiesResponse(() => _lessonService.GetByTypeAsync(courseId, type), pagination, "Lessons");
        }


        [HttpPost("Lesson")]
        public async Task<ActionResult<LessonDto>> AddLesson([FromBody] LessonDto lessonDto)
        {
            return await AddEntityResponse(_lessonService.AddAsync, lessonDto, "Lesson", nameof(GetLessonById));
        }

        [HttpPut("{lessonId}")]
        public async Task<ActionResult> UpdateLesson(int lessonId, LessonDto lessonDto)
        {
            return await UpdateEntityResponse(_lessonService.GetByIdAsync, _lessonService.UpdateAsync, lessonId, lessonDto, "Lesson");
        }

        [HttpDelete("{lessonId}")]
        public async Task<IActionResult> DeleteLesson(int lessonId)
        {
            return await DeleteEntityResponse(_lessonService.GetByIdAsync, _lessonService.DeleteAsync, lessonId);
        }
    }
}