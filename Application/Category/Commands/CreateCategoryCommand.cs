using Application.Category.Dto;
using Domain.common;
using Domain.Model.Category;
using Mapster;
namespace Application.Category.Commands;
using MediatR;
using FluentValidation;
public class CreateCategoryCommand:IRequest<Result<CategoryDto>>
{
    public string ArabicName { get; set; }
    public string EnglishName { get; set; }

    class Validator:AbstractValidator<CreateCategoryCommand>
    {
        public Validator()
        {
            RuleFor(c => c.ArabicName).NotEmpty();
            RuleFor(c => c.EnglishName).NotEmpty();
        }
    }

    class Handler:IRequestHandler<CreateCategoryCommand,Result<CategoryDto>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public Handler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryDto>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = request.Adapt<Domain.Model.Category.Category>();
            var result = await _categoryRepository.Add(category,cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<CategoryDto>().AsSuccessResult()
                : Result.Failure<CategoryDto>(result.Error);
        }
    }
    
}