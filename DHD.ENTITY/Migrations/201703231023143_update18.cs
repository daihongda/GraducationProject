namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update18 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserThumbs",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        SchoolNewId = c.Int(nullable: false),
                        OperateTime = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.SchoolNewId });
            
        }
        
        public override void Down()
        {
            DropTable("dbo.UserThumbs");
        }
    }
}
