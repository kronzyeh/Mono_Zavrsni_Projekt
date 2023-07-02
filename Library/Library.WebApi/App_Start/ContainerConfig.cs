using Autofac;
using Autofac.Integration.WebApi;
using Library.Repository;
using Library.Repository.Common;
using Library.Service;
using Library.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Library.WebApi.App_Start
{
    public class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();


            builder.RegisterType<TypeService>().As<ITypeService>();
            builder.RegisterType<TypeRepository>().As<ITypeRepository>();
            builder.RegisterType<PublisherService>().As<IPublisherService>();
            builder.RegisterType<PublisherRepository>().As<IPublisherRepository>();
            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>();
            builder.RegisterType<RegistrationService>().As<IRegistrationService>().InstancePerDependency();
            builder.RegisterType<RegistrationRepository>().As<IRegistrationRepository>().InstancePerDependency();
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerDependency();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerDependency();
            builder.RegisterType<PublicationService>().As<IPublicationService>().InstancePerDependency();
            builder.RegisterType<PublicationRepository>().As<IPublicationRepository>().InstancePerDependency();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).InstancePerDependency();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();

            builder.RegisterType<SubscriptionHistoryService>().As<ISubscriptionHistoryService>().InstancePerDependency();
            builder.RegisterType<SubscriptionHistoryRepository>().As<ISubscriptionHistoryRepository>().InstancePerDependency();

            builder.RegisterType<TypeService>().As<ITypeService>();
            builder.RegisterType<TypeRepository>().As<ITypeRepository>();

            builder.RegisterType<PublisherService>().As<IPublisherService>();
            builder.RegisterType<PublisherRepository>().As<IPublisherRepository>();

            builder.RegisterType<GenreService>().As<IGenreService>();
            builder.RegisterType<GenreRepository>().As<IGenreRepository>();

            builder.RegisterType<RegistrationService>().As<IRegistrationService>().InstancePerDependency();
            builder.RegisterType<RegistrationRepository>().As<IRegistrationRepository>().InstancePerDependency();

            builder.RegisterType<ReservationService>().As<IReservationService>().InstancePerDependency();
            builder.RegisterType<ReservationRepository>().As<IReservationRepository>().InstancePerDependency();

            return builder.Build();
        }
    }
}