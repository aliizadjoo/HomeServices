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
        public async Task<bool> ChangeProfileCustomer(int appuserId, ProfileCustomerDto profileCustomerDto,  CancellationToken cancellationToken)
        {
            int customerRows;

            
                customerRows = await _context.Customers
                   .Where(c => c.AppUserId == appuserId ) 
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(c => c.Address, profileCustomerDto.Address)
                        .SetProperty(c => c.CityId, profileCustomerDto.CityId)
                        .SetProperty(c => c.WalletBalance, profileCustomerDto.WalletBalance),
                    
                        cancellationToken);
            
         

         
            var userRows = await _context.Users
                .Where(u => u.Id == appuserId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.FirstName, profileCustomerDto.FirstName)
                    .SetProperty(u => u.LastName, profileCustomerDto.LastName)
                    .SetProperty(u => u.ImagePath, profileCustomerDto.ImagePath)
                    .SetProperty(u=>u.PhoneNumber,profileCustomerDto.PhoneNumber)
                    ,
                    cancellationToken);

            return customerRows > 0 || userRows > 0;
        }

        public async Task<ProfileCustomerDto?> GetProfileCustomer(int appuserId, CancellationToken cancellationToken)
        {
            _logger.LogInformation("تلاش برای دریافت پروفایل مشتری با شناسه: {CustomerId}", appuserId);


            return await _context.Customers.Where(c => c.AppUserId == appuserId)
                .Select(c => new ProfileCustomerDto
                {
                    AppUserId = appuserId,
                    WalletBalance = c.WalletBalance,
                    FirstName = c.AppUser.FirstName,
                    LastName = c.AppUser.LastName,
                    Email = c.AppUser.Email,
                    ImagePath = c.AppUser.ImagePath,
                    Address = c.Address,
                    CityName = c.City.Name,
                    CityId = c.CityId,
                    PhoneNumber = c.AppUser.PhoneNumber,


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

        public async Task<CustomerPagedResultDto> GetAll( int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
            var query = _context.Customers
                           .AsNoTracking()
                           .Where(c => !c.IsDeleted) 
                           .AsQueryable();
                          
            
            var totalCount = await query.CountAsync(cancellationToken);

           
            var data = await query.OrderBy(c=>c.Id)
                .Select(c => new CustomerListDto
                {
                    CustomerId = c.Id,
                    AppUserId = c.AppUserId,
                    FirstName = c.AppUser.FirstName,
                    LastName = c.AppUser.LastName,
                    Email = c.AppUser.Email,
                    CityName = c.City.Name,
                    WalletBalance = c.WalletBalance,
                    CreatedAt = c.CreatedAt,
                    PhoneNumber = c.AppUser.PhoneNumber
                })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            
            return new CustomerPagedResultDto
            {
                Customers = data,
                TotalCount = totalCount
            };


        }


        public async Task<bool> Delete(int appUserId,  CancellationToken cancellationToken)
        {
            
            var expertRows = await _context.Customers
                .Where(e => e.AppUserId == appUserId)
                .ExecuteUpdateAsync(s => s.SetProperty(e => e.IsDeleted, true), cancellationToken);

       

            return expertRows > 0 ;
        }
        public async Task<int> GetIdByAppUserId(int appUserId, CancellationToken cancellationToken)
        {
            return await _context.Customers
                .Where(c => c.AppUserId == appUserId)
                .Select(c => c.Id) 
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}
