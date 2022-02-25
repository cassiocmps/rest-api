using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace DevIO.Business.Intefaces
{
    // interface usada para interação com o usuario na app 
    public interface IUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        bool IsAuthenticated();
        bool IsInRole(string role);
        IEnumerable<Claim> GetClaimsIdentity();
    }
}