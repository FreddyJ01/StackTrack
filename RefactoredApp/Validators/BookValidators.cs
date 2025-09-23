using FluentValidation;
using StackTrack.RefactoredApp.DTOs;

namespace StackTrack.RefactoredApp.Validators;

public class CreateBookDtoValidator : AbstractValidator<CreateBookDto>
{
    public CreateBookDtoValidator()
    {
        RuleFor(x => x.BookTitle)
            .NotEmpty().WithMessage("Book title is required")
            .Length(1, 200).WithMessage("Book title must be between 1 and 200 characters");

        RuleFor(x => x.BookAuthor)
            .NotEmpty().WithMessage("Book author is required")
            .Length(1, 100).WithMessage("Book author must be between 1 and 100 characters");

        RuleFor(x => x.BookGenre)
            .NotEmpty().WithMessage("Book genre is required")
            .Length(1, 50).WithMessage("Book genre must be between 1 and 50 characters");
    }
}

public class CheckoutBookDtoValidator : AbstractValidator<CheckoutBookDto>
{
    public CheckoutBookDtoValidator()
    {
        RuleFor(x => x.BookID)
            .NotEmpty().WithMessage("Book ID is required");

        RuleFor(x => x.UserID)
            .NotEmpty().WithMessage("User ID is required");
    }
}