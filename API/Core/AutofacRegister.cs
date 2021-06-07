using Autofac;
using System.Reflection;
using Module = Autofac.Module;

namespace API.Core
{
    /// <summary>
    /// 通过反射的方式实现注入
    /// </summary>
    public class AutofacRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("BLL")).Where(c => c.Name.EndsWith("Bll")).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("DAL")).Where(c => c.Name.EndsWith("Dal"))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(Assembly.Load("API")).Where(c => c.Name.EndsWith("Controller"))
                .AsImplementedInterfaces();
        }

    }
}
