using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace IdentitySolution.TokenHelper
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes("fiver-secret-key"));
        }
    }
}
