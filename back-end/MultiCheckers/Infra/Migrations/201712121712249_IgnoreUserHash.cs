namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IgnoreUserHash : DbMigration
    {
        public override void Up()
        {
            DropColumn("mc.Usuario", "UserHash");
        }
        
        public override void Down()
        {
            AddColumn("mc.Usuario", "UserHash", c => c.String());
        }
    }
}
