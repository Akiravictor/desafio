using System;
using System.Collections.Generic;
using System.Text;

namespace Desafio_Globo.Domain.Models
{
	public class MemoryRequest
	{
		public List<string> Labels { get; set; }

		public List<double> Data { get; set; }
	}
}
