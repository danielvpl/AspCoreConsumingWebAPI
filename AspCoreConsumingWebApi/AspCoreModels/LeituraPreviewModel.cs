using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace AspCoreModels
{
    [DataContract]
    [Table("mq_leitura_preview")]
    public class LeituraPreviewModel
    {
        [Key]
        [DataMember(Name = "ltp_codigo")]
        public long ltp_codigo { get; set; }

        [DataMember(Name = "ltp_data_hora")]
        public DateTime ltp_data_hora { get; set; }

        [DataMember(Name = "ltp_valor")]
        public double ltp_valor { get; set; }

        [DataMember(Name = "ltp_mq_codigo")]
        public int ltp_mq_codigo { get; set; }
        
        [DataMember(Name = "ltp_cam_codigo")]
        public int ltp_cam_codigo { get; set; }

        [DataMember(Name = "ltp_ban_codigo")]
        public int ltp_ban_codigo { get; set; }

        [DataMember(Name = "ltp_observacoes")]
        public string ltp_observacoes { get; set; }
    }
}
