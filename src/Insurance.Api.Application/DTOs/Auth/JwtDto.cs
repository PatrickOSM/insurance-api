using System;

namespace Insurance.Api.Application.DTOs.Auth
{
    public class JwtDto
    {
        public string Token { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
