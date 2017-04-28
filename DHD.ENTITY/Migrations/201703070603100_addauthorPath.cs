namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addauthorPath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Configures", "AuthorPath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Configures", "AuthorPath");
        }
    }
}
