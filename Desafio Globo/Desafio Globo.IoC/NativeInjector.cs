using Desafio_Globo.Application.IoC;
using Desafio_Globo.CrossCutting.IoC;
using Desafio_Globo.Domain.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Globo.IoC
{
	public static class NativeInjector
	{
		public static void RegisterServices(IServiceCollection services)
		{
			DependenciesAwsRequestServices.RegisterDependencies(services);
			DependenciesDomainServices.RegisterDependencies(services);
			DependenciesApplicationServices.RegisterDependencies(services);
		}
	}
}
