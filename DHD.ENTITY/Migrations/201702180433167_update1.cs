namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NewTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NewTypeUrls",
                c => new
                    {
                        SchoolId = c.Int(nullable: false),
                        NewTypeId = c.Int(nullable: false),
                        Url = c.String(),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SchoolId, t.NewTypeId });
            
            CreateTable(
                "dbo.SchoolNews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Content = c.String(nullable: false),
                        PublishTime = c.DateTime(nullable: false),
                        Author = c.String(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SchoolNews");
            DropTable("dbo.NewTypeUrls");
            DropTable("dbo.NewTypes");
        }
    }
}
