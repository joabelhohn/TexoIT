namespace TexoIt.Core.Entities
{
    public class Studio : BaseEntity
    {
        public string Name { get; set; }
        public List<MovieStudio> Movies { get; set; }

    }
}
