using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HuseKnives.Data;
using HuseKnives.Models;
using PagedList.Mvc;
using PagedList;
using System.IO;

namespace HuseKnives.Controllers
{
    public class InventoriesController : Controller
    {
        private InventoryContext db = new InventoryContext();

        // GET: Inventories

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {


            //This creates a tempary sorting order (which will be default if null)
            ViewBag.CurrentSort = sortOrder;

            //This checks to see if searchstring is null or not, and if searchstring IS NULL , then list page one
            if (searchString != null)
            {
                page = 1;

            }
            else
            {
                // if it's NOT NULL then assign the current filter to the searchstring paramter
                searchString = currentFilter;
            }

            // filter the values of teh database
            var Results = (IQueryable<Inventory>)db.Inventories;


            //assign searchstring to whatever the currentfilter is
            ViewBag.CurrentFilter = searchString;

            //if the user types in anything in the search box, search the database
            if (!String.IsNullOrEmpty(searchString))
            {
                Results = Results.Where(x => x.WeaponName.Contains(searchString)
                || x.BladeSteel.Contains(searchString)
                || x.HandleMaterial.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "WeaponName":
                    Results = Results.OrderByDescending(x => x.WeaponName);
                    break;
                case "BladeSteel":
                    Results = Results.OrderByDescending(x => x.BladeSteel);
                    break;
                case "HandleMaterial":
                    Results = Results.OrderByDescending(x => x.HandleMaterial);
                    break;
                default:
                    Results = Results.OrderByDescending(x => x.Id);
                    break;

            }


            int pageSize = 6;
            int pageNumber = (page ?? 1);

            //return the results of the search
            return View(Results.ToPagedList(pageNumber, pageSize));
        }

        //    public actionresult index()
        //{
        //    return view(db.inventories.tolist());
        //}

        //GET: Inventories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // GET: Inventories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,WeaponName,WeaponDescription,BladeSteel,HandleMaterial,RCHardness,Weight,Price,IsActive")] Inventory inventory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Inventories.Add(inventory);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(inventory);
        //}
        public ActionResult Create(InventoryViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.File != null)
                {
                    //string pic = string.Concat(String.Format("{0:s}", DateTime.Now), Path.GetFileName(model.File.FileName));
                    string pic = Path.GetFileName(model.File.FileName);
                    string dirPath = Server.MapPath("~/images/");
                    if (!System.IO.Directory.Exists(dirPath))
                    {
                        System.IO.Directory.CreateDirectory(dirPath);
                    }

                    string path = Path.Combine(
                                         Server.MapPath("~/images"), pic);

                    //Save the image
                    model.File.SaveAs(path);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        model.File.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                    model.Inventories.Image.ImageName = model.File.FileName;

                }

                db.Inventories.Add(model.Inventories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }


        // GET: Inventories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,WeaponName,WeaponDescription,BladeSteel,HandleMaterial,RCHardness,Weight,Price,IsActive")] Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(inventory);
        }

        // GET: Inventories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventory inventory = db.Inventories.Find(id);
            if (inventory == null)
            {
                return HttpNotFound();
            }
            return View(inventory);
        }

        // POST: Inventories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Inventory inventory = db.Inventories.Find(id);
            db.Inventories.Remove(inventory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
