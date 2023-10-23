using Application.Category.Dto;
using Domain.common;
using Domain.Model.Category;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Category.Commands;

public class UpdateCategoryCommand:IRequest<Result<CategoryDto>>
{
    public string Id { get; set; }
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }

    class Validator:AbstractValidator<UpdateCategoryCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
            RuleFor(c => c.ArabicName).NotEmpty();
            RuleFor(c => c.EnglishName).NotEmpty();
        }
    }

    class Handler:IRequestHandler<UpdateCategoryCommand,Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ApplicationDbContext _context;

        public Handler(ApplicationDbContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _context.Categories.FindAsync(request.Id);
            if(category is null)
                return Result.Failure<CategoryDto>("Category Not Found");
            request.Adapt(category);
            var result = await _categoryRepository.Update(category, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<CategoryDto>().AsSuccessResult()
                : Result.Failure<CategoryDto>(result.Error);
        }
    }
}