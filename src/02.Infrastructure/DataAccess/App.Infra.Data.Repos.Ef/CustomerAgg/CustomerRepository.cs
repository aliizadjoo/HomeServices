using App.Domain.Core.Contract.CustomerAgg.Repository;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.CustomerAgg
{
    public class CustomerRepository
        (AppDbContext _context ,ILogger<CustomerRepository> _logger) 
        : ICustomerRepository
    {
        public async Task<bool> ChangeProfileCustomer(int appuserId, ProfileCustomerDto profileCustomerDto, CancellationToken cancellationToken)
        {
          
            var customerRows = await _context.Customers
                .Where(c => c.AppUserId == appuserId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(c => c.Address, profileCustomerDto.Address)
                    .SetProperty(c => c.CityId, profileCustomerDto.CityId),
                    cancellationToken);

       
            var userRows = await _context.Users
                .Where(u => u.CustomerProfile!.AppUserId == appuserId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.FirstName, profileCustomerDto.FirstName)
                    .SetProperty(u => u.LastName, profileCustomerDto.LastName)
                    .SetProperty(u => u.ImagePath, profileCustomerDto.ImagePath),
                    cancellationToken);

            return customerRows > 0 || userRows > 0;
        }

        public async Task<ProfileCustomerDto?> GetProfileCustomer(int appuserId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("تلاش برای دریافت پروفایل مشتری با شناسه: {CustomerId}", appuserId);


            return await _context.Customers.Where(c => c.AppUserId == appuserId)
                .Select(c => new ProfileCustomerDto
                {
                    FirstName = c.AppUser.FirstName,
                    LastName = c.AppUser.LastName,
                    ImagePath = c.AppUser.ImagePath,
                    Address = c.Address,
                    CityName = c.City.Name

                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> Create(CreateCustomerDto custmoerDto, CancellationToken cancellationToken)
        {
            var customer = new Customer()
            {
                CityId = custmoerDto.CityId,
                AppUserId = custmoerDto.AppUserId,
            };

            await _context.Customers.AddAsync(customer, cancellationToken);
            return await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
