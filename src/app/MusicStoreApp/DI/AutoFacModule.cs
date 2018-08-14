using Autofac;
using MusicStoreApp.Core;

namespace MusicStoreApp.DI
{
    public class AutoFacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.Register((c, p) => new MenuItem(p.Positional<string>(0))).Keyed<MenuComponent>(MenuType.Item);
            //builder.Register((c, p) => new MenuGroup(p.Positional<string>(0))).Keyed<MenuComponent>(MenuType.Composite);
        }
    }
}