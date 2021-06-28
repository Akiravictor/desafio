using Desafio_Globo.Domain.Interfaces;
using Desafio_Globo.Domain.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Desafio_Globo.CrossCutting
{
	public class AwsRequest : IAwsRequest
	{
		private readonly IConfiguration config;
		private readonly IHttpClientFactory httpClient;

		public AwsRequest(IConfiguration config,
			IHttpClientFactory httpClient)
		{
			this.config = config;
			this.httpClient = httpClient;
		}

		public async Task<CpuRequest> GetCpuUsage()
		{
			var endpoint = config.GetSection("CpuUsageEndpoint").Value.ToString();

			var client = httpClient.CreateClient();

			var response = await client.GetAsync(endpoint);

			return (CpuRequest)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(CpuRequest));
		}

		public async Task<MemoryRequest> GetMemoryUsage()
		{
			var endpoint = config.GetSection("MemoryUsageEndpoint").Value.ToString();

			var client = httpClient.CreateClient();

			var response = await client.GetAsync(endpoint);

			return (MemoryRequest)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(MemoryRequest));
		}

		public async Task<ClusterStatusRequest> GetClusterStatus()
		{
			var endpoint = config.GetSection("ClusterStatusEndpoint").Value.ToString();

			var client = httpClient.CreateClient();

			var response = await client.GetAsync(endpoint);

			return (ClusterStatusRequest)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync(), typeof(ClusterStatusRequest));
		}
	}
}
