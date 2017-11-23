using Newtonsoft.Json;
using System.Collections.Generic;

namespace OpenSim.Region.OptionalModules.RegionsDataPublisher.Data
{
    class mainDataSet
    {
        [JsonProperty(PropertyName = "Regions")]
        public List<regionDataSet> RegionData = new List<regionDataSet>();

        [JsonProperty(PropertyName = "Parcels")]
        public List<parcelDataSet> ParcelData = new List<parcelDataSet>();

        [JsonProperty(PropertyName = "Objects")]
        public List<objectDataSet> ObjectData = new List<objectDataSet>();

        [JsonProperty(PropertyName = "Agents")]
        public List<agentDataSet> AgentData = new List<agentDataSet>();
    }
}
