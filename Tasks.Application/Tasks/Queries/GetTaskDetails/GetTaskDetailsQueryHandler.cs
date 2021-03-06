using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using System.Threading;
using AutoMapper;
using Tasks.Application.Interfaces;
using Tasks.Domain;
using Tasks.Application.Common.Exeption;
using Microsoft.EntityFrameworkCore;

namespace Tasks.Application.Tasks.Queries.GetTaskDetails
{
    public class GetTaskDetailsQueryHandler : IRequestHandler<GetTaskDetaailsQuery, GetTaskVm>
    {
        private readonly ITasksDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetTaskDetailsQueryHandler(ITasksDbContext dbContext,
            IMapper mapper) => (_dbContext, _mapper) = (dbContext, mapper);

        public async Task<GetTaskVm> Handle(GetTaskDetaailsQuery request,
            CancellationToken cancellationToken)
        {
            var entity = await _dbContext.STasks
                .FirstOrDefaultAsync(task =>
                task.ID == request.ID, cancellationToken);

            if (entity == null || entity.UserID != request.UserID)
            {
                throw new NotFoundExeption(nameof(TaskD), request.ID);
            }

            return _mapper.Map<GetTaskVm>(entity);
        }
    }
}
