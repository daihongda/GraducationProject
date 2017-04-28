namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update131 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SchoolNews", "PublishTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SchoolNews", "PublishTime", c => c.DateTime(nullable: false));
        }
    }
}
