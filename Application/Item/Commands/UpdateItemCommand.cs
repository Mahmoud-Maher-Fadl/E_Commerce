using Application.Item.Dto;
using Domain.common;
using Domain.Model.Item;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Item.Commands;

public class UpdateItemCommand:IRequest<Result<ItemDto>>
{
    public string Id { get; set; }
    public string ArabicName{ get; set; }
    public string EnglishName{ get; set; }
    public string Description{ get; set; }
    public string CategoryId { get; set; }
    public decimal Price{ get; set; }
    public int Quantity{ get; set; }

    class Validator:AbstractValidator<UpdateItemCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.ArabicName).NotEmpty();
            RuleFor(c => c.EnglishName).NotEmpty();
            RuleFor(c => c.Description).NotEmpty();
            RuleFor(c => c.CategoryId).NotEmpty();
            RuleFor(c => c.Price).NotEmpty();
            RuleFor(c => c.Quantity).NotEmpty();
        }
    }

    class Handler:IRequestHandler<UpdateItemCommand,Result<ItemDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly IItemRepository _itemRepository;

        public Handler(ApplicationDbContext context, IItemRepository itemRepository)
        {
            _context = context;
            _itemRepository = itemRepository;
        }

        public async Task<Result<ItemDto>> Handle(UpdateItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _context.Items.FindAsync(request.Id);
            if(item is null)
                return Result.Failure<ItemDto>($"Item Not Found");
            request.Adapt(item);
            var result = await _itemRepository.Update(item, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<ItemDto>().AsSuccessResult()
                : Result.Failure<ItemDto>(result.Error);
        }
    }
}