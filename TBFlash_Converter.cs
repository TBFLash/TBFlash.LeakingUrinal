using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using SimAirport.Logging;
using Newtonsoft.Json.Serialization;


namespace TBFlash.LeakingUrinal
{
    public class TBFlash_Converter : DefaultContractResolver
    {
		public static DefaultContractResolver Instance = new TBFlash_Converter();

		protected override JsonContract CreateContract(Type objectType)
		{
			JsonContract contract = base.CreateContract(objectType);

			// this will only be called once and then cached
			if (objectType == typeof(TBFlash_LeakingUrinal.TBFlash_LeakingUrinalData))
			{
				Game.Logger.Write(Log.FromPool("").WithCodepoint());
			}
			return contract;
		}

		protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
		{
			IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

			foreach(JsonProperty prop in properties)
			{
				Game.Logger.Write(Log.FromPool($"{prop.PropertyName}:{prop.DeclaringType}"));
				if (prop.DeclaringType == typeof(SmartQueueManager) && prop.PropertyName == "parent")
				{
					Game.Logger.Write(Log.FromPool("SQM+parent").WithCodepoint());
					prop.ShouldSerialize =
						_ => false;
				}
				if(prop.DeclaringType == typeof(SmartQueue) && prop.PropertyName == "obj")
				{
					Game.Logger.Write(Log.FromPool("SQ+obj").WithCodepoint());
					prop.ShouldSerialize =
						_ => false;
				}
				if (prop.DeclaringType == typeof(SmartObject))
				{
					Game.Logger.Write(Log.FromPool("SO").WithCodepoint());
				}
			}
			return properties;
		}
	}
}
