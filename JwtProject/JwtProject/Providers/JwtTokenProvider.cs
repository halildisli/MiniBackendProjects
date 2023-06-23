using JwtProject.Models;
using JwtProject.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JwtProject.Providers
{
    public class JwtTokenProvider : IJwtTokenProvider
    {
        private readonly JwtOptions _jwtOptions;
        public JwtTokenProvider(IOptions<JwtOptions> options)
        {
            ///IOptions,IOptionsMonitor,IOptionsSnapshot farkları;
            ///IOptions singleton gibi çalışır ve siz o verileri program çalıştığı anda bir defa elde edersiniz ve program süresi boyunca aynı nesneyi kullanarak işlem yapılır.
            ///IOptionsMonitor, Transient gibi çalışır. Nesne her talep edildiğinde yeniden kontrol ediler ve oluşturulur.
            ///IOptionsSnapshot, Scoped gibi çalışır. Her request ve response arasında bir defa talep edilir ve oluşturulur.
            _jwtOptions = options.Value;
        }
        public string GenerateToken(AppUser user)
        {
            var claims = new Claim[]
            {
                //Gönderilecek token içerisinde yer alacak bilgilerin belirlendiği bölümdür.

                new Claim(ClaimTypes.NameIdentifier,user.Id),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim(JwtRegisteredClaimNames.Email,user.Email),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FirstName),
                new Claim(JwtRegisteredClaimNames.FamilyName,user.LastName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            };
            var encodedKey = Encoding.UTF8.GetBytes(_jwtOptions.Secret); //Encoded key oluşturuluyor.
            var signInCredentials = new SigningCredentials(new SymmetricSecurityKey(encodedKey), SecurityAlgorithms.HmacSha256); //SignIn kimlik bilgileri belirleniyor.

            var token = new JwtSecurityToken(issuer: _jwtOptions.Issuer, audience: _jwtOptions.Audience, claims: claims, expires: DateTime.Now.Add(_jwtOptions.ExpiredTime), signingCredentials: signInCredentials); //Token oluşturuluyor.

            return new JwtSecurityTokenHandler().WriteToken(token); //Oluşturulan token yazdırılarak geri döndürülüyor.
        }
    }
}
