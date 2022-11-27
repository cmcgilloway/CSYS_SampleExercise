using SampleExercises.Models;

namespace SampleExercises.Repositories
{
    public class OrganizationRepository
    {
        private List<Organization> _organizationData;

        public OrganizationRepository(List<Organization> organizationData)
        {
            _organizationData = organizationData;
        }

        public bool IsEmpty()
        {
            return _organizationData.Count == 0;
        }

        public int Count()
        {
            return _organizationData.Count;
        }

        public int EntitiesWithAssociations()
        {
            return _organizationData.Where(x => x.Associations.Count > 0).Count();
        }

        public Organization? GetById(string entityId)
        {
            return _organizationData.FirstOrDefault(x => x.EntityId == entityId);
        }

        public Organization? GetFullEntity(Organization organization)
        {
            return _organizationData.FirstOrDefault(x => (string.IsNullOrWhiteSpace(organization.Name) || x.Name == organization.Name)
                && (string.IsNullOrWhiteSpace(organization.Type) || x.Type == organization.Type)
                && (organization.YearStarted == 0 || x.YearStarted == organization.YearStarted));
        }

        public List<Organization> FindIdLike(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return _organizationData;
            }

            return _organizationData.Where(a => (a.EntityId ?? string.Empty).ToLower().Contains(value)).ToList();
        }
    }
}
