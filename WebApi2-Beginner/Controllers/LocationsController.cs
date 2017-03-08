using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi2_Beginner.Entities;
using WebApi2_Beginner.Models.Location;

namespace WebApi2_Beginner.Controllers
{
    public class LocationsController : ApiController
    {
        public async Task<IHttpActionResult> Get()
        {
            using (PropertyDbContext ctx = new PropertyDbContext())
            {
                var locations = await ctx.Locations.Select(l => new LocationViewModel
                                                            {
                                                                Id = l.Id,
                                                                Name = l.Name
                                                            }).ToListAsync();
                return Ok(locations);
            }
        }

        public async Task<IHttpActionResult> Get(int id)
        {
            using (PropertyDbContext ctx = new PropertyDbContext())
            {
                var locationData = await ctx.Locations.FirstOrDefaultAsync(l => l.Id == id);
                if (locationData == null)
                    return NotFound();

                var location = new LocationViewModel
                {
                    Id = locationData.Id,
                    Name = locationData.Name
                };

                return Ok(location);
            }
        }

        public async Task<IHttpActionResult> Post(CreateLocationViewModel createModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new PropertyDbContext())
            {
                var locationData = new Location
                {
                    Name = createModel.CreateName
                };
                ctx.Locations.Add(locationData);
                await ctx.SaveChangesAsync();

                var location = new LocationViewModel
                {
                    Id = locationData.Id,
                    Name = locationData.Name
                };

                return Created(
                    new Uri(Request.RequestUri + "api/loctions" + location.Id), 
                    location);
            }
        }


        public async Task<IHttpActionResult> Put(EditLocationViewModel editModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (var ctx = new PropertyDbContext())
            {
                var locationData = await ctx.Locations.FirstOrDefaultAsync(l => l.Id == editModel.Id);
                if (locationData == null)
                    return NotFound();

                locationData.Id = editModel.Id;
                locationData.Name = editModel.EditName;

                ctx.Locations.Attach(locationData);
                ctx.Entry(locationData).State = EntityState.Modified;
                await ctx.SaveChangesAsync();

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        public async Task<IHttpActionResult> Delete(int id)
        {
            using (var ctx = new PropertyDbContext())
            {
                var location = await ctx.Locations.FirstOrDefaultAsync(o => o.Id == id);
                if (location == null)
                    return NotFound();

                ctx.Locations.Remove(location);
                await ctx.SaveChangesAsync();

                return StatusCode(HttpStatusCode.NoContent);
            }
        }
    }
}
