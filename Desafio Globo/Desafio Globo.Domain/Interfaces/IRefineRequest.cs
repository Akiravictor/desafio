using Desafio_Globo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Globo.Domain.Interfaces
{
	public interface IRefineRequest
	{
		AwsUsage BuildAwsUsage(CpuRequest cpuRequest, MemoryRequest memoryRequest, ClusterStatusRequest clusterRequest);
	}
}
