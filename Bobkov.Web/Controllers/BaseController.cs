using Bobkov.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bobkov.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ILogger<BaseController> logger;
        protected readonly UnitOfWork uow;

        public BaseController(ILogger<BaseController> logger, UnitOfWork uow) : this (logger)
        {
            this.uow = uow;
        }

        public BaseController(ILogger<BaseController> logger)
        {
            this.logger = logger;
        }
    }
}
