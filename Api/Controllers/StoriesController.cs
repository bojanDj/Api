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
using Api.jwt;
using Api.Models;

namespace Api.Controllers
{
    public class StoriesController : ApiController
    {
        private StoriesEntities db = new StoriesEntities();

        // GET: api/Stories
        public IQueryable<Story> GetStories()
        {
            return db.Stories;
        }

        // GET: api/Stories/5
        [ResponseType(typeof(Story))]
        public IHttpActionResult GetStory(int id)
        {
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return NotFound();
            }

            return Ok(story);
        }

        // PUT: api/Stories/5
        [ResponseType(typeof(void))]
        [CustomAuthenticationFilter]
        public IHttpActionResult PutStory(int id, Story story)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != story.storyID)
            {
                return BadRequest();
            }

            db.Entry(story).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StoryExists(id))
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

        // POST: api/Stories
        [ResponseType(typeof(Story))]
        [CustomAuthenticationFilter]
        public IHttpActionResult PostStory(Story story)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Stories.Add(story);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (StoryExists(story.storyID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = story.storyID }, story);
        }

        // DELETE: api/Stories/5
        [ResponseType(typeof(Story))]
        [CustomAuthenticationFilter]
        public IHttpActionResult DeleteStory(int id)
        {
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return NotFound();
            }

            db.Stories.Remove(story);
            db.SaveChanges();

            return Ok(story);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StoryExists(int id)
        {
            return db.Stories.Count(e => e.storyID == id) > 0;
        }
    }
}