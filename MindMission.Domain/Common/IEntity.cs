﻿namespace MindMission.Domain.Common
{
    public interface IEntity<T>
    {
        public T Id { get; set; }

    }
}