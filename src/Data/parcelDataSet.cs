using Newtonsoft.Json;
using System;

namespace OpenSim.Region.OptionalModules.RegionsDataPublisher.Data
{
    class parcelDataSet
    {
        [JsonProperty(PropertyName = "Name")]
        public String ParcelName = "";

        [JsonProperty(PropertyName = "Description")]
        public String ParcelDescription = "";

        [JsonProperty(PropertyName = "InSearch")]
        public bool ParcelIsVisibleInSearch = false;

        [JsonProperty(PropertyName = "Sale")]
        public bool ParcelIsForSale = false;

        [JsonProperty(PropertyName = "Price")]
        public int ParcelPrice = 0;

        [JsonProperty(PropertyName = "SaleTo")]
        public String ParcelSaleToUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "Owner")]
        public OwnerDataSet ParcelOwner = new OwnerDataSet();

        [JsonProperty(PropertyName = "Group")]
        public String ParcelGroup = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "Image")]
        public String ImageUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "Bitmap")]
        public String ParcelBitmap = "";

        [JsonProperty(PropertyName = "Traffic")]
        public int ParcelDwell = 0;

        [JsonProperty(PropertyName = "OpenAccess")]
        public bool ParcelPublicAccess = true;

        [JsonProperty(PropertyName = "UUID")]
        public String ParcelUUID = "00000000-0000-0000-0000-000000000000";

        [JsonProperty(PropertyName = "Parent")]
        public String ParentUUID = "00000000-0000-0000-0000-000000000000";

    }
}
