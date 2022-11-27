namespace SampleExercises.Models
{
    public abstract class Entity
    {
        public string? EntityId { get; set; }

        public string? EntityType { get; set;}

        IList<Association>? _entities;
        public IList<Association> Associations
        {
            get
            {
                if (_entities == null)
                    _entities = new List<Association>();

                return _entities;
            }
            set
            {
                _entities = value;
            }
        }
    }
}
