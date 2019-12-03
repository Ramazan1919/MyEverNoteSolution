namespace MyEverNote.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class image : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvernoteUsers", "ProfileImageFilename", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EvernoteUsers", "ProfileImageFilename");
        }
    }
}
