using System;
using System.Collections.Generic;
using System.Data.Entity;
using WebApi2_Beginner.Entities;

namespace WebApi2_Beginner
{
    public class PropertyDbInitializer : CreateDatabaseIfNotExists<PropertyDbContext>
    {
        protected override void Seed(PropertyDbContext context)
        {
            List<Location> locationsList = new List<Location>();
            for (int i = 0; i < 5; i++)
            {
                Location location = new Location
                {
                    Name = "Location " + i,
                    Properties = new List<Property>()
                };
         

                int noOfProperties = new Random().Next(1,10);
                for (int j = 0; j < noOfProperties; j++)
                {
                    location.Properties.Add(new Property
                    {
                        Title = "Property Title ",
                        Description = "Property Description",
                    });
                }
                locationsList.Add(location);
            }
            context.Locations.AddRange(locationsList);
            base.Seed(context);
        }
    }
}