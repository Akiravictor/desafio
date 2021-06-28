using Desafio_Globo.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Globo.CrossCutting.IoC
{
	public static class DependenciesAwsRequestServices
	{
		public static void RegisterDependencies(this IServiceCollection services)
		{
			services.AddScoped<IAwsRequest, AwsRequest>();
		}
	}
}
