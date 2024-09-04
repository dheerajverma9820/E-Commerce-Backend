using AutoMapper;
using ECommerce_app.Services.Concrete;
using ECommerce_app.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ECommerce_app.Controllers
{
    public class BaseApiController : ControllerBase
    {
       
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        protected BaseApiController(IMapper mapper,IHttpContextAccessor httpContextAccessor)
        { 
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        protected string UserId
        {
            get
            {
                var value = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
                return value;
            }
        }

    }
}
