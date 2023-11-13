using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodePulse.API.Models.Domain;

namespace CodePulse.API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        Task<Category> CreateAsync(Category category);
        Task<IEnumerable<Category>> GetAllAsync ();
    }
}