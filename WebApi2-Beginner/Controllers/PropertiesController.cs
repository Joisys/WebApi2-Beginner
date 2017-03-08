using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi2_Beginner.Entities;
using WebApi2_Beginner.Models.Property;

namespace WebApi2_Beginner.Controllers
{
    public class PropertiesController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            using (PropertyDbContext ctx = new PropertyDbContext())
            {
                var properties = await ctx.Properties.Select(p => new PropertyViewModel
                                                            {
                                                                Id = p.Id,
                                                                Title = p.Title,
                                                                Description = p.Description,
                                                                LocationId = p.LocationId,
                                                                LocationName = p.Location.Name
                                                            }).ToListAsync();
                return Ok(properties);
            }
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            using (PropertyDbContext ctx = new PropertyDbContext())
            {
                var propertyData = await ctx.Properties.FirstOrDefaultAsync(p => p.Id == id);
                if (propertyData == null)
                    return NotFound();

                var location = new PropertyViewModel
                {
                    Id = propertyData.Id,
                    Title = propertyData.Title,
                    Description = propertyData.Description,
                    LocationId = propertyData.LocationId,
                    LocationName = propertyData.Location.Name
                };

                return Ok(location);
            }
        }

        public async Task<IHttpActionResult> Post(CreatePropertyViewModel createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new PropertyDbContext())
            {
                var propertyData = new Property
                {
                    Title = createModel.Title,
                    Description = createModel.Description,
                    LocationId = createModel.LocationId
                };
                ctx.Properties.Add(propertyData);
                await ctx.SaveChangesAsync();

                var property = new PropertyViewModel
                {
                    Id = propertyData.Id,
                    Title = propertyData.Title,
                    Description = propertyData.Description,
                    LocationId = propertyData.LocationId,
                    LocationName = propertyData.Location.Name
                };

                return Created(
                    new Uri(Request.RequestUri + "api/properties" + property.Id), 
                    property);
            }
        }


        public async Task<IHttpActionResult> Put(EditPropertyViewModel editModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new PropertyDbContext())
            {
                var propertyData = await ctx.Properties.FirstOrDefaultAsync(p => p.Id == editModel.Id);
                if (propertyData == null)
                    return NotFound();

                propertyData.Id = editModel.Id;
                propertyData.Title = editModel.Title;
                propertyData.Description = editModel.Description;
                propertyData.LocationId = editModel.LocationId;

                ctx.Properties.Attach(propertyData);
                ctx.Entry(propertyData).State = EntityState.Modified;
                await ctx.SaveChangesAsync();

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            using (var ctx = new PropertyDbContext())
            {
                var property = await ctx.Properties.FirstOrDefaultAsync(p => p.Id == id);
                if (property == null)
                    return NotFound();

                ctx.Properties.Remove(property);
                await ctx.SaveChangesAsync();

                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
