using MediatR;
using System;
using System.Collections.Generic;
using HotelBookingSystemAPI.Domain.Entities;

namespace HotelBookingSystemAPI.Application.Hotel.Queries.GetAvailableHotels
{
    public sealed record GetAvailableHotelsQuery(
        string Location,
        DateTime CheckIn,
        DateTime CheckOut,
        decimal? MaxPrice, // Changed from MinPrice/MaxPrice
        HotelSortBy SortBy
    ) : IRequest<IReadOnlyList<HotelSearchResultDto>>;

    public sealed record HotelSearchResultDto(
        int HotelId,
        string Name,
        string? Location,
        decimal? Price,
        int? Rating
    );
}