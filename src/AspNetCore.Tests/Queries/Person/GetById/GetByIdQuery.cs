using System;
using Tolitech.CodeGenerator.Domain.Queries;

namespace Tolitech.CodeGenerator.AspNetCore.Tests.Queries.Person.GetById
{
    public class GetByIdQuery : Query
    {
        public Guid? Id { get; set; }

        public override void Validate()
        {
            var validator = new GetByIdQueryValidator();
            Validate(validator.Validate(this));
        }
    }
}
