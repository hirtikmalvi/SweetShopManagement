namespace SweetShop.Api.Helpers
{
    public interface ICurrentUserContext
    {
        int UserId { get; }
        string Email { get; }
        string Role { get; }
        bool IsAdmin { get; }
    }
}
