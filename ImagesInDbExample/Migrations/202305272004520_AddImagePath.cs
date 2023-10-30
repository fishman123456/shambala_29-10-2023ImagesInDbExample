namespace ImagesInDbExample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImagePath : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ImagePaths",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        FileLocation = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ImagePaths");
        }
    }
}
