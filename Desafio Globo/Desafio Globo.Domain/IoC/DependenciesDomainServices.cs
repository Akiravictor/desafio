using Desafio_Globo.Domain.Interfaces;
using Desafio_Globo.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Globo.Domain.IoC
{
	public static class DependenciesDomainServices
	{
		public static void RegisterDependencies(this IServiceCollection services)
		{
			services.AddScoped<IRefineRequest, RefineRequest>();
		}
	}
}
