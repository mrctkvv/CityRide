using Confluent.Kafka;

namespace CityRide.Kafka.Interfaces
{
    public interface IProducerFactory<TKey, TValue>
	{
		IProducer<TKey, TValue> CreateProducer();
	}
}
