using Domain.Infrastructure.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace RebusPublisher.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class RabbitPublisherController : ControllerBase
	{
		private readonly ILogger<KafkaPublisherController> _logger;
		private readonly IRabbitMessagePublisher _publisher;
		public RabbitPublisherController(ILogger<KafkaPublisherController> logger, IRabbitMessagePublisher publisher)
		{
			_logger = logger;
			_publisher = publisher;
		}

		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] RabbitModel message)
		{
			await _publisher.PublishMessageAsync(message);
			return Ok("Message sended");
		}
	}
}
