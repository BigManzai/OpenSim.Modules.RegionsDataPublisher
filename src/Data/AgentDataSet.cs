using Newtonsoft.Json;
using System;

namespace OpenSim.Region.OptionalModules.RegionsDataPublisher.Data
{
    class agentDataSet
    {
        [JsonProperty(PropertyName = "Name")]
        public String AgentName = "";

        [JsonProperty(PropertyName = "Position")]
        public String AgentPosition = "0/0/0";

        [JsonProperty(PropertyName = "UUID")]
        public String AgentUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "Parent")]
        public String ParentUUID = "00000000-0000-0000-0000-000000000000";
    }
}
