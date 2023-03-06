namespace TexoIt.Core.Entities
{
    public class Producer: BaseEntity
    {
        public string Name { get; set; }

        public List<MovieProducer> Movies { get; set; }
    }
}
