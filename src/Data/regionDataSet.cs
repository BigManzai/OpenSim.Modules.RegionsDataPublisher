using Newtonsoft.Json;
using System;

namespace OpenSim.Region.OptionalModules.RegionsDataPublisher.Data
{
    class regionDataSet
    {
        [JsonProperty(PropertyName = "Name")]
        public String RegionName = "";

        [JsonProperty(PropertyName = "Estate")]
        public String RegionEstate = "";

        [JsonProperty(PropertyName = "InSearch")]
        public bool RegionIsVisibleInSearch = false;

        [JsonProperty(PropertyName = "OpenAccess")]
        public bool RegionPublicAccess = true;

        [JsonProperty(PropertyName = "Position")]
        public String RegionPosition = "0/0";

        [JsonProperty(PropertyName = "Size")]
        public String RegionSize = "256";

        [JsonProperty(PropertyName = "HomeURI")]
        public String RegionHomeURI = "";

        [JsonProperty(PropertyName = "Image")]
        public String RegionImageUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "UUID")]
        public String RegionUUID = "00000000-0000-0000-0000-000000000000";


    }
}
