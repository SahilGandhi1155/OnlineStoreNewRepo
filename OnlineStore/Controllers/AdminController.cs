using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineStore.Model;
using OnlineStore.Repository;
using OnlineStore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: /<controller>/
       
        private readonly ICategoryRepository _categoryrepository;
        private readonly IProductRepository _productrepository;
       
        public AdminController(ICategoryRepository categoryrepository,IProductRepository productRepository)
        {
           this._categoryrepository = categoryrepository;
            this._productrepository = productRepository;
        }

        public List<SelectListItem> GetCategory()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _categoryrepository.GetAllCategory();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        public IActionResult Dashboard()
        {
            return View();
        }
        
        
        [Authorize(Roles = "User,Administator")]
        public IActionResult Products()
        {
            var model = _productrepository.GetAllProducts();
            return View(model);
        }


      
        [Route("ProductEdit/{id}")]
        [HttpGet]
        public IActionResult ProductEdit(int id)
        {
            ViewBag.CategoryList = GetCategory();
            var product = _productrepository.GetProduct(id);

            var listCategories = new List<Category>();
           
            HomeDetailsViewModel productDetailsViewModel = new HomeDetailsViewModel()
            {
                Product = product,
                PageTilte = "Product Details"
            };
            return View(productDetailsViewModel);
        }

        [Route("ProductEdit/{id}")]
        [HttpPost]
        public IActionResult ProductEdit(HomeDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                Product product = _productrepository.GetProduct(model.Product.Id);
                product.Name = model.Product.Name;
                product.Image = model.Product.Image;
                product.OriginalPrice = model.Product.OriginalPrice;
                product.DiscountPrice = model.Product.DiscountPrice;
                product.DiscountPercentage = model.Product.DiscountPercentage;
                product.Description = model.Product.Description;
                product.CategoryId = model.Product.CategoryId;
                _productrepository.Update(product);

                return RedirectToAction("Products");
            }

            return View();
        }

        public IActionResult Categories()
        {
            var model = _categoryrepository.GetAllCategory();
            
            return View(model);
        }


        [Route("CategoryEdit/{id}")]
        [HttpGet]
        public IActionResult CategoryEdit(int id)
        {
            var category = _categoryrepository.GetCategory(id);
            CategoryDetailsViewModel categoryDetailsViewModel = new CategoryDetailsViewModel()
            {
                Category = category,
                PageTilte = "Category Details"
            };
            return View(categoryDetailsViewModel);
        }

        [Route("CategoryEdit/{id}")]
        [HttpPost]
        public IActionResult CategoryEdit(CategoryDetailsViewModel model)
        {
            if(ModelState.IsValid)
            {
                Category category = _categoryrepository.GetCategory(model.Category.CategoryId);
                category.CategoryName = model.Category.CategoryName;
                category.IsActive = model.Category.IsActive;
                category.IsDelete = model.Category.IsDelete;
                _categoryrepository.Update(category);

                return RedirectToAction("Categories");
            }

            return View();
        }

        //public IActionResult AddCategory()
        //{
        //    return UpdateCategory(0);
        //}

        //public IActionResult UpdateCategory(int categoryId)
        //{
        //    CategoryDetail cd;
        //    if (categoryId != null)
        //    {
        //        cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId)));
        //    }
        //    else
        //    {
        //        cd = new CategoryDetail();
        //    }
        //    return View("UpdateCategory", cd);
        //}
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                Category newCategory = _categoryrepository.Add(category);
                return RedirectToAction("Categories");
            }

            return View();
        }


        [HttpGet]
        public ViewResult AddProduct()
        {
            var listCategories = new List<Category>();
            listCategories = _categoryrepository.GetAllCategory().ToList();

           
            ViewBag.ListofCategories = listCategories;
               return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            var listCategories = new List<Category>();
            listCategories = _categoryrepository.GetAllCategory().ToList();

            Product newProduct = _productrepository.Add(product);
            return RedirectToAction("Details","Home",new { id = product.Id });

           
        }
    }
}
