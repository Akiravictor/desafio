using Desafio_Globo.Application.Interfaces;
using Desafio_Globo.Application.UseCases;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Globo.Application.IoC
{
	public static class DependenciesApplicationServices
	{
		public static void RegisterDependencies(this IServiceCollection services)
		{
			services.AddScoped<IGetProcessingDataUseCase, GetProcessingDataUseCase>();
		}
	}
}
