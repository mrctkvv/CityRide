using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CityRide.Kafka.Serializers
{
	public class JsonSerializer<T> : ISerializer<T>
	{
		byte[] ISerializer<T>.Serialize(T data, SerializationContext context)
		{
			var json = JsonSerializer.Serialize(data);
			return Encoding.UTF8.GetBytes(json);
		}
	}
}
