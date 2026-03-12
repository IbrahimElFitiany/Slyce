using Microsoft.EntityFrameworkCore;
using Shared.Domain.ValueObjects;
using Subscriptions.Domain.Interfaces;
using Subscriptions.Infrastructure.Persistence;

namespace Subscriptions.Infrastructure
{
    public class PostGISDistanceCalculator : IDistanceCalculator
    {
        private readonly SubscriptionsDbContext _context;
        private const double RadiusInMeters = 6000;

        public PostGISDistanceCalculator(SubscriptionsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> LocationWithinLocationRadius(
            Coordinates locationA,
            Coordinates locationB)
        {
            var result = await _context.Database
                .SqlQuery<bool>(
                    $"""
                     SELECT ST_DWithin(
                         ST_MakePoint({locationA.Longitude}, {locationA.Latitude})::geography,
                         ST_MakePoint({locationB.Longitude}, {locationB.Latitude})::geography,
                         {RadiusInMeters}
                     ) AS "Value"
                     """)
                .FirstAsync();

            return result;
        }
    }
}