using Leaguelane.Constants.Enums;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


namespace Leaguelane.Api.Configurations
{
    public static class JwtConfigurations
    {
        public static void JwtConfiguration(this WebApplicationBuilder builder)
        {
            var key = Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]);
            var validAudience = builder.Configuration["JWT:ValidAudience"];
            var validIssuer = builder.Configuration["JWT:ValidIssuer"];

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = validAudience,
                    ValidIssuer = validIssuer,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.AddAuthorizationBuilder().AddPolicy(UserRole.Admin.ToString(), policy => policy.RequireRole(UserRole.Admin.ToString()));
            builder.Services.AddAuthorizationBuilder().AddPolicy(UserRole.User.ToString(), policy => policy.RequireRole(UserRole.User.ToString()));
            builder.Services.AddAuthorizationBuilder().AddPolicy(UserRole.Employee.ToString(), policy => policy.RequireRole(UserRole.Employee.ToString()));
        }
    }
}
