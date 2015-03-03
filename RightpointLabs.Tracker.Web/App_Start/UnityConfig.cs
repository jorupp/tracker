using System;
using System.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using RightpointLabs.Tracker.Domain.Models;
using RightpointLabs.Tracker.Domain.Repositories;
using RightpointLabs.Tracker.Domain.Services;
using RightpointLabs.Tracker.Infrastructure.Persistence;
using RightpointLabs.Tracker.Infrastructure.Persistence.Repositories;
using RightpointLabs.Tracker.Infrastructure.Services;

namespace RightpointLabs.Tracker.Web.App_Start
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();
            
            var connectionString =
                System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            var database = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Mongo"].ProviderName;

            container.RegisterType<IMongoConnectionHandler, MongoConnectionHandler>(
                new ContainerControlledLifetimeManager(),
                new InjectionConstructor(connectionString, database));

            container.RegisterType<IDeviceSnapshotRepository, DeviceSnapshotRepository>();
            container.RegisterType<IDeviceSnapshotService, DeviceSnapshotService>(new InjectionFactory(c =>
                new DeviceSnapshotService(
                    ConfigurationManager.AppSettings["DeviceSnapshotServiceUrl"],
                    ConfigurationManager.AppSettings["DeviceSnapshotServiceUsername"],
                    ConfigurationManager.AppSettings["DeviceSnapshotServicePassword"],
                    ConfigurationManager.AppSettings["DeviceSnapshotServiceSiteId"]
                )));

            // get it running
            container.Resolve<Application.Tracker>();
        }
    }
}
