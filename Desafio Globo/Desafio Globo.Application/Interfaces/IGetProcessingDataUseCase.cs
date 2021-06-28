using Desafio_Globo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_Globo.Application.Interfaces
{
	public interface IGetProcessingDataUseCase
	{
		Task<AwsUsage> ExecuteAsync();
	}
}
