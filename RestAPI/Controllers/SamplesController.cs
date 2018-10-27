using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using RestAPI.Models;
using System.Web.SessionState;
using System.Web.Mvc;

namespace RestAPI.Controllers
{
    public class SamplesController : ApiController
    {
        private RestAPIContext db = new RestAPIContext();

        // GET: api/Samples
        public IQueryable<Sample> GetSamples()
        {
            return db.Samples;
        }

        // GET: api/Samples/5
        [ResponseType(typeof(Sample))]
        public IHttpActionResult GetSample(string id)
        {
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return NotFound();
            }

            return Ok(sample);
        }

        // PUT: api/Samples/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSample(string id, Sample sample)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sample.Id)
            {
                return BadRequest();
            }

            db.Entry(sample).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SampleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Samples
        [ResponseType(typeof(Sample))]
        public HttpResponseMessage PostSample(List<Sample> sampleList)
        {
            if (!ModelState.IsValid)
            {
                return new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest };
            }

            foreach (var sample in sampleList)
            {
                db.Samples.Add(sample);
            }
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {

                throw;
            }

            return new HttpResponseMessage { StatusCode = HttpStatusCode.Created };
        }

        // DELETE: api/Samples/5
        [ResponseType(typeof(Sample))]
        public IHttpActionResult DeleteSample(string id)
        {
            Sample sample = db.Samples.Find(id);
            if (sample == null)
            {
                return NotFound();
            }

            db.Samples.Remove(sample);
            db.SaveChanges();

            return Ok(sample);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SampleExists(string id)
        {
            return db.Samples.Count(e => e.Id == id) > 0;
        }
    }
}