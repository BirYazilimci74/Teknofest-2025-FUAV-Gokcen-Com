using GokcenCom.Domain.Models;
using GokcenCom.Repository.Server;
using GokcenCom.Domain.Interfaces.Server;

namespace GokcenCom.Service.Server
{
    public class LoginService : IPOSTService<LoginRequest>
    {
        private readonly BaseServerRepositoryPOST<LoginRequest> _serverRepository;
        private readonly string _endPoint = "/api/giris";

        public LoginService(BaseServerRepositoryPOST<LoginRequest> serverRepository)
        {
            _serverRepository = serverRepository;
        }

        public async Task<bool> PostAsync(LoginRequest data)
        {
            try
            {
                var result = await _serverRepository.PostAsync(_endPoint,data);
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"POST Login Request to Server Exception: {ex.Message}");
                return false;
            }
            
        }

        public async Task<LoginResponse> Login(LoginRequest loginRequest)
        {
            try
            {
                if (_serverRepository is LoginSR loginSR)
                {
                    var response = await loginSR.Login(_endPoint,loginRequest);
                    return response;
                }
                else
                {
                    return new LoginResponse
                    {
                        response_message = "Invalid operation",
                        takimNumarasi = 0
                    };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Login Exception: {ex.Message}");
                return new LoginResponse{
                    response_message = "Fail",
                    takimNumarasi = 0
                };
            }
            
        }
    }
}