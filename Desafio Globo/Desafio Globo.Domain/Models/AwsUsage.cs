using Desafio_Globo.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Globo.Domain.Models
{
	public class AwsUsage
	{
		public CpuData CpuUsage { get; set; }

		public MemoryData MemoryUsage { get; set; }

		public ClusterStatus ClusterUsage { get; set; }
	}
}
