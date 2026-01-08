using App.Domain.Core.Dtos.AdminAgg;
using App.Domain.Core.Dtos.CustomerAgg;
using App.Domain.Core.Dtos.ExpertAgg;
using App.Domain.Core.Dtos.UserAgg;
using System.ComponentModel.DataAnnotations;

namespace App.EndPoints.MVC.HomeService.Areas.Admin.Models
{
    public class UserManagementViewModel
    {

        public List<CustomerListDto> Customers { get; set; } = new();
        public int CustomerTotalCount { get; set; }
        public int CustomerPage { get; set; } 

        public List<ExpertListDto> Experts { get; set; } = new();
        public int ExpertTotalCount { get; set; }
        public int ExpertPage { get; set; } 

        public List<AdminListDto> Admins { get; set; } = new();
        public int AdminTotalCount { get; set; }
        public int AdminPage { get; set; } 

        public int PageSize { get; set; }

     


        public string ActiveTab { get; set; } = "customers"; 

        public int GetTotalPages(int totalCount) => (int)Math.Ceiling((double)totalCount / PageSize);


    }
}
