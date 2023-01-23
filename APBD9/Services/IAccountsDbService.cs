using APBD9.DTOs;

namespace APBD9.Services
{
    public interface IAccountsDbService
    {
        public Task AddUser(LoginRequest request);
        public Task<String> Login(LoginRequest loginRequest);
        public Task<String> refreshToken(string token);
    }
}
