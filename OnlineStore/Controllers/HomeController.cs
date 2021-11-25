using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using OnlineStore.ViewModels;
using OnlineStore.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using OnlineStore.Repository;
using System.Text;
using System;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Authorization;

namespace OnlineStore.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public HomeController(AppDbContext context, IProductRepository productRepository,
            ICustomerDetailRepository customerDetailRepository,
            IPriceRepository priceRepository,
            IDiscountPercentageRepository discountPercentageRepository,
            ISortRepository sortRepository)
        {
            this.context = context;
            this._productRepository = productRepository;
            this._customerDetailRepository = customerDetailRepository;
            this._priceRepository = priceRepository;
            this._discountPercentageRepository = discountPercentageRepository;
            this._sortRepository = sortRepository;
        }
        
        public AppDbContext context { get; }
        private readonly IProductRepository _productRepository;
        private readonly ICustomerDetailRepository _customerDetailRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IDiscountPercentageRepository _discountPercentageRepository;
        private readonly ISortRepository _sortRepository;
        public ISession _session;

        public IActionResult Checkout()
        {
            return View();
        }




        public IActionResult CheckoutDetails()
        {
            return View();
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Search(string search)
        {
            if (search == null)
            {
                return View("Index");
            }
            else
            {
                var searchedProducts = _productRepository.GetAllProducts().Where(x => x.Name.ToUpper().Contains(search.ToUpper())).ToList();
                ProductListingViewModel productListingViewModel4 = new ProductListingViewModel
                {
                    product = searchedProducts,
                    price = _priceRepository.GetAllPrices(),
                    discountPercentage= _discountPercentageRepository.GetAllDiscountPercentages()
                };
                ViewData["searchValue"] = search;
                return View("ProductListing", productListingViewModel4);
            }
        }
        
        [HttpGet]
        public ViewResult ProductListing()
        {
            var products =_productRepository.GetAllProducts();
            var prices = _priceRepository.GetAllPrices();
            var discountpercentages = _discountPercentageRepository.GetAllDiscountPercentages();
            ProductListingViewModel productListingViewModel = new ProductListingViewModel();
            productListingViewModel.product = products;
            productListingViewModel.price =prices;
            productListingViewModel.discountPercentage = discountpercentages;
            productListingViewModel.sorts = _sortRepository.GetAllSortPrices();
             
            
            return View(productListingViewModel);
        }

        [HttpPost]
        public ViewResult ProductListing(ProductListingViewModel model)
        {
            
            var productListingViewModel = new ProductListingViewModel()
            {
                product = model.product,
                price = model.price
            };

            var selectedIds = productListingViewModel.price
                                .Where(x => x.isSelected==true)
                                .Select(y => y.id).ToList<int>();
            var products = _productRepository.GetAllProducts();
           List<Product> listProducts = new List<Product>();
            
           

            foreach (int i in selectedIds)
            {
                foreach (var pr in products)
                {
                    if (pr.DiscountPrice < i*500 && pr.DiscountPrice >(i-1)*500)
                    {
                        listProducts.Add(pr);
                    }
            }
                //products = products.Where(x => x.DiscountPrice < i * 500).Where(x =>x.DiscountPrice>(i-1)*500).ToList();
            }
           
            
                var prices = _priceRepository.GetAllPrices();
                 ViewBag.PriceList = prices;
                ProductListingViewModel productListingViewModel2 = new ProductListingViewModel();
                productListingViewModel2.product = listProducts;
                productListingViewModel2.price = productListingViewModel.price;
                productListingViewModel2.sorts = _sortRepository.GetAllSortPrices();


            return View("ProductListing", productListingViewModel2);

            
        }


        [HttpGet]
       [Route("Home/SearchFilter/{catId?}/{subcatId?}")]
        [Route("Home/SearchFilter/{catId?}/{brandId?}")]
        public ViewResult SearchFilter(int? catId, [Optional]int? subcatId, [Optional]int ?brandId)
        {

            var products = _productRepository.GetAllProducts();
            var prices = _priceRepository.GetAllPrices();
            var discountpercentages = _discountPercentageRepository.GetAllDiscountPercentages();

            if (catId != null)
            { 
                products = products.Where(x => x.catId == catId.Value).ToList();
            }
            if (subcatId != null)
            {
                products = products.Where(x => x.subCatId == subcatId.Value).ToList();
            }

            if (brandId != null)
            {
                products = products.Where(x => x.brandId == brandId.Value).ToList();
            }

            ProductListingViewModel model = new ProductListingViewModel();
            model.product = products;
            model.price = prices;
            model.discountPercentage = discountpercentages;

            ViewData["subcatId"] = subcatId;
            ViewData["catId"] = catId;
            ViewData["brandId"] = brandId;

            return View("ProductListing", model);


        }


        [HttpGet]
        public ViewResult FilterByPrice(ProductListingViewModel model,[Optional]int? catId, 
            [Optional]int? subcatId, [Optional]int? brandId, [Optional]String searchValue,[Optional]int? priceValue,
            [Optional] int? discountValue,[Optional] int? sortBy,[Optional] int? pageIndex, int PageSize =2)
        {
            //string subcatId1 = Request.Form["subcatId"];
            //string catId1 = Request.Form["catId"];
            //string brandId1 = Request.Form["brandId"];
            var productListingViewModel = new ProductListingViewModel()
            {
                product = model.product,
                price = model.price,
                discountPercentage = model.discountPercentage

            };

            //var selectedIdsInPrice = productListingViewModel.price
            //                    .Where(x => x.isSelected == true)
            //                    .Select(y => y.id).ToList();
            ProductListingViewModel productListingViewModel3 = new ProductListingViewModel();


            var products = _productRepository.GetAllProducts();
            //var products = _productRepository.GetAllProducts().ToList();

            if (catId != null)
            {
                products = products.Where(x => x.catId == catId.Value).ToList();
            }
            if (subcatId != null)
            {
                products = products.Where(x => x.subCatId == subcatId.Value).ToList();
            }

            if (brandId != null)
            {
                products = products.Where(x => x.brandId == brandId.Value).ToList();
            }

            if (!String.IsNullOrEmpty(searchValue))
            {
                products = products.Where(x => x.Name.ToUpper().Contains(searchValue.ToUpper())).ToList();
            }
            List<Product> listProducts = new List<Product>();
            if (priceValue != null)
            {
                listProducts.Clear();
                foreach (var pr in products)
                {
                    if (pr.DiscountPrice < priceValue * 500 && pr.DiscountPrice > (priceValue - 1) * 500)
                    {
                        listProducts.Add(pr);
                 }
                }
                products = listProducts.ToList();
                
            }
           

            if(discountValue != null)
            {
                listProducts.Clear();
                foreach (var pr in products)
                {
                   if (pr.DiscountPercentage >= discountValue*5)
                     {
                            listProducts.Add(pr);
                    }
                }
                products = listProducts.ToList();
            }
            
            
            if(sortBy != null)
            {
                if(sortBy==1)
                {
                    //product newest based ordering
                }

                if (sortBy == 2)
                {
                     products = products.OrderBy(x => x.DiscountPrice).ToList();
                }
                if (sortBy == 3)
                {
                    products = products.OrderByDescending(x => x.DiscountPrice).ToList();
                }
                if (sortBy == 4)
                {
                   //product popularity based ordering
                }
            }
            int count = products.Count();
            int pageNumber = pageIndex ?? 1;
           products = products.Skip(PageSize * (pageNumber - 1)).Take(PageSize).ToList();

            //if(page != null)
            //{
            //    int pagenumber = page ?? 1;
            //    products = products.ToPagedList()
            //}


            productListingViewModel3.product = products;

            productListingViewModel3.price = _priceRepository.GetAllPrices();
            productListingViewModel3.discountPercentage = _discountPercentageRepository.GetAllDiscountPercentages();
            productListingViewModel3.sorts = _sortRepository.GetAllSortPrices();
            ViewData["subcatId"] = subcatId;
            ViewData["catId"] = catId;
            ViewData["brandId"] = brandId;
            ViewData["searchValue"] = searchValue;
            ViewData["priceValue"] = priceValue;
            ViewData["discountValue"] = discountValue;
            ViewData["SortBy"] = sortBy;
            ViewData["Count"] = count;
            ViewData["pageNumber"] = pageNumber;
            ViewData["pageSize"] = PageSize;
            return View("ProductListing", productListingViewModel3);


        }


        [HttpPost]
        public ViewResult filterbyDiscountPercent(ProductListingViewModel model, [Optional]int? catId, [Optional]int? subcatId, [Optional]int? brandId, [Optional]String searchValue)
        {
            var productListingViewModel = new ProductListingViewModel()
            {
                product = model.product,
                price = model.price,
                discountPercentage = model.discountPercentage

            };
            List<Product> listProducts = new List<Product>();
           
            var products = _productRepository.GetAllProducts().ToList();
            if (catId != null)
            {
                products = products.Where(x => x.catId == catId.Value).ToList();
            }
            if (subcatId != null)
            {
                products = products.Where(x => x.subCatId == subcatId.Value).ToList();
            }

            if (brandId != null)
            {
                products = products.Where(x => x.brandId == brandId.Value).ToList();
            }
            if (!String.IsNullOrEmpty(searchValue))
            {
                products = products.Where(x => x.Name.ToUpper().StartsWith(searchValue.ToUpper())).ToList();
            }
            //foreach(var receivedPr in productListingViewModel.product)
            //{

            //    products.Add(_productRepository.GetProduct(receivedPr.Id));
            //}


            //Price Filtering//
            var selectedIdsInPrice = productListingViewModel.price
                                .Where(x => x.isSelected == true)
                                .Select(y => y.id).ToList();
            if (selectedIdsInPrice.Count != 0)
            {
                foreach (var pr in products)
                {
                    foreach (var id in selectedIdsInPrice)
                {
                        if (pr.DiscountPrice < id * 500 && pr.DiscountPrice > (id - 1) * 500)
                        {
                            listProducts.Add(pr);
                        }
                    }
                    //products = products.Where(x => x.DiscountPrice < i * 500).Where(x =>x.DiscountPrice>(i-1)*500).ToList();
                }
                products = listProducts.ToList();
                listProducts.Clear();
            }


            //DiscountPercentage//
            var selectedIdsinDiscountPercentage = productListingViewModel.discountPercentage
                               .Where(x => x.isSelected == true)
                               .Select(y => y.id).ToList();
            
           
            if (selectedIdsinDiscountPercentage.Count != 0)
            {
                foreach (int i in selectedIdsinDiscountPercentage)
                {
                    foreach (var pr in products)
                    {
                        if (pr.DiscountPercentage > i * 5 && pr.DiscountPercentage <= (i+1)*5 )
                        {
                            listProducts.Add(pr);
                        }
                    }
                    //products = products.Where(x => x.DiscountPrice < i * 500).Where(x =>x.DiscountPrice>(i-1)*500).ToList();
  
                }
                products = listProducts.ToList();
                listProducts.Clear();
            }

            ProductListingViewModel productListingViewModel3 = new ProductListingViewModel();
            productListingViewModel3.product = products;
            productListingViewModel3.discountPercentage = productListingViewModel.discountPercentage;
            productListingViewModel3.price = productListingViewModel.price;
            ViewData["subcatId"] = subcatId;
            ViewData["catId"] = catId;
            ViewData["brandId"] = brandId;
            ViewData["searchValue"] = searchValue;
            return View("ProductListing", productListingViewModel3);


        }

        [HttpPost]
        public ViewResult SortByPriceDesc(ProductListingViewModel model)
        {
            var productListingViewModel = new ProductListingViewModel()
            {
                product = model.product,
                price = model.price,
                discountPercentage = model.discountPercentage
                
            };

            
            var products = productListingViewModel.product.OrderByDescending(x=>x.DiscountPrice).ToList();
            var prices = productListingViewModel.price;
            var discountpercentage = productListingViewModel.discountPercentage;

            ProductListingViewModel productListingViewModel4 = new ProductListingViewModel();
            productListingViewModel4.product = products;
            productListingViewModel4.price = prices;
            productListingViewModel4.discountPercentage = discountpercentage;

            return View("ProductListing", productListingViewModel4);


        }
        [HttpPost]
        public ViewResult SortByPriceAsc(ProductListingViewModel model)
        {
            var productListingViewModel = new ProductListingViewModel()
            {
                product = model.product,
                price = model.price,
                discountPercentage= model.discountPercentage
               
            };


            var products = productListingViewModel.product.OrderBy(x => x.DiscountPrice).ToList();
            var prices = productListingViewModel.price;
            var discountpercentage = productListingViewModel.discountPercentage;

            ProductListingViewModel productListingViewModel2 = new ProductListingViewModel();
            productListingViewModel2.product = products;
            productListingViewModel2.price = prices;
            productListingViewModel2.discountPercentage = discountpercentage;

            return View("ProductListing", productListingViewModel2);


        }
        public class RequestItem
        {
            public int productId { get; set; }
            public int Qty { get; set; }
        }
        public class ResponseItem
        {
            public string Name { get;  set; }
            public int price { get; set; }
            public int productId { get; set; }
            public int Qty { get; set; }
        }

        

        [Route("Details/{id}")]
        public ViewResult Details(int id)
        {
            var product = _productRepository.GetProduct(id);
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Product = product,
                PageTilte = "Channel Details"
            };
            return View(homeDetailsViewModel);
        }

        [HttpGet]
        public IActionResult AddCustomerDetails()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCustomerDetails(CustomerDetail customerDetail)
        {
            CustomerDetail newCustomer = _customerDetailRepository.Add(customerDetail);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Checkbox()
        {
            var prices = _priceRepository.GetAllPrices();
            var products = _productRepository.GetAllProducts();
            ProductListingViewModel productListingViewModel = new ProductListingViewModel();
            productListingViewModel.product = products;
            productListingViewModel.price = prices;

            return View("ProductListing", productListingViewModel);
        }

        [HttpPost]
        public ActionResult Checkbox(PriceViewModel model)
        {
            var price = model.prices.Where(x => x.isSelected == true).ToList<Price>();
            return Content(String.Join(",", price.Select(x => x.Value)));
        }
    }
}
