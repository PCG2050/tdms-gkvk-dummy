


namespace Infrastructure.Services
{
    public class RouteService : IRouteService
    {
        private readonly IUrlHelperFactory _urlHelperFactory;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RouteService(IUrlHelperFactory urlHelperFactory, IHttpContextAccessor httpContextAccessor)
        {
            _urlHelperFactory = urlHelperFactory;
            _httpContextAccessor = httpContextAccessor;
        }
        private IUrlHelper GetUrlHelper()
        {
            var actionContext = _httpContextAccessor.HttpContext?.RequestServices.GetRequiredService<IActionContextAccessor>()?.ActionContext;

            if (actionContext == null)
            {
                throw new InvalidOperationException("HttpContext or ActionContext is null. Cannot generate URLs outside of a request scope.");
            }

            return _urlHelperFactory.GetUrlHelper(actionContext);
        }
        public string? GetPathForAction(string actionName, object? routeValues = null)
        {
            var urlHelper = GetUrlHelper();
            return urlHelper.Action(actionName, routeValues);
        }
    }
}
