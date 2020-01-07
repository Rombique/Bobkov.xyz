using Bobkov.DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bobkov.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> logger;
        protected readonly IUnitOfWork uow;

        public BaseController(ILogger<BaseController> logger, IUnitOfWork uow) : this (logger)
        {
            this.uow = uow;
        }

        public BaseController(ILogger<BaseController> logger)
        {
            this.logger = logger;
        }
    }
}
