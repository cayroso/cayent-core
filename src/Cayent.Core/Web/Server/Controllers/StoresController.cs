//using App.CQRS.Navbar.Common.Queries.Query;
//using Cayent.Core.Common;
//using Cayent.Core.CQRS.Queries;
//using Cayent.Core.Data.Fileuploads;
//using Data.App.DbContext;
//using Data.Identity.DbContext;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Newtonsoft.Json;
//using System;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;
//using Cayent.Core.Common.Extensions;
//using Data.App.Models.Stores;
//using Data.App.Models.Items;
//using System.Collections.Generic;
//using Data.App.Models.Products;
//using Web.Models.Stores;
//using Data.App.Models.Stocks;

//namespace Web.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("api/[controller]")]
//    [Produces("application/json")]
//    public class StoresController : BaseController
//    {
//        readonly IQueryHandlerDispatcher _queryHandlerDispatcher;

//        public StoresController(IQueryHandlerDispatcher queryHandlerDispatcher)
//        {
//            _queryHandlerDispatcher = queryHandlerDispatcher ?? throw new ArgumentNullException(nameof(queryHandlerDispatcher));
//        }

//        [HttpGet("search")]
//        public async Task<IActionResult> GetBranchStores(string c, int p, int s, string sf, int so, [FromServices] AppDbContext dbContext, CancellationToken cancellationToken = default)
//        {
//            var sql = from bs in dbContext.Stores
//                      .Include(e => e.StoreProducts)
//                        .ThenInclude(e => e.Product)
//                      .AsNoTracking()

//                      select new
//                      {
//                          bs.StoreId,
//                          bs.Name,
//                          bs.Address,
//                          bs.Description,
//                          Products = bs.StoreProducts.Count()//.Select(e => e.Product.Item.Name)
//                      };

//            var dto = await sql.ToPagedItemsAsync(p, s, cancellationToken);

//            return Ok(dto);
//        }

//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetStores(string id, [FromServices] AppDbContext dbContext, CancellationToken cancellationToken = default)
//        {
//            //var sqlStoreStocks = from ss in dbContext.StoreStocks
//            //                     .Include(e => e.Stock)
//            //                        .ThenInclude(e => e.Item)
//            //                     .AsNoTracking()

//            //                     where ss.StoreId == id

//            //                     select new StoreInfo.StoreStock
//            //                     {
//            //                         StockId = ss.StockId,
//            //                         ItemId = ss.Stock.ItemId,
//            //                         Serial = ss.Stock.Serial,
//            //                         Quantity = ss.Quantity,
//            //                         SafetyQuantity = ss.SafetyQuantity,
//            //                         ReorderQuantity = ss.ReorderQuantity,
//            //                     };

//            //var storeStocks = await sqlStoreStocks.ToListAsync();

//            var sqlStoreProducts = from sp in dbContext.StoreProducts
//                                   .Include(e => (e).Product)
//                                        .ThenInclude(e => (e as Product).ProductImages)
//                                   .Include(e => e.Product)
//                                        .ThenInclude(e => (e as Product).ProductPrices)
//                                   .AsNoTracking()

//                                   where sp.StoreId == id

//                                   select new StoreInfo.StoreProduct
//                                   {
//                                       ProductId = sp.Product.ProductId,
//                                       Name = sp.Product.Item.Name,
//                                       MultiPack = sp.Product.MultiPack,
//                                       PrimaryImageUrl = sp.Product.PrimaryImageUrl,
//                                       Price = sp.Product.ProductPrices.First().Price
//                                   };

//            var storeProducts = await sqlStoreProducts.ToListAsync();





//            var sql = from bs in dbContext.Stores.AsNoTracking()
//                          //.Include(e => e.StoreProducts)
//                          //    .ThenInclude(e => (e as StoreProduct).Product)
//                          //        .ThenInclude(e => (e as Product).ProductImages)
//                          //.Include(e => e.StoreProducts)
//                          //    .ThenInclude(e => (e as StoreProduct).Product)
//                          //        .ThenInclude(e => (e as Product).ProductPrices)
//                          //.Include(e => e.StoreProducts)
//                          //    .ThenInclude(e => (e as StoreProduct).Product)
//                          //        .ThenInclude(e => e.Item)

//                      where bs.StoreId == id

//                      select new StoreInfo
//                      {
//                          StoreId = bs.StoreId,
//                          Name = bs.Name,
//                          Description = bs.Description,
//                          Address = bs.Address,
//                          GeoX = bs.GeoX,
//                          GeoY = bs.GeoY,
//                          Token = bs.ConcurrencyToken
//                      };

//            var dto = await sql.FirstOrDefaultAsync(cancellationToken);

//            //storeProducts.ForEach(sp =>
//            //{
//            //    sp.Stocks = storeStocks.Where(e => e.ItemId == sp.ProductId).ToList();
//            //});

//            dto.Products = storeProducts;


//            return Ok(dto);
//        }

//        [HttpPost]
//        public async Task<IActionResult> AddStoreAsync([FromBody] AddStoreInfo info, [FromServices] AppDbContext dbContext, CancellationToken cancellationToken = default)
//        {
//            var branchStore = new Store
//            {
//                StoreId = GuidStr(),
//                Name = info.Name,
//                Description = info.Description,
//                Address = info.Address,
//                GeoX = info.GeoX,
//                GeoY = info.GeoY,
//                Active = true,
//            };

//            await dbContext.AddAsync(branchStore, cancellationToken);

//            await dbContext.SaveChangesAsync(cancellationToken);

//            return Ok(branchStore.StoreId);
//        }

//        [HttpPut]
//        public async Task<IActionResult> EditStoreAsync([FromBody] EditStoreInfo info, [FromServices] AppDbContext dbContext, CancellationToken cancellationToken = default)
//        {
//            var data = await dbContext.Stores.FirstOrDefaultAsync(e => e.StoreId == info.StoreId, cancellationToken);

//            //data.ThrowIfNullOrAlreadyUpdated(info.Token, GuidStr());

//            data.Name = info.Name;
//            data.Description = info.Description;
//            data.Address = info.Address;
//            data.GeoX = info.GeoX;
//            data.GeoY = info.GeoY;

//            await dbContext.SaveChangesAsync(cancellationToken);

//            return Ok(info.StoreId);
//        }


//        [HttpPost("set-branch-store/{id}")]
//        public async Task<IActionResult> SetStore(string id, [FromServices] AppDbContext dbContext, CancellationToken cancellationToken = default)
//        {
//            var data = await dbContext.Stores.FirstOrDefaultAsync(e => e.StoreId == id, cancellationToken);

//            if (data == null)
//                return NotFound();

//            Response.Cookies.Append("StoreId", data.StoreId, new CookieOptions { Secure = true, HttpOnly = true });
//            Response.Cookies.Append("StoreName", data.Name, new CookieOptions { Secure = true, HttpOnly = true });

//            return Ok();
//        }


//        [HttpGet("{id}/unassigned")]
//        public async Task<IActionResult> GetUnassignedProducts(string id, [FromServices] AppDbContext dbContext, CancellationToken cancellationToken = default)
//        {
//            var sql = from p in dbContext.Products.AsNoTracking()

//                      where !dbContext.StoreProducts.Any(e => e.StoreId == id && e.ProductId == p.ProductId)

//                      select new
//                      {
//                          p.ProductId,
//                          p.Item.Name,
//                          p.PrimaryImageUrl
//                      };
//            var data = await sql.ToListAsync(cancellationToken);

//            return Ok(data);
//        }

//        [HttpPut("assign-product")]
//        public async Task<IActionResult> AssignProducts([FromBody] AssignStoreProductInfo info, [FromServices] AppDbContext dbContext, CancellationToken cancellationToken = default)
//        {
//            var existing = await dbContext.StoreProducts
//                .FirstOrDefaultAsync(e => e.StoreId == info.StoreId && e.ProductId == info.ProductId)
//                ;

//            if (existing == null)
//            {
//                var newStoreProduct = new StoreProduct
//                {
//                    StoreId = info.StoreId,
//                    ProductId = info.ProductId
//                };

//                //var stock = await dbContext.Stocks.FirstOrDefaultAsync(e => e.ItemId == info.ProductId);

//                //if (stock == null)
//                //{
//                //    stock = new Stock
//                //    {
//                //        StockId = GuidStr(),
//                //        ItemId = info.ProductId,
//                //        Serial = "",
//                //    };

//                //    await dbContext.AddAsync(stock, cancellationToken);
//                //}


//                //var newStoreStock = new StoreStock
//                //{
//                //    StoreId = info.StoreId,
//                //    StockId = stock.StockId,
//                //};

//                //await dbContext.AddRangeAsync(new object[] { newStoreProduct, newStoreStock }, cancellationToken);

//                await dbContext.AddAsync(newStoreProduct, cancellationToken);

//                await dbContext.SaveChangesAsync(cancellationToken);
//            }

//            return Ok();
//        }

//        [HttpPut("unassign-product")]
//        public async Task<IActionResult> UnssignProduct([FromBody] UnassignStoreProductInfo info, [FromServices] AppDbContext dbContext, CancellationToken cancellationToken = default)
//        {

//            var existing = await dbContext.StoreProducts
//                .FirstOrDefaultAsync(e => e.StoreId == info.StoreId && e.ProductId == info.ProductId)
//                ;

//            if (existing != null)
//            {
//                dbContext.Remove(existing);

//                //var storeStocks = await dbContext.StoreStocks.Include(e => e.Stock).Where(e => e.StoreId == info.StoreId && e.Stock.ItemId == info.ProductId).ToListAsync();

//                //if (storeStocks != null)
//                //{
//                //    dbContext.RemoveRange(storeStocks);
//                //    var stocks = storeStocks.Select(e => e.Stock);

//                //    dbContext.RemoveRange(stocks);
//                //}

//                await dbContext.SaveChangesAsync(cancellationToken);
//            }

//            return Ok();
//        }
//    }


//}



//[ApiController]
//[Route("api/[controller]")]
//[Produces("application/json")]
//public class FilesController : BaseController
//{
//    [HttpGet("{id}")]
//    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
//    public async Task<IActionResult> GetFileAsStreamAsync([FromServices] AppDbContext appDbContext, string id)
//    {

//        var data = await appDbContext.FileUploads
//            .AsNoTracking()
//            .FirstOrDefaultAsync(p => p.FileUploadId == id);

//        if (data == null)
//            return NotFound();

//        var stream = new MemoryStream(data.Content.Length);

//        stream.Write(data.Content, 0, data.Content.Length);
//        stream.Position = 0;

//        return File(stream, data.ContentType, data.FileName, true);
//    }

//    [HttpGet("{tenantId}/{id}")]
//    [ResponseCache(Duration = 600, Location = ResponseCacheLocation.Any)]
//    public async Task<IActionResult> GetFileAsStreamAsync(
//        [FromServices] IConfiguration configuration,
//        [FromServices] DbContextOptions<AppDbContext> options,
//        [FromServices] IdentityWebContext dbContext, string tenantId, string id)
//    {

//        var tenant = await dbContext
//            .Tenants
//            .AsNoTracking()
//            .SingleOrDefaultAsync(p => p.TenantId == tenantId);

//        if (tenant == null)
//            return NotFound();

//        var dummyProvider = new DummyTenantProvider(tenant);
//        var appDbContext = new AppDbContext(options, configuration, dummyProvider);

//        var data = await appDbContext.FileUploads
//                    .AsNoTracking()
//                    .FirstOrDefaultAsync(e => e.FileUploadId == id);

//        if (data == null)
//            return NotFound();

//        if (data.Content == null)
//            return NotFound();

//        var stream = new MemoryStream(data.Content.Length);

//        stream.Write(data.Content, 0, data.Content.Length);
//        stream.Position = 0;

//        return File(stream, data.ContentType, data.FileName, true);
//    }
//}