namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update14 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Images", "SchoolNewId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Images", "SchoolNewId");
        }
    }
}
