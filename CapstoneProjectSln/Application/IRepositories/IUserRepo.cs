using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IRepositories
{
    public interface IUserRepo : IGenericRepo<User>
    {
        Task<bool> CheckEmailAddressExisted(string sEmail);
        Task<User?> GetByEmailAsync(string sEmail);
        Task<User> GetUserByEmailAddressAndPasswordHash(string email, string passwordHash);

        int GetCount();
    }
}
