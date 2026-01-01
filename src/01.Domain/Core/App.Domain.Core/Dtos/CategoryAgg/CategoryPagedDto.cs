using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.Dtos.CategoryAgg
{
    public class CategoryPagedDto
    {
        public List<CategoryDto> CategoryDtos { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
