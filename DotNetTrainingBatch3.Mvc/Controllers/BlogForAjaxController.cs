using DotNetTrainingBatch3.Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch3.Mvc.Controllers
{
    public class BlogForAjaxController : Controller
    {
        private readonly DotNetTrainingBatch3DbContext _db;

        public BlogForAjaxController(DotNetTrainingBatch3DbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            var blogs = await _db.TblBlogs
                .AsNoTracking()
                .OrderByDescending(x => x.BlogId)
                .ToListAsync();
            return Json(blogs);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Save(TblBlog blog)
        {
            await _db.TblBlogs.AddAsync(blog);
            var result = await _db.SaveChangesAsync();
            var response = new
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Saving Succesful." : "Saving Failed."
            };
            return Json(response);
        }

        //Update
        [HttpGet]
        public async Task<IActionResult> EditPage(int id)
        {
            var blog = await _db.TblBlogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (blog is null)
            {
                return Json(new { IsSuccess = false, Message = "Blog not found!" });
            }
            return Json(blog);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.BlogId = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, TblBlog blog)
        {
            var item = await _db.TblBlogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Json(new { IsSuccess = false, Message = "Blog Not found" });
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            var result = await _db.SaveChangesAsync();
            var response = new
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Updating Successful" : "Updating Failed."
            };
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _db.TblBlogs.AsNoTracking().FirstOrDefaultAsync(x => x.BlogId == id);
            if (item is null)
            {
                return Json(new { IsSuccess = false, Message = "Blog Not Found!" });
            }
            _db.TblBlogs.Remove(item);
            var result = await _db.SaveChangesAsync();
            var response = new
            {
                IsSuccess = result > 0,
                Message = result > 0 ? "Deleting Successful" : "Deleting Failed."
            };
            return Json(response);
        }
    }
}
