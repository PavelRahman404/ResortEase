using ResortEase.Application.Common.Interfaces;
using ResortEase.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortEase.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IVillaRepository Villa {  get; private set; }
        public IAmenityRepository Amenity {  get; private set; }
        public IVillaNumberRepository VillaNumber {  get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Villa = new VillaRepository(context);
            Amenity = new AmenityRepository(context);
            VillaNumber = new VillaNumberRepository(context);
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
