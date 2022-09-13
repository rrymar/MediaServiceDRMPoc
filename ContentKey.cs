using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.IdentityModel.Tokens;

namespace MediaServicesPoc;

public static class ContentKey
{
    private const string KeyString = "LPLSvB5RuzFxpVqFxWsUsCL2D83zLOFlGV0R8rTp1f+xrQTWXO/XAQ==";

    public static string GetToken(string issuer, string audience)
    {
        byte[] tokenVerificationKey  = Convert.FromBase64String(KeyString);;
        
        var tokenSigningKey = new SymmetricSecurityKey(tokenVerificationKey);

        var cred = new SigningCredentials(
            tokenSigningKey,
            // Use the  HmacSha256 and not the HmacSha256Signature option, or the token will not work!
            SecurityAlgorithms.HmacSha256,
            SecurityAlgorithms.Sha256Digest);

        // To set a limit on how many times the same token can be used to request a key or a license.
        // add  the "urn:microsoft:azure:mediaservices:maxuses" claim.
        // For example, claims.Add(new Claim("urn:microsoft:azure:mediaservices:maxuses", 4));

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            notBefore: DateTime.Now.AddMinutes(-5),
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: cred);

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

        return handler.WriteToken(token);
    }

}