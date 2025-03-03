using Domain.Models;
using Rebus.Handlers;


namespace Infraestructure.Consumers
{
	public class RabbitMessageHandler : IHandleMessages<RabbitModel>
	{
		private static int _attemptCount = 0;
		public async Task Handle(RabbitModel message)
		{
			var mensagem = message;
			_attemptCount++;
			Console.WriteLine($"Tentativa {_attemptCount} em {DateTime.Now}");
			if (mensagem.Value > 10)
				throw new Exception("Value message > 10");
			Console.WriteLine($"Recebida mensagem: {message.Description}");
		}
	}
}
