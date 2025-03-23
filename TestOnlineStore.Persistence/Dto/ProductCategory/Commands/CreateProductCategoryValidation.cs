using FluentValidation;

namespace TestOnlineStore.Persistence.Dto.ProductCategory.Commands;

public class CreateProductCategoryValidation : AbstractValidator<CreateProductCategory>
{
    private const int MaxLengthName = 100;
    private const int MaxLengthDescription = 400;

    public CreateProductCategoryValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(MaxLengthName);

        RuleFor(x => x.Description)
            .MaximumLength(MaxLengthDescription);
    }
}
