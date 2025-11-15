using MediatR;
using System;
using System.Collections.Generic;

namespace HotelBookingSystemAPI.Application.Hotel.Queries.GetAvailableHotels
{
    public sealed record GetAvailableHotelsQuery(
        string Location,
        DateTime CheckIn,
        DateTime CheckOut,
        decimal? MinPrice,
        decimal? MaxPrice,
        int? MinRating,
        int? MaxRating
    ) : IRequest<IReadOnlyList<HotelSearchResultDto>>;

    public sealed record HotelSearchResultDto(
        int HotelId,
        string Name,
        string? Location,
        decimal? Price,
        int? Rating
    );
}