using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Chinook.UnitOfWork;

namespace Chinook.WebApi.Controllers
{
    [Produces("application/json")]
    [Authorize]
    public class BaseController : Controller
    {
        protected IUnitOfWork _unit;
        public BaseController(IUnitOfWork unit)
        {
            _unit = unit;
        }

    }
}