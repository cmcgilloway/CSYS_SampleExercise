using SampleExercises.Models;

namespace SampleExercises.Repositories
{
    public class AddressRepository
    {
        private List<Address> _addressData;

        public AddressRepository(List<Address> addressData)
        {
            _addressData = addressData;
        }

        public bool IsEmpty()
        {
            return _addressData.Count == 0;
        }

        public int Count()
        {
            return _addressData.Count;
        }

        public int EntitiesWithAssociations()
        {
            return _addressData.Where(x => x.Associations.Count > 0).Count();
        }

        public Address? GetById(string entityId)
        {
            return _addressData.FirstOrDefault(x => x.EntityId == entityId);
        }

        public Address? GetFullEntity(Address address)
        {
            return _addressData.FirstOrDefault(x => (string.IsNullOrWhiteSpace(address.StreetAddress) || x.StreetAddress == address.StreetAddress)
                && (string.IsNullOrWhiteSpace(address.City) || x.City == address.City)
                && (string.IsNullOrWhiteSpace(address.State) || x.State == address.State)
                && (string.IsNullOrWhiteSpace(address.ZipCode) || x.ZipCode == address.ZipCode));
        }

        public List<Address> FindIdLike(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return _addressData;
            }

            return _addressData.Where(a => (a.EntityId ?? string.Empty).ToLower().Contains(value)).ToList();
        }
    }
}
