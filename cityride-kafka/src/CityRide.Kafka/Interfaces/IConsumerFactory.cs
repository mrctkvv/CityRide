using Confluent.Kafka;

namespace CityRide.Kafka.Interfaces
{
    public interface IConsumerFactory<TKey, TValue>
	{
		IConsumer<TKey, TValue> CreateConsumer();
	}
}
