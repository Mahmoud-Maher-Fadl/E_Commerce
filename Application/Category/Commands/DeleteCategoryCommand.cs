using Application.Category.Dto;
using Domain.common;
using Domain.Model.Category;
using FluentValidation;
using Infrastructure;
using Mapster;
using MediatR;

namespace Application.Category.Commands;

public class DeleteCategoryCommand:IRequest<Result<CategoryDto>>
{
    public string Id;

    class Validator:AbstractValidator<DeleteCategoryCommand>
    {
        public Validator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }

    class Handler:IRequestHandler<DeleteCategoryCommand,Result<CategoryDto>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoryRepository _categoryRepository;

        public Handler(ApplicationDbContext context, ICategoryRepository categoryRepository)
        {
            _context = context;
            _categoryRepository = categoryRepository;
        }

        public async Task<Result<CategoryDto>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            
            var category = await _context.Categories.FindAsync(request.Id);
            if(category is null)
                return Result.Failure<CategoryDto>("Category Not Found");
            var result =await _categoryRepository.Delete(category, cancellationToken);
            //var result =await _categoryRepository.DeleteById(request.Id, cancellationToken);
            return result.IsSuccess
                ? result.Value.Adapt<CategoryDto>().AsSuccessResult()
                : Result.Failure<CategoryDto>(result.Error);
        }
    }
}