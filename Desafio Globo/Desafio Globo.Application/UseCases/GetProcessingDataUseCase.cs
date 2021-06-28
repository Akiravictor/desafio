using Desafio_Globo.Application.Interfaces;
using Desafio_Globo.Domain.Interfaces;
using Desafio_Globo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Globo.Application.UseCases
{
	public class GetProcessingDataUseCase : IGetProcessingDataUseCase
	{
		private readonly IAwsRequest awsRequest;
		private readonly IRefineRequest refineRequest;

		public GetProcessingDataUseCase(IAwsRequest awsRequest,
			IRefineRequest refineRequest)
		{
			this.awsRequest = awsRequest;
			this.refineRequest = refineRequest;
		}

		public async Task<AwsUsage> ExecuteAsync()
		{
			var cpuUsage = await awsRequest.GetCpuUsage();
			var memoryUsage = await awsRequest.GetMemoryUsage();
			var clusterStatus = await awsRequest.GetClusterStatus();

			return refineRequest.BuildAwsUsage(cpuUsage, memoryUsage, clusterStatus);
		}
	}
}
