using DeckOfCards.Data;
using System.Web.Http;
using Unity;
using Unity.WebApi;

namespace DeckOfCards
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // Register your repository
            container.RegisterType<IDeckRepository, DeckRepository>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}