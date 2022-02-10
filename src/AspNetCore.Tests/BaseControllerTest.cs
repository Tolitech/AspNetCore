using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Tolitech.CodeGenerator.AspNetCore.Tests.Controllers;
using Tolitech.CodeGenerator.Domain.Queries;

namespace Tolitech.CodeGenerator.AspNetCore.Tests
{
    public class BaseControllerTest
    {
        [Fact(DisplayName = "BaseController - PrepareCommand - Valid")]
        public void Entity_PrepareCommand_Valid()
        {
            var command = new Commands.Person.Update.UpdateCommand { Name = "Name" };
            var controller = new TestController();
            var result = controller.Prepare(command);

            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "BaseController - PrepareCommand - Invalid")]
        public void Entity_PrepareCommand_Invalid()
        {
            var command = new Commands.Person.Update.UpdateCommand { };
            var controller = new TestController();
            var result = controller.Prepare(command);

            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "BaseController - PrepareCommandNull - Invalid")]
        public void Entity_PrepareCommandNull_Invalid()
        {
            Activity.Current = new Activity("Test");
            Commands.Person.Update.UpdateCommand? command = null;
            var controller = new TestController();
            var result = controller.Prepare(command);

            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "BaseController - PrepareQuery - Valid")]
        public void Entity_PrepareQuery_Valid()
        {
            var query = new Queries.Person.GetById.GetByIdQuery { Id = Guid.NewGuid() };
            var controller = new TestController();
            var result = controller.Prepare(query);

            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "BaseController - PrepareQuery - Invalid")]
        public void Entity_PrepareQuery_Invalid()
        {
            var query = new Queries.Person.GetById.GetByIdQuery { };
            var controller = new TestController();
            var result = controller.Prepare(query);

            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "BaseController - PrepareQueryNull - Invalid")]
        public void Entity_PrepareQueryNull_Invalid()
        {
            Activity.Current = new Activity("Test");
            Queries.Person.GetById.GetByIdQuery? query = null;
            var controller = new TestController();
            var result = controller.Prepare(query);

            Assert.False(result.IsValid);
        }

        [Fact(DisplayName = "BaseController - FileNotFound - Valid")]
        public void Entity_FileNotFound_Valid()
        {
            var file = new FileQueryResult();
            var controller = new TestController();
            var result = controller.File(file);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
