#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Client.ViewModels;
using PetShop.Data.Model;
using PetShop.Service;
using PetShop.Service.Interfaces;
using PetShop.Service.Services;

namespace PetShop.Client.Controllers
{
    [AllowAnonymous]
    public class AnimalsController : Controller
    {
        private readonly IAnimalService<Animal> animalService;
        private readonly ICategoryService<Category> categoryService;
        private readonly ICommentService<Comment> commentService;

        public AnimalsController(IAnimalService<Animal> context,
            ICategoryService<Category> categoryService, ICommentService<Comment> commentService)
        {
            this.categoryService = categoryService;
            animalService = context;
            this.commentService = commentService;
        }

        // GET: Animals
        public async Task<IActionResult> Index(AnimalViewModel model)
        {
            model.Animals = await animalService.GetAll().Include(a => a.Category).ToListAsync();
            ViewData["CategoryId"] = new SelectList(categoryService.GetAll(), "CategoryId", "Name");
            ViewData["Animals"] = AnimalsByCategory(model.CategoryID);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AnimalsByCategory(int categoryId)
        {
            var animals = await animalService.GetAnimalsCategory(categoryId);
            return RedirectToAction(nameof(Index), new AnimalViewModel
            { CategoryID = categoryId, Animals = animals });
        }

        public async Task<IEnumerable<Animal>> SelectByCategory(Category category)
        {
            var animalsCategory = animalService.GetAll()
                .Where(a => a.CategoryId == category.CategoryId);
            ViewBag.CategoryId = category.CategoryId;
            return await animalsCategory.ToListAsync();
        }

        // GET: Animals/Details/5
        public async Task<IActionResult> Details(CommentViewModel model, int id)
        {
            model.Animal = await animalService.GetAll().Include(c => c.Comments)
                .FirstOrDefaultAsync(m => m.AnimalId == id);
            if (model.Animal == null)
            {
                return NotFound();
            }

            return View(model);
        }

        public  IActionResult AddComment(CommentViewModel model)
        {
            if (model.CommentText != null)
            {
                Comment comment = new()
                { 
                    AnimalId = model.Animal.AnimalId,
                    CommentText = model.CommentText
                };
                commentService.Add(comment);
            }
            return RedirectToAction(nameof(Details), new { id = model.Animal.AnimalId,
                viewModel = model });
        }
    }
}
