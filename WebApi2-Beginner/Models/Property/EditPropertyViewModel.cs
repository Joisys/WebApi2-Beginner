﻿namespace WebApi2_Beginner.Models.Property
{
    public class EditPropertyViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int LocationId { get; set; }
    }
}