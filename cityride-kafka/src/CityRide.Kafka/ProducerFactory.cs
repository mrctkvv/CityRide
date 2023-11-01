using CityRide.Kafka.Interfaces;
using CityRide.Kafka.Serializers;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace CityRide.Kafka
{
	public class ProducerFactory<TKey, TValue>
		: IProducerFactory<TKey, TValue>
	{
		private readonly ProducerConfig _producerConfig;
		public ProducerFactory(IConfiguration configuration)
		{
			_producerConfig = new ProducerConfig();
			configuration.GetSection("KafkaProducer").Bind(_producerConfig);
		}
		public IProducer<TKey, TValue> CreateProducer()
		{
			return new ProducerBuilder<TKey, TValue>(_producerConfig).SetValueSerializer(new JsonSerializer<TValue>()).Build();
		}
	}
}
