using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineStore.Model;
using OnlineStore.Repository;
using OnlineStore.ViewModels;

namespace OnlineStore.Controllers
{
    public class CartController : Controller
    {
        public CartController(AppDbContext context,IProductRepository productRepository,
            ICustomerDetailRepository customerDetailRepository,
            IPriceRepository priceRepository,
            IDiscountPercentageRepository discountPercentageRepository,
            ISortRepository sortRepository)
        {
            this._context = context;
            this._productRepository = productRepository;
            this._customerDetailRepository = customerDetailRepository;
            this._priceRepository = priceRepository;
            this._discountPercentageRepository = discountPercentageRepository;
            this._sortRepository = sortRepository;
        }

        public AppDbContext _context { get; }
        private readonly IProductRepository _productRepository;
        private readonly ICustomerDetailRepository _customerDetailRepository;
        private readonly IPriceRepository _priceRepository;
        private readonly IDiscountPercentageRepository _discountPercentageRepository;
        private readonly ISortRepository _sortRepository;
        public ISession _session;
       
       
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BuyNowviaProductDetail(int productId)
        {
            List<Item> cartList = new List<Item>();
            var stringObject = HttpContext.Session.GetString("cart");
            if (stringObject == null)
            {

                var product = _context.Products.Find(productId);
                cartList.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                cartList.OrderBy(b => b.Product.DiscountPrice);
                stringObject = JsonConvert.SerializeObject(cartList);
                HttpContext.Session.SetString("cart", stringObject);
            }
            else
            {

                cartList = JsonConvert.DeserializeObject<List<Item>>(stringObject);
                var count = cartList.Count();
                var product = _context.Products.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cartList[i].Product.Id == productId)
                    {
                        int prevQty = cartList[i].Quantity;
                        cartList.Remove(cartList[i]);
                        cartList.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cartList.Where(x => x.Product.Id == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cartList.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                stringObject = JsonConvert.SerializeObject(cartList);
                HttpContext.Session.SetString("cart", stringObject);
            }
           
            return View("Checkout");

        }

        [HttpGet]
        public ActionResult AddToCartviaProductDetail(int productId)
        {
            List<Item> cartList = new List<Item>();
            var stringObject = HttpContext.Session.GetString("cart");
            if (stringObject == null)
            {

                var product = _context.Products.Find(productId);
                cartList.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                cartList.OrderBy(b => b.Product.DiscountPrice);
                stringObject = JsonConvert.SerializeObject(cartList);
                HttpContext.Session.SetString("cart", stringObject);
            }
            else
            {

                cartList = JsonConvert.DeserializeObject<List<Item>>(stringObject);
                var count = cartList.Count();
                var product = _context.Products.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cartList[i].Product.Id == productId)
                    {
                        int prevQty = cartList[i].Quantity;
                        cartList.Remove(cartList[i]);
                        cartList.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cartList.Where(x => x.Product.Id == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cartList.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                stringObject = JsonConvert.SerializeObject(cartList);
                HttpContext.Session.SetString("cart", stringObject);
            }
            var productToReturn = _productRepository.GetProduct(productId);
            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Product = productToReturn,
                PageTilte = "Channel Details"
            };
            

            return View("Details", homeDetailsViewModel);

        }

        [HttpGet]
        public ActionResult AddToCart(int productId, [Optional]int? catId,
            [Optional]int? subcatId, [Optional]int? brandId, [Optional]String searchValue, [Optional]int? priceValue,
            [Optional] int? discountValue, [Optional] int? sortBy, [Optional] int? pageIndex, int PageSize = 2)
        {
            List<Item> cartList = new List<Item>();
            var stringObject = HttpContext.Session.GetString("cart");
            if (stringObject == null)
            {

                var product = _context.Products.Find(productId);
                cartList.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                cartList.OrderBy(b => b.Product.DiscountPrice);
                stringObject = JsonConvert.SerializeObject(cartList);
                HttpContext.Session.SetString("cart", stringObject);
            }
            else
            {
               
                cartList = JsonConvert.DeserializeObject<List<Item>>(stringObject);
                var count = cartList.Count();
                var product = _context.Products.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cartList[i].Product.Id == productId)
                    {
                        int prevQty = cartList[i].Quantity;
                        cartList.Remove(cartList[i]);
                        cartList.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cartList.Where(x => x.Product.Id == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cartList.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                stringObject = JsonConvert.SerializeObject(cartList);
                HttpContext.Session.SetString("cart", stringObject);
            }

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


            if (discountValue != null)
            {
                listProducts.Clear();
                foreach (var pr in products)
                {
                    if (pr.DiscountPercentage >= discountValue * 5)
                    {
                        listProducts.Add(pr);
                    }
                }
                products = listProducts.ToList();
            }


            if (sortBy != null)
            {
                if (sortBy == 1)
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
            int countofProducts = products.Count();
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
            ViewData["Count"] = countofProducts;
            ViewData["pageNumber"] = pageNumber;
            ViewData["pageSize"] = PageSize;

            return View("ProductListing", productListingViewModel3);

        }

        public ActionResult DecreaseQty(int productId,ProductListingViewModel model)
        {
            var stringObject = HttpContext.Session.GetString("cart");
            if (stringObject != null)
            {
                var cartList = JsonConvert.DeserializeObject<List<Item>>(stringObject);
                List<Item> cart = (List<Item>)cartList;
                var product = _context.Products.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.Id == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                stringObject = JsonConvert.SerializeObject(cartList);
                HttpContext.Session.SetString("cart", stringObject);
            }
            return RedirectToAction("Checkout", "Cart");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            var stringObject = HttpContext.Session.GetString("cart");
            var cartList = JsonConvert.DeserializeObject<List<Item>>(stringObject);
            List<Item> cart = (List<Item>)cartList;
            foreach (var item in cart)
            {
                if (item.Product.Id == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            stringObject = JsonConvert.SerializeObject(cartList);
            HttpContext.Session.SetString("cart", stringObject);
            ProductListingViewModel model = new ProductListingViewModel();
            model.product = _context.Products.ToList();
            model.price = _context.Prices.ToList();

            return View("ProductListing", model);
        }

      

        }
}