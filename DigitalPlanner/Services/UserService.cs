using System.Security.Authentication;
using System.Security.Claims;
using DigitalPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace DigitalPlanner.Services;

public class UserService
{
    private DBContext db;

    public UserService(DBContext db)
    {
        this.db = db;
    }

    public async Task<Guid> GetCurrentUserId(ClaimsPrincipal userClaims)
    {
        var userName = userClaims.Identity?.Name;
        var user = await db.Users.FirstOrDefaultAsync(u => u.Username == userName);

        if (user != null)
        {
            return user.Id;
        }

        throw new AuthenticationException();
    }
}