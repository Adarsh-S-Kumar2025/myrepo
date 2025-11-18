namespace HotelBookingSystemAPI.Application.Auth
{
    public interface ITokenService
    {
        string CreateToken(Domain.Entities.Employee employee);
    }
}