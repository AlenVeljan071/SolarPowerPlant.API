using AutoMapper;
using SolarPowerPlant.API.Errors;
using SolarPowerPlant.Core.Entities;
using SolarPowerPlant.Core.Enums;
using SolarPowerPlant.Core.Helpers;
using SolarPowerPlant.Core.Interfaces;
using SolarPowerPlant.Core.Interfaces.Repositories;
using SolarPowerPlant.Core.Responses;
using SolarPowerPlant.Core.Responses.TimeSeries;
using SolarPowerPlant.Core.Specifications.TimeSeriesSpecification;

namespace SolarPowerPlant.API.Services
{
    public class TimeSeriesDataService : ITimeSeriesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IReadGenericRepository<Core.Entities.TimeseriesProduction> _readGenericRepository;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TimeSeriesDataService(IUnitOfWork unitOfWork, IReadGenericRepository<TimeseriesProduction> readGenericRepository, IMapper mapper, ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _readGenericRepository = readGenericRepository;
            _mapper = mapper;
            _tokenService = tokenService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<CreateResponse> CreateRealProductionAsync(int id, int power)
        {
            try
            {
                var production = new TimeseriesProduction
                {
                    SolarPowerPlantId = id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Production = Random.Shared.Next(0, power),
                    ProductionTime = DateTime.Now,
                    TimeSerieType = Core.Enums.TimeSerieType.Real,
                };
                _unitOfWork.Repository<TimeseriesProduction>()?.Add(production);
                await _unitOfWork.Complete();
                
                return new CreateResponse()
                {
                    Id = production.Id,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<CreateResponse> CreateForecastProductionAsync(int id, int power)
        {
            try
            {
                var production = new TimeseriesProduction
                {
                    SolarPowerPlantId = id,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    ProductionTime = DateTime.Now,
                    TimeSerieType = Core.Enums.TimeSerieType.Forecasted,
                };
                var temperature = Random.Shared.Next(-20, 55);

                if (temperature <= 20 && temperature >= -15)
                {
                    production.Production = Random.Shared.Next((int)(0.5 * power), (int)(0.7 * power));
                }
                else if (temperature > 52 || temperature < -15)
                {
                    production.Production = Random.Shared.Next(0, (int)(0.3 * power));
                }
                else
                {
                    production.Production = Random.Shared.Next((int)(0.8 * power), power);
                }
                _unitOfWork.Repository<TimeseriesProduction>()?.Add(production);
                await _unitOfWork.Complete();

                return new CreateResponse()
                {
                    Id = production.Id,
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<TimeSeriesListResponse> GetTimeSeriesListAsync(TimeSeriesSpecParams request)
        {
            try
            {
               
                request.UserId = _tokenService.ReadUserId(_httpContextAccessor.HttpContext!.Request.Headers["Authorization"][0]!);
                if (request.UserId == 0) throw new UnauthorizedException("401", "This user doesn't exist.");
                var totalItems = await _readGenericRepository.CountAsync(new TimeSeriesSpecification(request));
                var timeSeries = await _readGenericRepository.ListAsync(new TimeSeriesSpecification(request));
                var timeSeriesResponse = _mapper.Map<IReadOnlyList<TimeSeriesResponse>>(timeSeries);

                if (request.Granularity == Core.Enums.Granularity.Hour)
                {
                    var hourlyProductions = timeSeriesResponse
                    .GroupBy(ts => new { Hour = new DateTime(ts.ProductionTime.Year, ts.ProductionTime.Month, ts.ProductionTime.Day, ts.ProductionTime.Hour, 0, 0), ts.TimeSerieType, ts.SolarPowerPlantId })
                    .Select(group => new TimeSeriesResponse
                    {
                        ProductionTime = group.Key.Hour,
                        TimeSerieType = group.Key.TimeSerieType,
                        Granularity = EnumExtensions.GetValues<Granularity>().First(c=>c.Id == (int)Granularity.Hour),
                        SolarPowerPlantId = group.Key.SolarPowerPlantId,
                        Production = group.Sum(ts => ts.Production)
                    })
                    .ToList();
                    totalItems = hourlyProductions.Count();

                    return new TimeSeriesListResponse
                    {
                        Pagination = new Pagination<TimeSeriesResponse>(request.PageIndex, request.PageSize, totalItems, hourlyProductions),
                    };
                }
                else
                {
                    foreach (var item in timeSeriesResponse)
                    {
                        item.Granularity = EnumExtensions.GetValues<Granularity>().First(x => x.Id == (int)Granularity.Minutes);
                    }
                    return new TimeSeriesListResponse
                    {
                        Pagination = new Pagination<TimeSeriesResponse>(request.PageIndex, request.PageSize, totalItems, timeSeriesResponse),
                    };
                }
             
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

   
}
