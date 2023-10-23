using Application.Item.Dto;
using Domain.common;
using Domain.Model.Item;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Item.Commands;

public class DeleteItemCommand:IRequest<Result<ItemDto>>
{
    public string Id { get; set; }

    class Handler:IRequestHandler<DeleteItemCommand,Result<ItemDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IItemRepository _itemRepository;

        public Handler(ApplicationDbContext context, IItemRepository itemRepository)
        {
            _context = context;
            _itemRepository = itemRepository;
        }

        public async Task<Result<ItemDto>> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id);
            if (item is null)
                return Result.Failure<ItemDto>($"Item Not Found");
            var result = await _itemRepository.Delete(item, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<ItemDto>().AsSuccessResult()
                : Result.Failure<ItemDto>(result.Error);
        }
    }
}