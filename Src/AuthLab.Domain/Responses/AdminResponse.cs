using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthLab.Domain.Responses;

public record AdminResponse
{
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
}
