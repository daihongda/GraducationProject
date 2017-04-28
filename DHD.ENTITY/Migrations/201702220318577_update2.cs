namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Content = c.String(),
                        PublishTime = c.DateTime(nullable: false),
                        Author = c.String(),
                        SchoolNewId = c.Int(nullable: false),
                        ParentId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Configures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ListPath = c.String(),
                        HrefPath = c.String(),
                        TitlePath = c.String(),
                        TimePath = c.String(),
                        ContentPath = c.String(),
                        SchoolId = c.Int(nullable: false),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentId = c.Int(nullable: false),
                        Href = c.String(),
                        IsDelete = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MenuItems");
            DropTable("dbo.Configures");
            DropTable("dbo.Comments");
        }
    }
}
