using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodePulse.API.Models.DTO
{
    public class UpdateCategoryRequestDTO
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; }
    }
}