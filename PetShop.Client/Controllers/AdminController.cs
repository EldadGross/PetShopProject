using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data.Model;
using PetShop.Service.Interfaces;

namespace PetShop.Client.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAnimalService<Animal> animalService;
        private readonly ICategoryService<Category> categoryService;
        private readonly ICommentService<Comment> commentService;


        public AdminController(IAnimalService<Animal> animalService,
            ICategoryService<Category> categoryService,
            ICommentService<Comment> commentService)
        {
            this.animalService = animalService;
            this.categoryService = categoryService;
            this.commentService = commentService;
        }

        public async Task<IActionResult> Index(AnimalViewModel model)
        {
            model.Animals = await animalService.GetAll().Include(a => a.Category).ToListAsync();
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "CategoryId", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AnimalsByCategory(int categoryId)
        {
            var animals = await animalService.GetAnimalsCategory(categoryId);
            return RedirectToAction(nameof(Index), new { category = categoryId });
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "CategoryId", "Name");
            return View();
        }

        // POST: Animals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AnimalId,Name,BirthDate,Descraption,PictureUrl,CategoryId")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                animalService.Add(animal);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "CategoryId", "Name", animal.CategoryId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await animalService.Get(id);
            if (animal == null)
            {
                return NotFound();
            }
            await animalService.Delete(id);
            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            animalService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await commentService.Delete(id);
            return RedirectToAction(nameof(Edit), new { id = comment.AnimalId });
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var entity = await animalService.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(),
                "CategoryId", "Name", entity.CategoryId);
            return View(entity);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("AnimalId,Name,BirthDate,Descraption,PictureUrl,CategoryId")] Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    animalService.Save(animal);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.AnimalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "CategoryId", "Name", animal.CategoryId);
            return View(animal);
        }

        private bool AnimalExists(int id)
        {
            return animalService.GetAll().Any(e => e.AnimalId == id);
        }
    }
}
