using Application.Category.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Category.Queries;

public class GetCategoriesQuery:IRequest<Result<List<CategoryDto>>>
{
    class Handler:IRequestHandler<GetCategoriesQuery,Result<List<CategoryDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await _context.Categories.ProjectToType<CategoryDto>().ToListAsync(cancellationToken);
            return categories.AsSuccessResult();
        }
    }
}