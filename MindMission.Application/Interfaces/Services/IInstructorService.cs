﻿using MindMission.Application.Interfaces.Services.Base;
using MindMission.Application.Repository_Interfaces;
using MindMission.Domain.Models;

namespace MindMission.Application.Service_Interfaces
{
    public interface IInstructorService : IService<Instructor, string>, IInstructorRepository
    {

    }
}