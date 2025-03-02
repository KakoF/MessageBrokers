
namespace Domain.Models
{
	public class KafkaModel
	{
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
		public int Value { get; set; }
    }
}
