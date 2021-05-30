using System;

namespace AppServiceDemo.Data.Contracts
{
    public class AuthenticationResponseDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string Token { get; set; }
    }
}
