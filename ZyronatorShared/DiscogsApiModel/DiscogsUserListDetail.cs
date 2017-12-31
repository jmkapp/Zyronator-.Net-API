using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ZyronatorShared.DiscogsApiModels
{
    public class DiscogsUserListDetail
    {
        public string Name { get; set; }
        public List<DiscogsUserListItem> Items { get; set; }
        public string Uri { get; set; }
        public int Id { get; set; }
        [JsonProperty("date_added")]
        public DateTime DateAdded { get; set; }
        [JsonProperty("date_changed")]
        public DateTime DateChanged { get; set; }
        [JsonProperty("resource_url")]
        public string ResourceUrl { get; set; }
        public bool Public { get; set; }
        public string Description { get; set; }
    }

    public class DiscogsUserListItem
    {
        public string Comment { get; set; }
        [JsonProperty("display_title")]
        public string DisplayTitle { get; set; }
        public string Uri { get; set; }
        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }
        [JsonProperty("resource_url")]
        public string ResourceUrl { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
    }
}
