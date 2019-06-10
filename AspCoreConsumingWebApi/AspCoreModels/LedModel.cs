using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AspCoreModels
{
    [DataContract]
    [Table("ledmodel")]
    public class LedModel
    {
        [Key]
        [Column(Order = 0)]
        [DataMember(Name = "id")]
        public string id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DataMember(Name = "gpio")]
        public int gpio { get; set; }

        [DataMember(Name = "status")]
        public int status { get; set; }

        [DataMember(Name = "desc")]
        public string desc { get; set; }        
    }
}
