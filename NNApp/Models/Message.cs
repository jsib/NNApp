using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace NNApp.Models
{
    [Table("NewspaperMailMessage")]
    public class Message
    {
        [Column("MessageType")]
        public int id { set; get; }
        public string MessageText { set; get; }
    }
}