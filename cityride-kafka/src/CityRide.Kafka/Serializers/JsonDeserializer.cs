using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CityRide.Kafka.Serializers
{
	public class JsonDeserializer<T> : IDeserializer<T>
	{
		public T Deserialize(ReadOnlySpan<byte> data, bool isNull, SerializationContext context)
		{
			var json = Encoding.UTF8.GetString(data);
			return JsonSerializer.Deserialize<T>(json);
		}
	}
}
