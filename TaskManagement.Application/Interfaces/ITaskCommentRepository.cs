﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface ITaskCommentRepository : IRepository<TaskComment>
    {
        void Update(TaskComment entity);
    }
}
