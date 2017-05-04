namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update29 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewTypeUrls", "LastCatchId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewTypeUrls", "LastCatchId");
        }
    }
}
