﻿using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;
using MindMission.Infrastructure.Context;

namespace MindMission.Infrastructure.Repositories
{
    public class ChapterRepository : Repository<Chapter, int>, IChapterRepository
    {
        public ChapterRepository(MindMissionDbContext context) : base(context)
        {

        }
    }
}