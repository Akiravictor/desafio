using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Globo.Domain.Models
{
	public class CpuRequest
	{
		public List<string> Labels { get; set; }

		public List<int> Data { get; set; }
	}
}
