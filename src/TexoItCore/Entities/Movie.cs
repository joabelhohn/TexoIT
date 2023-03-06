namespace TexoIt.Core.Entities
{
    public class Movie: BaseEntity
    {
        public int Year { get; set; }
        public string Title { get; set; }
        public List<MovieStudio> Studios { get; set; } = new List<MovieStudio>();
        public List<MovieProducer> Producers { get; set; } = new List<MovieProducer>();
        public bool Winner { get; set; }

    }
}
