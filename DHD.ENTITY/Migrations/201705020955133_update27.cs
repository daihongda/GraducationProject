namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update27 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "UserId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Comments", "UserId");
        }
    }
}
