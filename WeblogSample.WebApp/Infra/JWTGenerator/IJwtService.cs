using WeblogSample.Data.Entities;

namespace WeblogSample.WebApp.Infra.JWTGenerator;

public interface IJwtService
{
    string GenerateToken(Person user);
}
