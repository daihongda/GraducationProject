namespace DHD.ENTITY.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addfk : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Areas", "ProvinceId");
            AddForeignKey("dbo.Areas", "ProvinceId", "dbo.Provinces", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Areas", "ProvinceId", "dbo.Provinces");
            DropIndex("dbo.Areas", new[] { "ProvinceId" });
        }
    }
}
