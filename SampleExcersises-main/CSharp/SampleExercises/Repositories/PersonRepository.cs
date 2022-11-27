using SampleExercises.Models;
using System.Linq;

namespace SampleExercises.Repositories
{
    public class PersonRepository
    {
        private List<Person> _personData;

        public PersonRepository(List<Person> personData)
        {
            _personData = personData;
        }

        public bool IsEmpty()
        {
            return _personData.Count == 0;
        }

        public int Count()
        {
            return _personData.Count;
        }

        public int EntitiesWithAssociations()
        {
            return _personData.Where(x => x.Associations.Count > 0).Count();
        }

        public Person? GetById(string entityId)
        {
            return _personData.FirstOrDefault(x => x.EntityId == entityId);
        }

        public List<Person> FindIdLike(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return _personData;
            }

            return _personData.Where(a => (a.EntityId ?? string.Empty).ToLower().Contains(value)).ToList();
        }

        public List<Person> GetDuplicatedFirstMiddleNames() 
        {
            return _personData.Where(x => (x.FirstName ?? string.Empty).ToLower() == (x.MiddleName ?? string.Empty).ToLower()).ToList();
        }
    }
}
