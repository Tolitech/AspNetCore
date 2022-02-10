using System;
using FluentValidation;

namespace Tolitech.CodeGenerator.AspNetCore.Tests.Queries.Person.GetById
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
