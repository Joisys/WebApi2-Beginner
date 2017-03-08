namespace WebApi2_Beginner.Models
{
    public class PropertyModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}