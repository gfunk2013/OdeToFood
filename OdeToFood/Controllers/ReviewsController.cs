using OdeToFood.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
    {
        private OdeToFoodDb _db = new OdeToFoodDb();

        // GET: Reviews
        public ActionResult Index(int restaurantId)
        {
            if (Session["RestaurantId"] == null || (int)Session["RestaurantId"] != restaurantId)
            {
                Session["RestaurantId"] = restaurantId;
            }

            var restaurant = _db.Restaurants.Find(restaurantId);
            if (restaurant != null)
            {
                IEnumerable<RestaurantReview> restaurantReviews = _db.Reviews.Where(review => review.RestaurantId == restaurantId);
                return View(restaurantReviews);
            }

            return HttpNotFound();
        }

        public ActionResult BestReview()
        {
            RestaurantReview bestReview = new RestaurantReview()
            {
                Rating = 20,
                Body = "this is the best review"
            };
            return PartialView("_Review", bestReview);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, RestaurantId, Rating, Body")] RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                review.RestaurantId = (int)Session["RestaurantId"];
                _db.Reviews.Add(review);
                _db.SaveChanges();
                return RedirectToAction("Index", new { RestaurantId = (int)Session["RestaurantId"] });
            }

            return View(review);
        }

        // GET: Restaurants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview review = _db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Restaurants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, Rating, Body", Exclude = "RestaurantId")] RestaurantReview review)
        {
            if (ModelState.IsValid)
            {
                review.RestaurantId = (int)Session["RestaurantId"];
                _db.Entry(review).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { RestaurantId = (int)Session["RestaurantId"] });
            }
            return View(review);
        }

        // GET: Restaurants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RestaurantReview review = _db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Restaurants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RestaurantReview review = _db.Reviews.Find(id);
            _db.Reviews.Remove(review);
            _db.SaveChanges();
            return RedirectToAction("Index", new { RestaurantId = (int)Session["RestaurantId"] });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}