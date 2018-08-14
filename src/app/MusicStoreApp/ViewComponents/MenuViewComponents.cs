using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Features.Indexed;
using Microsoft.AspNetCore.Mvc;
using MusicStoreApp.Core;
using Newtonsoft.Json;

namespace MusicStoreApp.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IComponentContext _componentContext;

        public MenuViewComponent(IComponentContext componentContext)
        {
            _componentContext = componentContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
