using Application.Item.Dto;
using Domain.common;
using Domain.Model.Item;
using FluentValidation;
using Mapster;
using MediatR;

namespace Application.Item.Commands;

public class CreateItemCommand:IRequest<Result<ItemDto>>
{
    public string ArabicName{ get; set; }
    public string EnglishName{ get; set; }
    public string Description{ get; set; }
    public string CategoryId { get; set; }
    public decimal Price{ get; set; }
    public int Quantity{ get; set; }

    class Validator:AbstractValidator<CreateItemCommand>
    {
        public Validator()
        {
            RuleFor(c => c.ArabicName).NotEmpty();
            RuleFor(c => c.EnglishName).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.CategoryId).NotEmpty();
            RuleFor(c => c.Price).NotEmpty();
            RuleFor(c => c.Quantity).NotEmpty();
        }
    }

    class Handler:IRequestHandler<CreateItemCommand,Result<ItemDto>>
    {
        private readonly IItemRepository _itemRepository;

        public Handler(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<Result<ItemDto>> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            var item = request.Adapt<Domain.Model.Item.Item>();
            var result = await _itemRepository.Add(item, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<ItemDto>().AsSuccessResult()
                : Result.Failure<ItemDto>(result.Error);
        }
    }
}