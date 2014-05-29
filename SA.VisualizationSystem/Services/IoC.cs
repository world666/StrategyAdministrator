using Ninject;
using Ninject.Parameters;
using SA.VisualizationSystem.ViewModel;

namespace SA.VisualizationSystem.Services
{
    public static class IoC
    {
        static IoC()
        {
            SetBindings();
        }

        public static void RegisterSingleton<TInterface, TClass>() where TClass : TInterface
        {
            _kernel.Bind<TInterface>().To<TClass>().InSingletonScope();
        }

        public static void RegisterInstance<TInterface, TClass>() where TClass : TInterface
        {
            _kernel.Bind<TInterface>().To<TClass>();
        }

        public static T Resolve<T>(params IParameter[] parameters)
        {
            return _kernel.Get<T>(parameters);
        }

        private static void SetBindings()
        {
            IoC.RegisterSingleton<MainVm, MainVm>();
        }

        private static readonly IKernel _kernel = new StandardKernel();
    }
}
