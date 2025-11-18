using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HotelBookingSystemAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using HotelBookingSystemAPI.Domain.Entities;

namespace HotelBookingSystemAPI.Application.Hotel.Queries.GetAvailableHotels
{
    public class GetAvailableHotelsQueryHandler : IRequestHandler<GetAvailableHotelsQuery, IReadOnlyList<HotelSearchResultDto>>
    {
        private readonly HotelBookingDbContext _context;

        public GetAvailableHotelsQueryHandler(HotelBookingDbContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<HotelSearchResultDto>> Handle(GetAvailableHotelsQuery request, CancellationToken cancellationToken)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(request.Location) || request.CheckIn >= request.CheckOut)
            {
                return new List<HotelSearchResultDto>();
            }

            var location = request.Location.Trim().ToLower();

            var hotelsQuery = _context.Hotels
                .AsNoTracking()
                .Where(h =>
                    (h.Address != null && EF.Functions.Like(h.Address.ToLower(), $"%{location}%")) ||
                    (h.City != null && EF.Functions.Like(h.City.ToLower(), $"%{location}%")) ||
                    (h.Country != null && EF.Functions.Like(h.Country.ToLower(), $"%{location}%"))
                );

            var hotels = await hotelsQuery.Select(h => new
            {
                Hotel = h,
                Reviews = h.Reviews,
                AvailableRooms = h.Rooms.Where(r =>
                    r.Status == RoomStatus.Available &&
                    (!request.MaxPrice.HasValue || r.PricePerNight <= request.MaxPrice.Value) &&
                    !r.Bookings.Any(b =>
                        b.Status != BookingStatus.Cancelled &&
                        b.CheckInDate < request.CheckOut &&
                        b.CheckOutDate > request.CheckIn
                    )
                )
            })
            .Where(h => h.AvailableRooms.Any())
            .ToListAsync(cancellationToken);

            var results = hotels.Select(h => new HotelSearchResultDto(
                HotelId: h.Hotel.Id,
                Name: h.Hotel.Name,
                Location: string.Join(", ", new[] { h.Hotel.Address, h.Hotel.City, h.Hotel.Country }.Where(s => !string.IsNullOrWhiteSpace(s))),
                Price: h.AvailableRooms.Min(r => r.PricePerNight),
                Rating: h.Reviews.Any() ? (int?)System.Math.Round(h.Reviews.Average(rv => rv.Rating)) : null
            )).ToList();

            // Apply sorting
            switch (request.SortBy)
            {
                case HotelSortBy.Rating:
                    results = results.OrderByDescending(r => r.Rating).ThenBy(r => r.Price).ToList();
                    break;
                case HotelSortBy.PriceHighToLow:
                    results = results.OrderByDescending(r => r.Price).ToList();
                    break;
                case HotelSortBy.PriceLowToHigh:
                    results = results.OrderBy(r => r.Price).ToList();
                    break;
                case HotelSortBy.Default:
                default:
                    results = results.OrderBy(r => r.Name).ToList();
                    break;
            }

            return results;
        }
    }
}