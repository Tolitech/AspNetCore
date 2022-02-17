using System;
using Microsoft.AspNetCore.Authorization;

namespace Tolitech.CodeGenerator.AspNetCore.Security
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
