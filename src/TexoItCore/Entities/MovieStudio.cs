namespace TexoIt.Core.Entities
{
    public class MovieStudio: BaseEntity
    {
        public long MovieId { get; set; }
        public Movie Movie { get; set; }

        public long StudioId { get; set; }  
        public Studio Studio { get; set; }  

    }
}
