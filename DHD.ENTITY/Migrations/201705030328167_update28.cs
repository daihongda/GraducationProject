namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update28 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "HaveExamined", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "HaveExamined");
        }
    }
}
