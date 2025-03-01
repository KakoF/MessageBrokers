using Domain.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RebusPublisher.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class KafkaPublisherController : ControllerBase
	{
		private readonly ILogger<KafkaPublisherController> _logger;
		private readonly IKafkaMessagePublisher _publisher;
		public KafkaPublisherController(ILogger<KafkaPublisherController> logger, IKafkaMessagePublisher publisher)
		{
			_logger = logger;
			_publisher = publisher;
		}

		[HttpPost]
		public async Task<IActionResult> PostAsync([FromBody] object message)
		{
			await _publisher.PublishMessageAsync(message);
			return Ok("Message sended");
		}
	}
}
