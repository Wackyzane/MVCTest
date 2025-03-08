using System.Text.Json.Serialization;

namespace TestProject.Models
{
    public class Puppy
    {
        public int puppyid;
        [JsonPropertyName("name")]
        public string name;
        [JsonPropertyName("gender")]
        public string gender;
    }
}
