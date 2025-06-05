using DataAccessLayer.Models;

namespace DataAccessLayer.Interfaces
{
    public interface IPartRepository
    {
        public IEnumerable<Part> GetAllParts();

        public Part? GetPartById(int id);

        public void AddPart(Part part);

        public void UpdatePart(Part part);

        public void DeletePart(Part part);
    }
}
