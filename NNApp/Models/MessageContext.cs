using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;



namespace NNApp.Models
{
    public class MessageContext : DbContext
    {
        public MessageContext()
            : base("main")
            { }
            public DbSet<Message> Messages { get; set; }
    }
}