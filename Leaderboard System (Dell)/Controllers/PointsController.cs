﻿using Leaderboard_System__Dell_.DAL;
using Leaderboard_System__Dell_.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Timers;

namespace Leaderboard_System__Dell_.Controllers
{
	public class PointsController : Controller
	{
		PointsDAL pointsContext = new PointsDAL();
		private System.Timers.Timer refreshTimer;
		private List<Points> cachedPoints = new List<Points>();

		public PointsController()
		{
			// Create a timer and set the interval to 5 minutes (changable)
			refreshTimer = new System.Timers.Timer(300000);
			refreshTimer.Elapsed += RefreshData;
			refreshTimer.AutoReset = true; 
			refreshTimer.Start(); 

			cachedPoints = pointsContext.AssignLeaderboard();
		}

		// GET: PointsController
		public ActionResult Index()
		{
			return View(cachedPoints);
		}
		private void RefreshData(object sender, ElapsedEventArgs e)
		{
			List<Points> refreshedPoints = pointsContext.AssignLeaderboard();
			cachedPoints = refreshedPoints;
		}

		// GET: PointsController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: PointsController/Create
		public ActionResult Create()
		{
			Points points = new Points
			{
				Score = 0
			};
			return View(points);
		}

		// POST: PointsController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Points points)
		{
			if (!ModelState.IsValid)
			{
				return RedirectToAction("Create");
			}

			points.ID = pointsContext.CreatePoints(points);
			return RedirectToAction("Index"); 
		}

		// GET: PointsController/Edit/5
		public ActionResult Edit(int id)
		{
			return View();
		}

		// POST: PointsController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: PointsController/Delete/5
		public ActionResult Delete(int id)
		{
			return View();
		}

		// POST: PointsController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
