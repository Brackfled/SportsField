using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SFFiles.Constants;
public static class SFFilesOperationClaims
{
    public const string _section = "SFFile";

    public const string Admin = $"{_section}.Admin";
    public const string Create = $"{_section}.Create";
    public const string Delete = $"{_section}.Delete";
}
