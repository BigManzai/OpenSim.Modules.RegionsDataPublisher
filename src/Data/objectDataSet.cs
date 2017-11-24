using Newtonsoft.Json;
using System;

namespace OpenSim.Region.OptionalModules.RegionsDataPublisher.Data
{
    class objectDataSet
    {
        [JsonProperty(PropertyName = "Name")]
        public String ObjectName = "";

        [JsonProperty(PropertyName = "Description")]
        public String ObjectDescription = "";

        [JsonProperty(PropertyName = "InSearch")]
        public bool ObjectIsVisibleInSearch = false;

        [JsonProperty(PropertyName = "Sale")]
        public bool ObjectIsForSale = false;

        [JsonProperty(PropertyName = "Price")]
        public int ObjectSalePrice = 0;

        [JsonProperty(PropertyName = "Copy")]
        public bool ObjectIsForCopy = false;

        [JsonProperty(PropertyName = "Position")]
        public String ObjectPosition = "0/0/0";

        [JsonProperty(PropertyName = "Owner")]
        public OwnerDataSet ObjectOwner = new OwnerDataSet();

        [JsonProperty(PropertyName = "Group")]
        public String ObjectGroupUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "Item")]
        public String ObjectItemUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "Image")]
        public String ObjectImageUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "UUID")]
        public String ObjectUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "Parent")]
        public String ParentUUID = "00000000-0000-0000-0000-000000000000";
    }
}
