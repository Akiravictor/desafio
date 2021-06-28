using Desafio_Globo.Domain.Entities;
using Desafio_Globo.Domain.Interfaces;
using Desafio_Globo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Desafio_Globo.Domain.Services
{
	public class RefineRequest : IRefineRequest
	{
		public RefineRequest()
		{

		}

		public AwsUsage BuildAwsUsage(CpuRequest cpuRequest, MemoryRequest memoryRequest, ClusterStatusRequest clusterRequest)
		{
			var response = new AwsUsage();

			response.CpuUsage = new CpuData();
			response.MemoryUsage = new MemoryData();

			response.CpuUsage.Data = string.Join(',', cpuRequest.Data);
			response.CpuUsage.Labels = $"\"{string.Join("\",\"", cpuRequest.Labels)}\"";

			for(int i = 0; i < memoryRequest.Data.Count; i++)
			{
				memoryRequest.Data[i] = Math.Round(memoryRequest.Data[i], 1);
			}

			response.MemoryUsage.Data = string.Join(',', memoryRequest.Data);
			response.MemoryUsage.Labels = $"\"{string.Join("\",\"", memoryRequest.Labels)}\"";

			response.MemoryUsage.MinData = $"{Math.Round(memoryRequest.Data.Min(), 0) - 1}";
			response.MemoryUsage.MaxData = $"{Math.Round(memoryRequest.Data.Max(), 0) + 1}";

			response.ClusterUsage = new ClusterStatus() { Status = clusterRequest.Status };

			return response;
		}
	}
}
