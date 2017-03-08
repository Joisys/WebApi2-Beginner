namespace WebApi2_Beginner.Models.Property
{
    public class PropertyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        //TODO: Use LocationViewModel for the following
        public int LocationId { get; set; }
        public string LocationName { get; set; }
    }
}