using SampleExercises.Models;

namespace SampleExercises.Repositories
{
    public class VehicleRepository
    {
        private List<Vehicle> _vehicleData;

        public VehicleRepository(List<Vehicle> vehicleData)
        {
            _vehicleData = vehicleData;
        }

        public bool IsEmpty()
        {
            return _vehicleData.Count == 0;
        }

        public int Count()
        {
            return _vehicleData.Count;
        }

        public int EntitiesWithAssociations()
        {
            return _vehicleData.Where(x => x.Associations.Count > 0).Count();
        }

        public Vehicle? GetById(string entityId)
        {
            return _vehicleData.FirstOrDefault(x => x.EntityId == entityId);
        }

        public List<Vehicle> FindIdLike(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return _vehicleData;
            }

            return _vehicleData.Where(a => (a.EntityId ?? string.Empty).ToLower().Contains(value)).ToList();
        }
    }
}
