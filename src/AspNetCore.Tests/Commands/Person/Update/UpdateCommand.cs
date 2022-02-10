using System;
using Tolitech.CodeGenerator.Domain.Commands;

namespace Tolitech.CodeGenerator.AspNetCore.Tests.Commands.Person.Update
{
    public class UpdateCommand : Command
    {
        public string? Name { get; set; }

        public override void Validate()
        {
            var validator = new UpdateCommandValidator();
            Validate(validator.Validate(this));
        }
    }
}
