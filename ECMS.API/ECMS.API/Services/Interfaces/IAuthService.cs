using ECMS.API.DTOs.User;

namespace ECMS.API.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SignupAsync(SignupDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
    }

}
