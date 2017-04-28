namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update7 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Schools", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Schools", "Introduce", c => c.String(nullable: false));
            AlterColumn("dbo.Schools", "HomePage", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Schools", "HomePage", c => c.String());
            AlterColumn("dbo.Schools", "Introduce", c => c.String());
            AlterColumn("dbo.Schools", "Name", c => c.String());
        }
    }
}
