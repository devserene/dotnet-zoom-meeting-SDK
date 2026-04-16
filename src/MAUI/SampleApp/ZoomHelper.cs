using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SampleApp
{
    public class ZoomHelper
    {
        public static string GenerateJwtToken(string sdkKey, string sdkSecret)
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(sdkSecret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var exp = now + 10 * 60 * 60;

            var payload = new JwtPayload();
            payload.Add("appKey", sdkKey);
            payload.Add("iat", now);
            payload.Add("exp", exp);
            payload.Add("tokenExp", exp);

            var header = new JwtHeader(signingCredentials);
            var securityToken = new JwtSecurityToken(header, payload);

            return handler.WriteToken(securityToken);
        }

        public static string GenerateZoomSignature(string sdkKey, string sdkSecret, string meetingNumber, int role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(sdkSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            var payload = new JwtPayload
    {
        { "sdkKey", sdkKey },
        { "mn", meetingNumber },
        { "role", role },
        { "iat", now },
        { "exp", now + 7200 },
        { "appKey", sdkKey },
        { "tokenExp", now + 7200 }
    };

            var token = new JwtSecurityToken(new JwtHeader(credentials), payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
