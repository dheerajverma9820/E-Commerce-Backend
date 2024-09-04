using AutoMapper;
using Azure;
using ECommerce_app.CommonRepository;
using ECommerce_app.Data;
using ECommerce_app.Entities;
using ECommerce_app.Generic_Interface;
using ECommerce_app.Models.RequestModel;
using ECommerce_app.Models.ResponseModel;
using ECommerce_app.Repositories.Abstract;
using ECommerce_app.Specification;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using static ECommerce_app.Repositories.Concrete.ProductRepository;

namespace ECommerce_app.Repositories.Concrete
{

    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public ProductRepository(ApplicationDbContext context, IWebHostEnvironment environment, IMapper mapper) : base(context)
        {
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<List<BrandResponseModel>> GetBrand(int id)
        {
            try
            {
                // Fetch the brand and associated data
                var query = from product in _context.Products
                            join brand in _context.Brands on product.BrandId equals brand.Id
                            join img in _context.Images on product.Id equals img.ProductId into productImages
                            where product.BrandId == id
                            select new
                            {
                                BrandName = brand.BrandName,
                                ProductName = product.Name,
                                Price = product.Price,
                                ImagePath = productImages.FirstOrDefault().ImagePath // Assuming you want the first image path if multiple exist
                            };

                var result = await query.ToListAsync();

                // Map to response model
                var response = result.Select(x => new BrandResponseModel
                {
                    BrandName = x.BrandName,
                    Name = x.ProductName,
                    Price = x.Price,
                    ImagePath = x.ImagePath
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                // Log exception or handle as needed
                // For logging, consider using a logging framework like Serilog or NLog
                throw new Exception("An error occurred while fetching the brand data.", ex);
            }
        }
        public async Task<List<BrandResponseModel>> GetBrandsAsync(int brandId, int categoryId)
        {
            try
            {
                // Fetch the brand and associated data
                var query = from product in _context.Products
                            join brand in _context.Brands on product.BrandId equals brand.Id
                            join img in _context.Images on product.Id equals img.ProductId into productImages
                            where product.BrandId == brandId && product.ProductCategoryId == categoryId
                            select new
                            {
                                BrandName = brand.BrandName,
                                ProductName = product.Name,
                                Price = product.Price,
                                ImagePath = productImages.FirstOrDefault().ImagePath // Assuming you want the first image path if multiple exist
                            };

                var result = await query.ToListAsync();

                // Map to response model
                var response = result.Select(x => new BrandResponseModel
                {
                    BrandName = x.BrandName,
                    Name = x.ProductName,
                    Price = x.Price,
                    ImagePath = x.ImagePath
                }).ToList();

                return response;
            }
            catch (Exception ex)
            {
                // Log exception or handle as needed
                // For logging, consider using a logging framework like Serilog or NLog
                throw new Exception("An error occurred while fetching the brand data.", ex);
            }
        }
        //try
        //{
        //    var query = from brnd in _context.Brands
        //                join product in _context.Products on brnd.ProductId equals product.Id
        //                join img in _context.Images on product.Id equals img.ProductId // Assuming img.ProductId is the correct join field
        //                where brnd.BrandName.Contains(brandName)
        //                select new
        //                {
        //                    brnd.BrandName,
        //                    product.Name,
        //                    product.Price,
        //                    img.ImagePath
        //                };
        //    var result = await query.ToListAsync();
        //    // Map to BrandResponseModel
        //    var response = result.Select(x => new BrandResponseModel
        //    {
        //        BrandName = x.BrandName,
        //        Name = x.Name,
        //        Price = x.Price,
        //        ImagePath = x.ImagePath
        //    }).ToList();
        //    return response;
        //}
        //catch (Exception ex)
        //{
        //    throw new Exception("An error occurred while fetching the brand data.", ex);
        //}




        public async Task<int> GetProductStockAsync(int productId)
        {
            return await _context.Inventories
                .Where(inv => inv.ProductId == productId)
                .SumAsync(inv => inv.Quantity);
        }

        public async Task<List<ProductResponse>> DiscountZone(int price)
        {
            var data = await _context.Products
             .Include(p => p.ProductCategory)
             .Include(p => p.Images)
             .Where(p => p.Price <= price)
             //.Where(p => p.Price< 599)
             .ToListAsync();
            var response = data.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ProductCategoryId = p.ProductCategoryId,
                productCategoryName = p.ProductCategory.CategoryName,
                ProductImages = p.Images.Select(img => _mapper.Map<ImageReponse>(img)).ToList()
            }).ToList();
            return response;
        }
      
        public async Task<List<ProductResponse>> GetPaginatedProductsAsync(int pageNumber, int pageSize)
        {
            // Apply eager loading with Include for related entities
            var products = await _context.Products
                .Include(p => p.ProductCategory) // Include ProductCategory
                .Include(p => p.Images)           // Include ProductImages
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            var productResponses = products.Select(p => new ProductResponse
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                ProductCategoryId = p.ProductCategoryId,
                productCategoryName = p.ProductCategory?.CategoryName, // Handle null with conditional access
                ProductImages = p.Images.Select(img => _mapper.Map<ImageReponse>(img)).ToList()
            }).ToList();
            return productResponses;
        }


        public async Task<bool> UpdateInventoryAsync(InventoryUpdateRequest inventoryUpdateRequest)
        {
            var product = await _context.Products.FindAsync(inventoryUpdateRequest.ProductId);
            if (product == null)
                return false; // Product not found
            var inventory = await _context.Inventories
                .FirstOrDefaultAsync(inv => inv.ProductId == inventoryUpdateRequest.ProductId);
            if (inventory == null)
            {
                inventory = new Inventory
                {
                    ProductId = inventoryUpdateRequest.ProductId,
                    Quantity = 0 // Assuming starting quantity is 0 if not found
                };
                _context.Inventories.Add(inventory);
            }
            inventory.Quantity += inventoryUpdateRequest.QuantityChange;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }


        public IEnumerable<Product> FindWithSpecificationPattern(ISpecification<Product> specification)
        {
            var query = SpecificationEvaluator<Product>.GetQuery(_context.Products.AsQueryable(), specification);
            return query.ToList();
        }


        public async Task<ProductCategory> FindProductCategoryAsync(int categoryId)
        {
            // Check if the category already exists
            var existingCategory = await _context.ProductCategorys.FirstOrDefaultAsync(pc => pc.Id == categoryId);

            if (existingCategory != null)
            {
                return existingCategory; // Return existing category if found
            }
            else
            {
                return null;
            }
        }


        public async Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync()
        {
            return await _context.Products
                .Include(p => p.ProductCategory)
                .ToListAsync();
        }


        public async Task<ProductResponse> GetProductWithCategoryAsync(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategory)
                .Where(p => p.Id == id)
                .Select(p => new ProductResponse
                {
                    Name = p.Name,
                    Price = p.Price,
                    productCategoryName = p.ProductCategory.CategoryName
                })
                .FirstOrDefaultAsync();
            return product;
        }


        //public async Task<ProductResponseModel> GetProductWithCategoryAsync(int id)
        //{
        //    var product = await _context.Products
        //        .Include(p => p.ProductCategory)
        //        .FirstOrDefaultAsync(p => p.Id == id);

        //    var productResponse = _mapper.Map<ProductResponseModel>(product);

        //    return productResponse;
        //}



        public async Task<string> UploadImage(ImageRequest imageRequest, List<IFormFile> files)
        {
            var imageUrls = new List<string>();
            try
            {
                var product = await _context.Products.FindAsync(imageRequest.ProductId);
                if (product == null)
                {
                    throw new ArgumentException("Product not found.");
                }
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "Image");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);
                foreach (var file in files)
                {
                    var uniqueFileName = Guid.NewGuid().ToString() + ".jpg"; // Assuming image format is JPG
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream); // Copy the contents of the uploaded file to fileStream
                    }
                    var imageUrl = $"/Image/{uniqueFileName}";
                    var image = new Image
                    {
                        ImgName = imageRequest.ImgName, // Use file name without extension as ImgName
                        ImagePath = imageUrl,
                        ProductId = product.Id
                    };
                    await _context.Images.AddAsync(image);
                    await _context.SaveChangesAsync();
                    imageUrls.Add(imageUrl);
                }
                return imageUrls.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception($"Upload failed: {ex.Message}");
            }
        }


        public async Task<ProductWithImageResponse> GetProductByIdAsync(int id)
        {
            var result = await _context.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductCategory)
                .Include(p => p.Images)
                .FirstOrDefaultAsync();
            if (result == null)
            {
                return null; // Or throw an exception, depending on your error handling strategy
            }
            var productResponse = new ProductResponse
            {
                Id = result.Id,
                Name = result.Name,
                Price = result.Price,
                ProductCategoryId = result.ProductCategory.Id,
                productCategoryName = result.ProductCategory.CategoryName
            };
            var imageResponses = result.Images.Select(img => new ImageReponse
            {
                Id = img.Id,
                imgName = img.ImgName,
                imgURL = img.ImagePath
            }).ToList();
            var productWithImageResponse = new ProductWithImageResponse
            {
                ProductResponse = productResponse,
                ImageReponse = imageResponses
            };
            return productWithImageResponse;
        }


    //    public async Task<List<ProductResponse>> GetAllDataAsync(int ? pageNumber, int ? pageSize, int? categoryId, int? brandId)
       // {
            public async Task<List<ProductResponse>> GetAllDataAsync(List<int> categoryIds, List<int>? brandIds,  int? pageNumber, int? pageSize)
            {
                IQueryable<Product> query = _context.Products
                    .Include(p => p.ProductCategory)
                    .Include(p => p.Images);

                // Apply filters based on categoryIds and brandIds
                if (categoryIds != null && categoryIds.Any())
                {
                    query = query.Where(p => categoryIds.Contains(p.ProductCategoryId));
                }

                if (brandIds != null && brandIds.Any())
                {
                    query = query.Where(p => brandIds.Contains(p.BrandId));
                }

                // Apply pagination
                if (pageNumber.HasValue && pageSize.HasValue && pageSize.Value > 0)
                {
                    int skip = (pageNumber.Value - 1) * pageSize.Value;
                    query = query.Skip(skip).Take(pageSize.Value);
                }

                var data = await query.ToListAsync();

                var response = data.Select(p => new ProductResponse
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ProductCategoryId = p.ProductCategoryId,
                    productCategoryName = p.ProductCategory.CategoryName,
                    ProductImages = p.Images.Select(img => _mapper.Map<ImageReponse>(img)).ToList()
                }).ToList();

                return response;
            }

            //var data = await _context.Products
            //  .Include(p => p.ProductCategory)
            //  .Include(p => p.Images)
            //  .ToListAsync();
            //var response = data.Select(p => new ProductResponse
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Price = p.Price,
            //    ProductCategoryId = p.ProductCategoryId,
            //    productCategoryName = p.ProductCategory.CategoryName,
            //    ProductImages = p.Images.Select(img => _mapper.Map<ImageReponse>(img)).ToList()
            //}).ToList();
            //return response;


            public Task<List<Brand>> GetAllBrand()
        {
            return  _context.Brands.ToListAsync();
            // throw new NotImplementedException();
        }

       





        //public Task<ProductCategory> GetByIdProductWithCategoryAsync(int ProductCategoryId)
        //{
        //    try
        //    {
        //        var data =  _context.ProductCategorys
        //            .Include(pc => pc.Products)
        //            .Include(pc => pc.Images)
        //            .FirstOrDefaultAsync(pc => pc.Id == ProductCategoryId);

        //        if (data == null)
        //        {
        //            return NotFound(); // Return 404 Not Found if product category with given ID is not found
        //        }

        //        // Mapping products and their associated image URLs
        //        var products = data.Products.Select(p => new ProductResponse
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            Price = p.Price,
        //          ProductImages = p.Images.Select(img => _mapper.Map<ImageReponse>(img)).ToList()
        //        }).ToList();

        //        // Creating response object
        //        var response = new
        //        {
        //            ProductCategoryId = data.Id,
        //            Products = products
        //        };

        //        return Ok(response); // Return 200 OK with the response object
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception or handle it accordingly
        //        Console.WriteLine($"Exception occurred in GetByIdProductWithCategoryAsync: {ex.Message}");
        //        return StatusCode(500, "Internal server error"); // Return 500 Internal Server Error for any exception
        //    }
    }
    }

        




//    public async Task<ProductCategory> FindProductCategoryAsync(int categoryId)
//    { 
//        var existingCategory = await Context.ProductCategorys.FirstOrDefaultAsync(pc => pc.Id == categoryId);

//        if (existingCategory != null)
//        {
//            return existingCategory; // Return existing category if found
//        }
//        else
//        {
//            return null;
//        }
//    }


//    public async Task<IEnumerable<Product>> GetAllProductsWithCategoriesAsync()
//    {
//        return await Context.Products
//            .Include(p => p.ProductCategory)
//            .ToListAsync();
//    }

//    public async Task<Product> GetProductWithCategoryAsync(int id)
//    {
//        return await Context.Products
//            .Include(p => p.ProductCategory)
//            .FirstOrDefaultAsync(p => p.Id == id);
//    }
//    public async Task AddAsync(Product product)
//    {
//        await Context.Products.AddAsync(product);
//        await Context.SaveChangesAsync();
//    }
//}



































//public async Task<IEnumerable<Product>> GetProductsWithCategoriesAsync()
//{
//    return await Context.Products
//        .Include(p => p.ProductCategory)
//        .ToListAsync();
//}

//public async Task<Product> GetProductWithCategoriesAsync(int id)
//{
//    return await Context.Products
//        .Include(p => p.ProductCategory)
//        .FirstOrDefaultAsync(p => p.Id == id);
//}


