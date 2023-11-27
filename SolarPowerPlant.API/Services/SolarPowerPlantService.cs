using AutoMapper;
using SolarPowerPlant.API.Errors;
using SolarPowerPlant.API.Requests.PowerPlant;
using SolarPowerPlant.Core.Helpers;
using SolarPowerPlant.Core.Interfaces;
using SolarPowerPlant.Core.Interfaces.Repositories;
using SolarPowerPlant.Core.Responses;
using SolarPowerPlant.Core.Responses.PowerPlant;
using SolarPowerPlant.Core.Specifications.SolarPowerPlantSpecification;

namespace SolarPowerPlant.API.Services
{
    public class SolarPowerPlantService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IReadGenericRepository<Core.Entities.SolarPowerPlant> _readGenericRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITokenService _tokenService;
        public SolarPowerPlantService(IUnitOfWork unitOfWork, IMapper mapper, IReadGenericRepository<Core.Entities.SolarPowerPlant> readGenericRepository, IHttpContextAccessor httpContextAccessor, ITokenService tokenService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _readGenericRepository = readGenericRepository;
            _httpContextAccessor = httpContextAccessor;
            _tokenService = tokenService;
        }

        public async Task<CreateResponse> CreateSolarPowerPlant(SolarPowerPlantRequest request)
        {
            try
            {
                var userId = _tokenService.ReadUserId(_httpContextAccessor.HttpContext!.Request.Headers["Authorization"][0]!);
                if (userId == 0) throw new UnauthorizedException("401", "This user doesn't exist.");
                var solarPowerPlant = _mapper.Map<SolarPowerPlantRequest, Core.Entities.SolarPowerPlant>(request);
                solarPowerPlant.UserId = userId;    
                _unitOfWork.Repository<Core.Entities.SolarPowerPlant>()?.Add(solarPowerPlant);
                await _unitOfWork.Complete();

                return new CreateResponse()
                {
                    Id = solarPowerPlant.Id,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CreateResponse> UpdateSolarPowerPlant(UpdatePowerPlantRequest request)
        {
            try
            {
                var solarPowerPlant = await _unitOfWork.Repository<Core.Entities.SolarPowerPlant>()!.GetByIdAsync(request.Id);
                if (solarPowerPlant == null) throw new BadRequestException("400", "Solar power plant with this id doesn't exist.");
                _mapper.Map<UpdatePowerPlantRequest, Core.Entities.SolarPowerPlant>(request, solarPowerPlant!);
                _unitOfWork.Repository<Core.Entities.SolarPowerPlant>()?.Update(solarPowerPlant!);
                await _unitOfWork.Complete();

                return new CreateResponse()
                {
                    Id = solarPowerPlant!.Id,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> DeleteSolarPowerPlantById(int id)
        {
            try
            {
                var solarPowerPlant = await _unitOfWork.Repository<Core.Entities.SolarPowerPlant>()!.GetByIdAsync(id);
                if (solarPowerPlant == null) throw new BadRequestException("400", "Solar power plant with this id doesn't exist.");
                _unitOfWork.Repository<Core.Entities.SolarPowerPlant>()!.Delete(solarPowerPlant!);
                await _unitOfWork.Complete();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SolarPowerPlantResponse> GetSolarPowerPlantById(int id)
        {
            try
            {
                var userId = _tokenService.ReadUserId(_httpContextAccessor.HttpContext!.Request.Headers["Authorization"][0]!);
                if (userId == 0) throw new UnauthorizedException("401", "This user doesn't exist.");
                var solarPowerPlant = await _readGenericRepository.GetEntityWithSpec(new SolarPowerPlantSpecification(id, userId));
                if (solarPowerPlant == null) throw new BadRequestException("400", "Solar power plant with this id doesn't exist.");
                return _mapper.Map<Core.Entities.SolarPowerPlant, SolarPowerPlantResponse>(solarPowerPlant!);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<SolarPowerPlantListResponse> GetSolarPowerPlantList(SolarPowerPlantSpecParams request)
        {
            try
            {
                request.UserId = _tokenService.ReadUserId(_httpContextAccessor.HttpContext!.Request.Headers["Authorization"][0]!);
                if (request.UserId == 0) throw new UnauthorizedException("401", "This user doesn't exist.");
                var totalItems = await _readGenericRepository.CountAsync(new SolarPowerPlantSpecification(request));
                var solarPowerPlants = await _readGenericRepository.ListAsync(new SolarPowerPlantSpecification(request));
                var solarPowerPlantsResponse = _mapper.Map<IReadOnlyList<SolarPowerPlantResponse>>(solarPowerPlants);

                return new SolarPowerPlantListResponse
                {
                    Pagination = new Pagination<SolarPowerPlantResponse>(request.PageIndex, request.PageSize, totalItems, solarPowerPlantsResponse),
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
