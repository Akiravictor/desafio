using Desafio_Globo.Domain.Models;
using System.Threading.Tasks;

namespace Desafio_Globo.Domain.Interfaces
{
	public interface IAwsRequest
	{
		Task<CpuRequest> GetCpuUsage();

		Task<MemoryRequest> GetMemoryUsage();

		Task<ClusterStatusRequest> GetClusterStatus();
	}
}
