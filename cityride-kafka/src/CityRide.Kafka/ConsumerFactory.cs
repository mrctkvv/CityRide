using CityRide.Kafka.Interfaces;
using CityRide.Kafka.Serializers;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace CityRide.Kafka
{
	public class ConsumerFactory<TKey, TValue> 
		: IConsumerFactory<TKey, TValue>
	{
		private readonly ConsumerConfig _consumerConfig;
		public ConsumerFactory(IConfiguration configuration)
		{
			_consumerConfig = new ConsumerConfig();
			configuration.GetSection("KafkaConsumer").Bind(_consumerConfig);
		}
		public IConsumer<TKey, TValue> CreateConsumer()
		{
			return new ConsumerBuilder<TKey, TValue>(_consumerConfig).SetValueDeserializer(new JsonDeserializer<TValue>()).Build();
		}
	}
}
