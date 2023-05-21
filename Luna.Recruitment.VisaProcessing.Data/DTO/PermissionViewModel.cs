using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Luna.Recruitment.VisaProcessing.Data.DTO
{
public class PermissionViewModel
{
    public string UserId { get; set; }
    public IList<RoleClaimsViewModel> RoleClaims { get; set; }
}

public class RoleClaimsViewModel
{
    public string Type { get; set; }
    public string Value { get; set; }
    public bool Selected { get; set; }
}
}
