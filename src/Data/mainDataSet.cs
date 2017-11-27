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

        [JsonProperty(PropertyName = "Avatars")]
        public List<agentDataSet> AvatarData = new List<agentDataSet>();

        [JsonProperty(PropertyName = "Groups")]
        public List<GroupDataSet> GroupData = new List<GroupDataSet>();
    }
}
