using AutoMapper;
using SolarPowerPlant.API.Errors;
using SolarPowerPlant.API.Requests.Account;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Interfaces;
using SolarPowerPlant.Core.Responses.Account;

namespace SolarPowerPlant.API.Services
{
    public class AccountService
    {
        private readonly IDataProtection _dataProtection;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService(IUnitOfWork unitOfWork, ITokenService tokenService, IDataProtection dataProtection, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _dataProtection = dataProtection;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<SignupUserResponse> SignUserAccount(SignUserAccountRequest request)
        {
            try
            {
                var user = _mapper.Map<SignUserAccountRequest, User>(request);
                user.Salt = _dataProtection.GenerateSalt();
                user.Password = _dataProtection.Hash(request.Password, user.Salt);
                _unitOfWork.Repository<Core.Entities.User>()?.Add(user);
                await _unitOfWork.Complete();

                return new SignupUserResponse()
                {
                    UserId = user.Id,
                    Email = user.Email!,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var user = await _unitOfWork.Repository<Core.Entities.User>()!.GetFirstAsync(x => x.Email == request.Email, x => x);
                if (user == null) throw new UnauthorizedException("401", "This user doesn't exist.");
                var hashedPassword = _dataProtection.Hash(request.Password, user.Salt!);
                if (user.Password != hashedPassword) throw new UnauthorizedException("401", "Wrong password, try again.");
                var refreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddYears(1);
                _unitOfWork.Repository<Core.Entities.User>()!.Update(user!);
                await _unitOfWork.Complete();
                
                return new LoginResponse
                {
                    FirstName = user.FirstName!,
                    LastName = user.LastName!,
                    Token = _tokenService.CreateToken(user.Id, user.Email!),
                    UserId = user.Id,
                    RefreshToken = refreshToken
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<LoginResponse> RefreshToken(RefreshTokenRequest request)
        {
            try
            {
                string refreshToken = request.RefreshToken!;
                string token = _httpContextAccessor.HttpContext!.Request.Headers["Authorization"][0]!;
                var userId = _tokenService.ReadUserId(token);
                var user = await _unitOfWork.Repository<Core.Entities.User>()!.GetFirstAsync(x => x.Id == userId, x => x);
                if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                    throw new NotFoundException("404", "Refresh token is not valid.");
                var newAccessToken = _tokenService.SecurityToken(_tokenService.ReadToken(token));
                var newRefreshToken = _tokenService.GenerateRefreshToken();
                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddHours(8);
                _unitOfWork.Repository<Core.Entities.User>()!.Update(user!);
                await _unitOfWork.Complete();

                return new LoginResponse()
                {
                    Token = newAccessToken,
                    RefreshToken = newRefreshToken
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
    }
}
