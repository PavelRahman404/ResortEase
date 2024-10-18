using Microsoft.EntityFrameworkCore;
using ResortEase.Application.Common.Interfaces;
using ResortEase.Domain.Entities;
using ResortEase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ResortEase.Infrastructure.Repository
{
    public class VillaRepository : Repository<Villa> ,IVillaRepository
    {
        private readonly ApplicationDbContext _context;

        public VillaRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public void Update(Villa entity)
        {
            _context.Update(entity);
        }
    }
}
