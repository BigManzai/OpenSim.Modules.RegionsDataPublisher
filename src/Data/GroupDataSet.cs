using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenSim.Region.OptionalModules.RegionsDataPublisher.Data
{
    class GroupDataSet
    {
        [JsonProperty(PropertyName = "Name")]
        public String GroupName = "";

        [JsonProperty(PropertyName = "HomeURI")]
        public String GroupHomeURI = "";

        [JsonProperty(PropertyName = "UUID")]
        public String GroupUUID = "";

        [JsonProperty(PropertyName = "Image")]
        public String GroupImage = "";

        [JsonProperty(PropertyName = "InSearch")]
        public bool GroupShowInList = false;

        [JsonProperty(PropertyName = "ShowInList")]
        public bool GroupAllowPublish = false;

        [JsonProperty(PropertyName = "Founder")]
        public String GroupFounder = "00000000-0000-0000-0000-000000000000";


    }
}
