namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update15 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Schools", "SchoolIconUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Schools", "SchoolIconUrl");
        }
    }
}
