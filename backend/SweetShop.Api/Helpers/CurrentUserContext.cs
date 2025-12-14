using System.Security.Claims;

namespace SweetShop.Api.Helpers
{
    public class CurrentUserContext: ICurrentUserContext
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        public CurrentUserContext(IHttpContextAccessor _httpContextAccessor)
        {
            httpContextAccessor = _httpContextAccessor;
        }
        public int UserId => Convert.ToInt32(httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier));
        public string Email => Convert.ToString(httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Email))!;
        public string Role => Convert.ToString(httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role))!;
        public bool IsAdmin => Role == "Admin" ? true : false;
    }
}
