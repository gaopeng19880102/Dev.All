// ***********************************************************************************
// Created by zbw911 
// 创建于：2012年12月18日 13:28
// 
// 修改于：2013年02月18日 18:24
// 文件名：NInjectContainerAdapter.cs
// 
// 如果有更好的建议或意见请邮件至zbw911#gmail.com
// ***********************************************************************************
 

using System;
using Kt.Framework.Repository.Configuration;
using Ninject;

namespace Kt.Framework.Repository.ContainerAdapter.NInject
{
    /// <summary>
    /// <see cref="IContainerAdapter"/> implementation for NInject.
    /// </summary>
    public class NInjectContainerAdapter : IContainerAdapter
    {
        readonly IKernel _kernel;

        /// <summary>
        /// Default Constructor.
        /// Creates an instance of <see cref="NInjectContainerAdapter"/> class.
        /// </summary>
        /// <param name="kernel">The <see cref="IKernel"/> instance used by the NInjectContainerAdapter
        /// to register components.</param>
        public NInjectContainerAdapter(IKernel kernel)
        {
            _kernel = kernel;
        }

        /// <summary>
        /// Registers a default implementation type for a service type.
        /// </summary>
        /// <typeparam name="TService">The <typeparamref name="TService"/> type representing the service
        /// for which the implementation type is registered. </typeparam>
        /// <typeparam name="TImplementation">The <typeparamref name="TImplementation"/> type representing
        /// the implementation registered for the <typeparamref name="TService"/></typeparam>
        public void Register<TService, TImplementation>() where TImplementation : TService
        {
            Register(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Registers a named implementation type of a service type.
        /// </summary>
        /// <typeparam name="TService">The <typeparamref name="TService"/> type representing the service
        /// for which the implementation type is registered. </typeparam>
        /// <typeparam name="TImplementation">The <typeparamref name="TImplementation"/> type representing
        /// the implementation registered for the <typeparamref name="TService"/></typeparam>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        public void Register<TService, TImplementation>(string named) where TImplementation : TService
        {
            Register(typeof(TService), typeof(TImplementation), named);
        }

        /// <summary>
        /// Registers a default implementation type for a service type.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing the service for which the
        ///  implementation type is registered.</param>
        /// <param name="implementation"><see cref="Type"/>. The type representing the implementation
        /// registered for the service type.</param>
        public void Register(Type service, Type implementation)
        {
            _kernel.Bind(service).To(implementation);
        }

        /// <summary>
        /// Registers a named implementation type for a service type.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing the service fow which the
        /// implementation type if registered.</param>
        /// <param name="implementation"><see cref="Type"/>. The type representing the implementaton
        /// registered for the service.</param>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        public void Register(Type service, Type implementation, string named)
        {
            _kernel.Bind(service).To(implementation).Named(named);
        }

        ///<summary>
        /// Registers a open generic implementation for a generic service type.
        ///</summary>
        ///<param name="service">The type representing the service for which the implementation type is registered.</param>
        ///<param name="implementation">The type representing the implementation registered for the service.</param>
        public void RegisterGeneric(Type service, Type implementation)
        {
            Register(service, implementation);
            //_kernel.Bind(service).To(implementation);
        }

        ///<summary>
        /// Registers a named open generic implementation for a generic service type.
        ///</summary>
        ///<param name="service">The type representing the service for which the implementation is registered.</param>
        ///<param name="implementation">The type representing the implementation registered for the service.</param>
        ///<param name="named">string. The service name with which the implementation is registerd.</param>
        public void RegisterGeneric(Type service, Type implementation, string named)
        {
            //_kernel.Bind(service).To(implementation).Named(named);
            Register(service, implementation, named);
        }

        /// <summary>
        /// Registers a default implementation type for a service type as a singleton.
        /// </summary>
        /// <typeparam name="TService"><typeparamref name="TService"/>. The type representing the service
        /// for which the implementation type is registered as a singleton.</typeparam>
        /// <typeparam name="TImplementation"><typeparamref name="TImplementation"/>. The type representing
        /// the implementation that is registered as a singleton for the service type.</typeparam>
        public void RegisterSingleton<TService, TImplementation>() where TImplementation : TService
        {
            RegisterSingleton(typeof(TService), typeof(TImplementation));
        }

        /// <summary>
        /// Registers a named implementation type for a service type as a singleton.
        /// </summary>
        /// <typeparam name="TService"><typeparamref name="TService"/>. The type representing the service
        /// for which the implementation type is registered as a singleton.</typeparam>
        /// <typeparam name="TImplementation"><typeparamref name="TImplementation"/>. The type representing
        /// the implementation that is registered as a singleton for the service type.</typeparam>
        /// <param name="named">string. The service name with which the implementation is registerd.</param>
        public void RegisterSingleton<TService, TImplementation>(string named) where TImplementation : TService
        {
            RegisterSingleton(typeof(TService), typeof(TImplementation), named);
        }

        /// <summary>
        /// Registers a default implementation type for a service type as a singleton.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing the service
        /// for which the implementation type is registered as a singleton.</param>
        /// <param name="implementation"><see cref="Type"/>. The type representing
        /// the implementation that is registered as a singleton for the service type.</param>
        public void RegisterSingleton(Type service, Type implementation)
        {
            _kernel.Bind(service).To(implementation).InSingletonScope();
        }

        /// <summary>
        /// Registers a named implementation type for a service type as a singleton.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing the service
        /// for which the implementation type is registered as a singleton.</param>
        /// <param name="implementation"><see cref="Type"/>. The type representing
        /// the implementation that is registered as a singleton for the service type.</param>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        public void RegisterSingleton(Type service, Type implementation, string named)
        {
            _kernel.Bind(service).To(implementation).InSingletonScope().Named(named);
        }

        /// <summary>
        /// Registers an instance as an implementation for a service type.
        /// </summary>
        /// <typeparam name="TService"><typeparamref name="TService"/>. The type representing
        /// the service for which the instance is registered.</typeparam>
        /// <param name="instance">An instance of type <typeparamref name="TService"/> that is
        /// registered as an instance for <typeparamref name="TService"/>.</param>
        public void RegisterInstance<TService>(TService instance) where TService : class
        {
            RegisterInstance(typeof(TService), instance);
        }

        /// <summary>
        /// Registers an named instance as an implementation for a service type.
        /// </summary>
        /// <typeparam name="TService"><typeparamref name="TService"/>. The type representing
        /// the service for which the instance is registered.</typeparam>
        /// <param name="instance">An instance of type <typeparamref name="TService"/> that is
        /// registered as an instance for <typeparamref name="TService"/>.</param>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        public void RegisterInstance<TService>(TService instance, string named) where TService : class
        {
            RegisterInstance(typeof(TService), instance, named);
        }

        /// <summary>
        /// Registers an instance as an implementation for a service type.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing
        /// the service for which the instance is registered.</param>
        /// <param name="instance">An instance of <paramref name="service"/> that is
        /// registered as an instance for the service.</param>
        public void RegisterInstance(Type service, object instance)
        {
            _kernel.Bind(service).ToConstant(instance);
        }

        /// <summary>
        /// Registers a named instance as an implementation for a service type.
        /// </summary>
        /// <param name="service"><see cref="Type"/>. The type representing
        /// the service for which the instance is registered.</param>
        /// <param name="instance">An instance of <paramref name="service"/> that is
        /// registered as an instance for the service.</param>
        /// <param name="named">string. The service name with which the implementation is registered.</param>
        public void RegisterInstance(Type service, object instance, string named)
        {
            _kernel.Bind(service).ToConstant(instance).Named(named);
        }
    }
}