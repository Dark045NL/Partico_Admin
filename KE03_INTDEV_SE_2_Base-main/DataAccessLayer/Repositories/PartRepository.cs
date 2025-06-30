using DataAccessLayer.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Repositories
{
    public class PartRepository(MatrixIncDbContext context) : IPartRepository
    {
        private readonly MatrixIncDbContext _context = context;

        public void AddPart(Part part)
        {
            _context.Parts.Add(part);
            _context.SaveChanges();
        }

        public void DeletePart(Part part)
        {
            _context.Parts.Remove(part);
            _context.SaveChanges();
        }

        public IEnumerable<Part> GetAllParts()
        {
            return _context.Parts;
        }

        public Part? GetPartById(int id)
        {
            return _context.Parts.FirstOrDefault(p => p.Id == id);
        }

        public void UpdatePart(Part part)
        {
            _context.Parts.Update(part);
            _context.SaveChanges();
        }
    }
}
