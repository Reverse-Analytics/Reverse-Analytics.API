using FluentValidation;
using ReverseAnalytics.Domain.DTOs.ProductCategory;

namespace ReverseAnalytics.Domain.Validators.ProductCategory;

public class ProductCategoryForCreateValidator : AbstractValidator<ProductCategoryForCreateDto>
{
    public ProductCategoryForCreateValidator()
    {
        RuleFor(x => x.Name)
            .NotNull()
            .NotEmpty()
            .WithMessage("Category name cannot be empty.")
            .MinimumLength(5)
            .WithMessage("Category name must contain at least 5 characters.")
            .MaximumLength(255)
            .WithMessage("Category name must contain max 255 characters.");
        RuleFor(x => x.Description)
            .MaximumLength(4000)
            .WithMessage("Description can contain 4000 max characters");
    }
}
