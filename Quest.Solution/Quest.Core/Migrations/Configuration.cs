namespace SuHui.Core.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SuHui.Framework;
    using System.Configuration;
    using SuHui.Framework.MVC;
    using SuHui.Core.Data;

    internal sealed class Configuration : DbMigrationsConfiguration<AppDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(AppDBContext context)
        {
        }
    }
}