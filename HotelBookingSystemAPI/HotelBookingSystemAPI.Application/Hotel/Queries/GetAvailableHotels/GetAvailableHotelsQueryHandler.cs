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

            // Load hotels with rooms, bookings and reviews
            var hotels = await _context.Hotels
                .AsNoTracking()
                .Include(h => h.Rooms)
                    .ThenInclude(r => r.Bookings)
                .Include(h => h.Reviews)
                .Where(h =>
                    (h.Address != null && EF.Functions.Like(h.Address.ToLower(), $"%{location}%")) ||
                    (h.City != null && EF.Functions.Like(h.City.ToLower(), $"%{location}%")) ||
                    (h.Country != null && EF.Functions.Like(h.Country.ToLower(), $"%{location}%"))
                )
                .ToListAsync(cancellationToken);

            var results = new List<HotelSearchResultDto>();

            foreach (var hotel in hotels)
            {
                // find rooms available in requested date range
                var availableRooms = hotel.Rooms
                    .Where(r => r.Status == RoomStatus.Available)
                    .Where(r =>
                        !r.Bookings.Any(b =>
                            b.Status != BookingStatus.Cancelled &&
                            b.CheckInDate < request.CheckOut &&
                            b.CheckOutDate > request.CheckIn
                        )
                    );

                // apply price filters if provided
                if (request.MinPrice.HasValue)
                    availableRooms = availableRooms.Where(r => r.PricePerNight >= request.MinPrice.Value);
                if (request.MaxPrice.HasValue)
                    availableRooms = availableRooms.Where(r => r.PricePerNight <= request.MaxPrice.Value);

                var roomList = availableRooms.ToList();
                if (!roomList.Any()) continue;

                var minPrice = roomList.Min(r => r.PricePerNight);

                // compute average rating if any reviews exist
                int? avgRating = null;
                if (hotel.Reviews != null && hotel.Reviews.Any())
                {
                    var average = hotel.Reviews.Average(rv => rv.Rating);
                    avgRating = (int)System.Math.Round(average);
                }

                // apply rating filters if provided
                if (request.MinRating.HasValue && (avgRating == null || avgRating < request.MinRating.Value)) continue;
                if (request.MaxRating.HasValue && (avgRating != null && avgRating > request.MaxRating.Value)) continue;

                var locationDisplay = string.Join(", ", new[] { hotel.Address, hotel.City, hotel.Country }
                    .Where(s => !string.IsNullOrWhiteSpace(s)));

                results.Add(new HotelSearchResultDto(
                    HotelId: hotel.Id,
                    Name: hotel.Name,
                    Location: string.IsNullOrWhiteSpace(locationDisplay) ? null : locationDisplay,
                    Price: minPrice,
                    Rating: avgRating
                ));
            }

            return results;
        }
    }
}