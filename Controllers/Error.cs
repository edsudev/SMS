using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EDSU_SYSTEM.Controllers
{
    public class Error : Controller
    {
        // GET: Error
        public ActionResult Badreq()
        {
            ViewBag.err = TempData["err"];
            return View();
        }
        public ActionResult ResourceNotFound()
        {
            return View();
        }
        public ActionResult ServerError()
        {
            return View();
        } 
        public ActionResult PageNotFound()
        {
            return View();
        }
        public ActionResult Nocourses()
        {
            return View();
        }
        public ActionResult ServiceUnavailable()
        {
            return View();
        }
        public ActionResult AuthError()
        {
            ViewBag.err = TempData["err"];
            return View();
        }
        // GET: Error/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Error/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Error/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: Error/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Error/Edit/5
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

        // GET: Error/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Error/Delete/5
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
