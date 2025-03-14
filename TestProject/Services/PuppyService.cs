using TestProject.Models;
using TestProject.Repositories;

namespace TestProject.Services
{
    public class PuppyService
    {
        private readonly PuppyRepository _repo;

        public PuppyService(PuppyRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Puppy> GetAllPuppies()
        {
            return _repo.GetAllPuppies();
        }

        public Puppy GetById(int puppyId)
        {
            Puppy foundPuppy;
            try
            {
                foundPuppy = _repo.GetById(puppyId);
            }
            catch
            {
                throw new Exception("Invalid ID");
            }

            return foundPuppy;
        }

        internal Puppy CreatePuppy(Puppy newPuppy)
        {
            return _repo.CreatePuppy(newPuppy);
        }

        internal Puppy EditPuppy(Puppy puppyToUpdate, int newID)
        {
            Puppy foundPuppy = GetById(newID);

            _repo.EditPuppy(puppyToUpdate, newID);

            return puppyToUpdate;
        }

        internal string DeletePuppy(int puppyID)
        {
            Puppy foundPuppy = GetById(puppyID);

            _repo.DeletePuppy(foundPuppy.puppyid);

            return "Successfully Delete Puppy";
        }
    }
}
