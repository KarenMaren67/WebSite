using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Host.Controllers
{
    public class BaseController : Controller
    {
		private IMapper _mapper;

		protected BaseController() { }

		protected IMapper Mapper { get {return _mapper ?? (_mapper = HttpContext.RequestServices.GetService<IMapper>()); } }
    }
}