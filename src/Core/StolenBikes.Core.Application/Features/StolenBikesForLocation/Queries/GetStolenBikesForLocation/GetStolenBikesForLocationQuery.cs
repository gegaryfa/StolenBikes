using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using EnsureThat;

using MediatR;

using StolenBikes.Core.Application.Interfaces.Services;

namespace StolenBikes.Core.Application.Features.StolenBikesForLocation.Queries.GetStolenBikesForLocation
{
    public class GetStolenBikesForLocationQuery : IRequest<GetStolenBikesForLocationViewModel>
    {
        public string Proximity { get; set; }

        public int ProximitySquare { get; set; }
    }

    public class GetGetStolenBikesForLocationQueryHandler : IRequestHandler<GetStolenBikesForLocationQuery, GetStolenBikesForLocationViewModel>
    {
        private readonly IGetStolenBikesForLocationService _getStolenBikesForLocationService;
        private readonly IMapper _mapper;
        public GetGetStolenBikesForLocationQueryHandler(IGetStolenBikesForLocationService getStolenBikesForLocationService, IMapper mapper)
        {
            EnsureArg.IsNotNull(getStolenBikesForLocationService, nameof(getStolenBikesForLocationService));
            EnsureArg.IsNotNull(mapper, nameof(mapper));

            _getStolenBikesForLocationService = getStolenBikesForLocationService;
            _mapper = mapper;
        }

        public async Task<GetStolenBikesForLocationViewModel> Handle(GetStolenBikesForLocationQuery query, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<GetStolenBikesForLocationParameter>(query);

            var stolenBikesForLocation = await _getStolenBikesForLocationService.GetStolenBikesForLocation(request.Proximity, request.ProximitySquare);

            return _mapper.Map<GetStolenBikesForLocationViewModel>(stolenBikesForLocation);
        }
    }
}
