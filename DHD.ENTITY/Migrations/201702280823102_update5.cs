namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update5 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        NickName = c.String(),
                        Password = c.String(),
                        IsDelete = c.Int(nullable: false),
                        SchoolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Managers", "IsDelete", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Managers", "IsDelete");
            DropTable("dbo.Users");
        }
    }
}
