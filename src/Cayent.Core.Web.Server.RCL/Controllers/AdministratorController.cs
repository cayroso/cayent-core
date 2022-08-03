
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Configuration;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;


//namespace Cayent.Core.Web.Server.RCL.Controllers
//{
//    [Authorize]
//    [ApiController]
//    [Route("api/[controller]")]
//    [Produces("application/json")]
//    public class AdministratorController : BaseController
//    {                
//        public AdministratorController()
//        {
//            //_queryHandlerDispatcher = queryHandlerDispatcher ?? throw new ArgumentNullException(nameof(queryHandlerDispatcher));
//        }

//        [HttpGet("search")]
//        public async Task<IActionResult> SearchParents(string? c, int p, int s, string? sf, int? so, CancellationToken cancellationToken)
//        {
//            var tenantId = TenantIdFromClaims;
//            var tenantId2 = TenantIdFromRouteValues;

//            var sql = from parent in _appDbContext.Parents.AsNoTracking()
//                                        .Include(e => e.User)

//                      select new PagedParentItemDto
//                      {
//                          ParentId = parent.ParentId,
//                          Name = parent.User.FirstLastName
//                      };

//            var dto = await sql.ToPagedItemsAsync(p, s, cancellationToken);

//            return Ok(dto);
//        }

//    }


//}
