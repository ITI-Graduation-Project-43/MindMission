﻿using Microsoft.AspNetCore.Http;
using MindMission.Application.DTOs;
using MindMission.Application.Interfaces.Repository;
using MindMission.Application.Interfaces.Services;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Enums;
using MindMission.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MindMission.Application.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _context;
        private readonly ILessonRepository _lessonRepository;

        public AttachmentService(IAttachmentRepository context, ILessonRepository lessonRepository)
        {
            _context = context;
            _lessonRepository = lessonRepository;
        }

        public async Task<Attachment> AddAttachmentAsync(Attachment attachment, IFormFile file, Lesson lesson)
        {
            using (var Stream = new MemoryStream())
            {
                file.CopyTo(Stream);
                attachment.FileData = Stream.ToArray();
            }

            await _context.PostAttachmentAsync(lesson, attachment);
            return attachment;
        }

        public async Task DownloadAttachmentAsync(Attachment attachment)
        {
            var FileContent = new MemoryStream(attachment.FileData);
            var FilePath = Path.Combine(Directory.GetCurrentDirectory(), "Downloaded Files", attachment.FileName);

            using(var Stream = new FileStream(FilePath,FileMode.Create, FileAccess.Write))
            {
                await FileContent.CopyToAsync(Stream);
            }
        }

        public async Task<Lesson> GetAttachmentLessonByIdAsync(int id)
        {
            Lesson Lesson = await _lessonRepository.GetByIdAsync(id);
            return Lesson;
        }

        public async Task<Attachment> GetAttachmentByIdAsync(int id)
        {
            Attachment? Attachment = await _context.GetAttachmentByIdAsync(id);
            return Attachment;
        }
    }
}