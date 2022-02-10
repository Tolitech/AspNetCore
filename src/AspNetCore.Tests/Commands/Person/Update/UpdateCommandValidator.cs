using System;
using FluentValidation;

namespace Tolitech.CodeGenerator.AspNetCore.Tests.Commands.Person.Update
{
    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();
        }
    }
}
