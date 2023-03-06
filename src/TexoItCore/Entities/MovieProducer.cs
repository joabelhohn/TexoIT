
namespace TexoIt.Core.Entities
{
    public class MovieProducer: BaseEntity
    {
        public long MovieId { get; set; }
        public Movie Movie { get; set; }

        public long ProducerId { get; set; }
        public Producer Producer { get; set; }

    }
}
