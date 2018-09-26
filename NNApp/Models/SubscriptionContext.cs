using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;


namespace NNApp.Models
{
    public class SubscriptionContext : DbContext
    {
        public SubscriptionContext()
            : base("main")
        {}
        public DbSet<Subscription> Subscriptions { get; set; }
    }
}