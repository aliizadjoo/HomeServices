using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.AccountAgg
{
    public class UserLoginDto
    {

        [Required(ErrorMessage = "وارد کردن ایمیل الزامی است")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "وارد کردن رمز عبور الزامی است")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
