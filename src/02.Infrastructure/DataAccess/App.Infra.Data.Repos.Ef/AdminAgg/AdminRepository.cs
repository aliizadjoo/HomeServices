using App.Domain.Core.Contract.AdminAgg.Repository;
using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Entities;
using App.Infra.Db.SqlServer.Ef.DbContextAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.AdminAgg
{
    public class AdminRepository
        (AppDbContext _context , ILogger<AdminRepository> _logger)  : IAdminRepository
    {
        public async Task<AdminPagedResultDto> GetAll(int pageNumber, int pageSize, CancellationToken cancellationToken)
        {
          
            var query = _context.Admins
                .AsNoTracking()
                .AsQueryable();

    
            var totalCount = await query.CountAsync(cancellationToken);

           
            var data = await query.OrderBy(a=>a.Id)
                .Select(a => new AdminListDto
                {
                    AdminId = a.Id,
                    AppUserId = a.AppUserId,
                    FirstName = a.AppUser.FirstName,
                    LastName = a.AppUser.LastName,
                    Email = a.AppUser.Email,
                    StaffCode = a.StaffCode ,
                    TotalRevenue = a.TotalRevenue,
                    CreatedAt = a.CreatedAt,
                })
               .Skip((pageNumber - 1) * pageSize) 
               .Take(pageSize) 
                .ToListAsync(cancellationToken);

            
            _logger.LogInformation("لیست مدیران دریافت شد. تعداد کل: {Total}", totalCount);

            return new AdminPagedResultDto
            {
                Admins = data,
                TotalCount = totalCount
            };


        }


        public async Task<int> Create(CreateAdminDto adminDto, CancellationToken cancellationToken)
        {
            
            var admin = new Admin()
            {
                AppUserId = adminDto.AppUserId,
                StaffCode = adminDto.StaffCode,
            
            };

          
             await _context.Admins.AddAsync(admin, cancellationToken); 

   
           return await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task<AdminProfileDto?> GetByAppUserId(int appUserId, CancellationToken cancellationToken)
        {
            
            return await _context.Admins
                .AsNoTracking()
                .Where(a => a.AppUserId == appUserId ) 
                .Select(a => new AdminProfileDto
                {
                    AppUserId = a.AppUserId,
                    FirstName = a.AppUser.FirstName,
                    LastName = a.AppUser.LastName,
                    Email = a.AppUser.Email,
                    ImagePath = a.AppUser.ImagePath,
                    StaffCode = a.StaffCode,
                    TotalRevenue = a.TotalRevenue
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> Update(AdminProfileDto adminProfileDto, CancellationToken cancellationToken)
        {
        
            var adminRows = await _context.Admins
                .Where(a => a.AppUserId == adminProfileDto.AppUserId )
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(a => a.StaffCode, adminProfileDto.StaffCode),
                    cancellationToken);

            var userRows = await _context.Users
                .Where(u => u.Id == adminProfileDto.AppUserId)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(u => u.FirstName, adminProfileDto.FirstName)
                    .SetProperty(u => u.LastName, adminProfileDto.LastName)
                    .SetProperty(u => u.ImagePath, adminProfileDto.ImagePath),
                    cancellationToken);

         
            return adminRows > 0 || userRows > 0;
        }


        public async Task<bool> Delete(int appUserId, CancellationToken cancellationToken)
        {
            
            var adminRows = await _context.Admins
                .Where(a => a.AppUserId == appUserId)
                .ExecuteUpdateAsync(s => s.SetProperty(a => a.IsDeleted, true), cancellationToken);

           
            var userRows = await _context.Users
                .Where(u => u.Id == appUserId)
                .ExecuteUpdateAsync(s => s.SetProperty(u => u.IsDeleted, true), cancellationToken);

            return adminRows > 0 && userRows > 0;
        }

    }
}
