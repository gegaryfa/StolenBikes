using System.Collections.Generic;

using StolenBikes.Core.Domain.Entities;

namespace StolenBikes.Infrastructure.Shared.Services.StolenBikes.Clients
{
    public class StolenBikesApiResponse
    {
        public StolenBikesApiResponse()
        {
            Paging = new Paging();
        }

        public List<StolenBikeIncident> Incidents { get; set; }

        public Paging Paging { get; set; }
    }

    public class Paging
    {
        public int TotalObjects { get; set; }
    }
}