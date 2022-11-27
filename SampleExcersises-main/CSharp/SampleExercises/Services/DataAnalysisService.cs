using Newtonsoft.Json;
using SampleExercises.Models;
using SampleExercises.Repositories;

namespace SampleExercises.Services
{
    internal class DataAnalysisService
    {
        private PersonRepository _personRepo;
        private OrganizationRepository _organizationRepo;
        private VehicleRepository _vehicleRepo;
        private AddressRepository _addressRepo;

        public DataAnalysisService(List<Person> personData, 
            List<Organization> organizationData, 
            List<Vehicle> vehicleData, 
            List<Address> addressData)
        {
            _personRepo = new PersonRepository(personData);
            _organizationRepo = new OrganizationRepository(organizationData);
            _vehicleRepo= new VehicleRepository(vehicleData);
            _addressRepo= new AddressRepository(addressData);
        }

        public bool IsAnyDataEmpty()
        {
            return _personRepo.IsEmpty()
                || _organizationRepo.IsEmpty()
                || _vehicleRepo.IsEmpty()
                || _addressRepo.IsEmpty();
        }

        public int TotalEntities()
        {
            return _personRepo.Count()
                + _organizationRepo.Count()
                + _vehicleRepo.Count()
                + _addressRepo.Count();
        }

        public string EntityCounts()
        {
            return string.Format("Person: {0} | Organization: {1} | Vehicle: {2} | Address: {3}", 
                _personRepo.Count(), 
                _organizationRepo.Count(), 
                _vehicleRepo.Count(), 
                _addressRepo.Count());
        }

        public string EntitiesWithAssociations()
        {
            int personsWithAssociations = _personRepo.EntitiesWithAssociations();
            int organizationsWithAssociations = _organizationRepo.EntitiesWithAssociations();
            int vehiclesWithAssociations = _vehicleRepo.EntitiesWithAssociations();
            int addressesWithAssociations = _addressRepo.EntitiesWithAssociations();

            return string.Format("Person: {0} | Organization: {1} | Vehicle: {2} | Address: {3} | Total: {4}",
                personsWithAssociations,
                organizationsWithAssociations,
                vehiclesWithAssociations,
                addressesWithAssociations, 
                personsWithAssociations + organizationsWithAssociations + vehiclesWithAssociations + addressesWithAssociations);
        }

        public List<Association> GetAssociations(string entityType, Entity? entity)
        {
            if (entity == null)
            {
                return new List<Association>();
            }

            if (entity.Associations.Count == 0)
            {

                switch (entityType)
                {
                    case "Address":
                        entity = _addressRepo.GetFullEntity((Address)entity) ?? new Address();
                        break;
                    case "Organization":
                        entity = _organizationRepo.GetFullEntity((Organization)entity) ?? new Organization();
                        break;
                    default:
                        break;
                }
            }

            return (List<Association>)entity.Associations;
        }

        public string GetAssociatedEntities(List<Association> associations)
        {
            string outputString = string.Empty;
            foreach (Association association in associations)
            {
                if (!string.IsNullOrWhiteSpace(outputString))
                {
                    outputString += "\r\n------------------\r\n";
                }

                switch (association.EntityType)
                {
                    case "Vehicle":
                        Vehicle? vehicle = _vehicleRepo.GetById(association.EntityId ?? string.Empty);
                        outputString += (vehicle == null) ? string.Empty : JsonConvert.SerializeObject(vehicle);
                        break;
                    case "Person":
                        Person? person = _personRepo.GetById(association.EntityId ?? string.Empty);
                        outputString += (person == null) ? string.Empty : JsonConvert.SerializeObject(person);
                        break;
                }
            }
            
            if (string.IsNullOrWhiteSpace(outputString))
            {
                outputString = "None";
            }

            return outputString;
        }

        public int GetDuplicateNameCount()
        {
            return _personRepo.GetDuplicatedFirstMiddleNames().Count();
        }

        public string FoundIdSearchCounts(string value)
        {
            int foundPersonCount = _personRepo.FindIdLike(value).Count();
            int foundOrganizationCount = _organizationRepo.FindIdLike(value).Count();
            int foundVehicleCount = _vehicleRepo.FindIdLike(value).Count();
            int foundAddressCount = _addressRepo.FindIdLike(value).Count();

            return string.Format("Person: {0} | Organization: {1} | Vehicle: {2} | Address: {3} | Total: {4}",
                foundPersonCount,
                foundOrganizationCount,
                foundVehicleCount,
                foundAddressCount,
                foundPersonCount + foundOrganizationCount + foundVehicleCount + foundAddressCount);
        }
        
    }
}
