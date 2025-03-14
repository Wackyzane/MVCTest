using Dapper;
using System.Data;
using TestProject.Models;

namespace TestProject.Repositories
{
    public class PuppyRepository
    {
        private IDbConnection _db;

        public PuppyRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Puppy> GetAllPuppies()
        {
            string sql = "SELECT * FROM puppies";
            return _db.Query<Puppy>(sql);
        }

        internal Puppy GetById(int puppyid)
        {
            string sql = "SELECT * FROM puppies WHERE puppyid = @puppyid";
            return _db.QueryFirstOrDefault<Puppy>(sql, new { puppyid });
        }

        internal Puppy CreatePuppy(Puppy newPuppy)
        {
            string sql = $"INSERT INTO puppies (name, gender) VALUES (@name, @gender);";
            var parameters = new { name = newPuppy.name, gender = newPuppy.gender };
            newPuppy.puppyid = _db.ExecuteScalar<int>(sql, parameters);
            return newPuppy;
        }

        internal Puppy EditPuppy(Puppy puppyToUpdate, int puppyid)
        {
            puppyToUpdate.puppyid = puppyid;
            var parameters = new { name = puppyToUpdate.name, gender = puppyToUpdate.gender, puppyid = puppyToUpdate.puppyid };

            string sql = "UPDATE puppies SET name = @name, gender = @gender WHERE puppyid = @puppyid";
            puppyToUpdate.puppyid = _db.Execute(sql, parameters);
            return puppyToUpdate;
        }
        internal string DeletePuppy(int puppyid)
        {
            string sql = "DELETE FROM puppies WHERE puppyid = @puppyid";
            _db.Execute(sql, new { puppyid });
            return "Successfully Deleted Puppy";
        }
    }
}
