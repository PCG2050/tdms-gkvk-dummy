using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Services
{
    public interface IRouteService
    {
        string? GetPathForAction(string actionName, object? routeValues = null);
    }
}
