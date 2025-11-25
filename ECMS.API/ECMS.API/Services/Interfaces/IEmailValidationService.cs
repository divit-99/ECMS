namespace ECMS.API.Services.Interfaces
{
    public interface IEmailValidationService
    {
        Task<bool> IsValidAsync(string email);
    }
}
