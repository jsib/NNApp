using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace NNApp.Models
{
    [Table("NewspaperMail")]
    public class Subscription
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }
        public int newspaper_id { get; set; }
        public string NewspaperName { get; set; }
        public string email { get; set; }
        public int MessageType { get; set; }
        public string FromEmail { get; set; }
        public bool Enabled { get; set; }
        public string Comment { get; set; }

    }
}