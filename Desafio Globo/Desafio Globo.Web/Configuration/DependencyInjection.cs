using Desafio_Globo.IoC;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio_Globo.Web.Configuration
{
	public static class DependencyInjection
	{
		public static void AddDependenciesConfiguration(this IServiceCollection services)
		{
			if (services == null)
				throw new ArgumentNullException(nameof(services));

			NativeInjector.RegisterServices(services);
		}
	}
}
