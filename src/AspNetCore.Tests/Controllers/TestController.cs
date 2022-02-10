using System;
using Microsoft.AspNetCore.Mvc;
using Tolitech.CodeGenerator.AspNetCore.Controllers;
using Tolitech.CodeGenerator.Notification;

namespace Tolitech.CodeGenerator.AspNetCore.Tests.Controllers
{
    public class TestController : BaseController
    {
        public NotificationResult Prepare(Domain.Commands.Command? command)
        {
            bool hasUser = HasUser;
            return PrepareCommand(command);
        }

        public NotificationResult Prepare(Domain.Queries.Query? query)
        {
            string? username = Username;
            return PrepareQuery(query);
        }

        public new ActionResult File(Domain.Queries.FileQueryResult file)
        {
            return base.File(file);
        }
    }
}