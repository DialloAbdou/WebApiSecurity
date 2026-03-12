
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography;

var rsa = RSA.Create();
if(!File.Exists( "key.bin"))
{
    var key = rsa.ExportRSAPrivateKey();
    File.WriteAllBytes("key.bin", key);
}
rsa.ImportRSAPrivateKey(File.ReadAllBytes("key.bin"), out var _);


var builder = WebApplication.CreateBuilder();
var app = builder.Build();


app.MapGet("/username", (HttpContext ctx) =>
{
    var username = ctx.Request.Cookies.FirstOrDefault(c => c.Key == "username").Value;
    if (string.IsNullOrEmpty(username))
    {
        return Results.NotFound("Vous devez vous loguer");
    }
    return Results.Ok($"Bonjour {username}");
});

app.MapGet("/login", (HttpContext ctx) =>
{

    var jwtHandler = new JsonWebTokenHandler();
    var key = new RsaSecurityKey(rsa);
    var token = jwtHandler.CreateToken(new SecurityTokenDescriptor
    {
         Subject = new ClaimsIdentity(new[]
         {
             new Claim("Sub","1"),
             new Claim("name", "Abdou Diallo"),
             
         }),
         Issuer = "http://localhost:54217",
         SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256),

    });
     
    return Results.Ok(token);
    //ctx.Response.Headers["set-cookie"] = "username=abdou";
    //return Results.Ok("Bienvenu sur votre Compte");

});

app.Run();

