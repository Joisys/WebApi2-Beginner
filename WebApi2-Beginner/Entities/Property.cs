namespace WebApi2_Beginner.Entities
{
    public class Property
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}