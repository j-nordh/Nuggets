using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SupplyChain.Dto.SerializationHelpers
{
    [JsonConverter(typeof(SpInfo))]
    class SpInfoConverter
    {
    }
}
