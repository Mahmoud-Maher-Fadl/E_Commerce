using Application.Item.Dto;
using Domain.common;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Item.Queries;

public class GetItemsQuery:IRequest<Result<List<ItemDto>>>
{
    class Handler:IRequestHandler<GetItemsQuery,Result<List<ItemDto>>>
    {
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<ItemDto>>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.Items.Include(i=>i.Category).ProjectToType<ItemDto>().ToListAsync(cancellationToken);
            return items.AsSuccessResult();
        }
    }
}