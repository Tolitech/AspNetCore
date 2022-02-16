using System;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Tolitech.CodeGenerator.Domain.Commands;
using Tolitech.CodeGenerator.Domain.Queries;
using Tolitech.CodeGenerator.Notification;
using Tolitech.CodeGenerator.Pagination;

namespace Tolitech.CodeGenerator.AspNetCore.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        #region Security

        protected Guid? UserId
        {
            get
            {
                var claim = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);

                if (claim == null)
                    return null;

                return new Guid(claim.Value);
            }
        }

        protected string? Username
        {
            get
            {
                var claim = User?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);

                if (claim == null)
                    return null;

                return claim.Value;
            }
        }

        protected bool HasUser
        {
            get
            {
                return UserId.HasValue;
            }
        }

        #endregion

        #region File

        protected void SetFileCommand(FileCommand command)
        {
            if (!string.IsNullOrEmpty(command.Name))
            {
                var file = Request.Form.Files[command.Name];

                if (file != null)
                {
                    using var memoryStream = new MemoryStream();
                    command.Name = file.Name;
                    command.FileName = file.FileName;
                    command.Length = file.Length;
                    command.ContentType = file.ContentType;

                    file.CopyTo(memoryStream);
                    command.File = memoryStream.ToArray();
                }
                else
                    command.Changed = Convert.ToBoolean(Request.Form[command.Name + "Changed"]);
            }
        }

        protected ActionResult File(FileQueryResult result)
        {
            if (result == null || result.File == null)
                return NotFound();

            Response.Headers["Content-Disposition"] = "attachment";

            if (string.IsNullOrEmpty(result.ContentType))
                result.ContentType = "application/octet-stream";

            if (string.IsNullOrEmpty(result.FileName))
                result.FileName = result.Name;

            return File(result.File, result.ContentType, result.FileName);
        }

        #endregion

        #region Pagination

        protected void AddPaginationHeader<T>(PaginatedList<T> items, int maxPages = 5)
        {
            if (items != null)
            {
                var paginationHeader = new Paginated<T>(items, maxPages);
                HttpContext.Response.Headers.Add("Access-Control-Expose-Headers", "X-Pagination");
                HttpContext.Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationHeader));
            }
        }

        #endregion

        #region Validation

        protected NotificationResult PrepareCommand(Command? command)
        {
            SetActivityTags();

            if (command == null)
            {
                var result = new NotificationResult();
                result.AddError(Resources.Controller.ControllerResource.CommandNull);
                return result;
            }

            command.SetLoggedUser(UserId);
            return command.GetNotifications();
        }

        protected NotificationResult PrepareQuery(Query? query)
        {
            SetActivityTags();

            if (query == null)
            {
                var result = new NotificationResult();
                result.AddError(Resources.Controller.ControllerResource.QueryNull);
                return result;
            }

            query.SetLoggedUser(UserId);
            return query.GetNotifications();
        }

        #endregion

        #region Activity tags

        private void SetActivityTags()
        {
            if (Activity.Current != null)
            {
                Activity.Current.AddTag("UserId", UserId);
                Activity.Current.AddTag("Username", Username);
            }
        }

        #endregion
    }
}