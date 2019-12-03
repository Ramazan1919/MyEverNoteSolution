namespace MyEverNote.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resim : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notes", "NoteImageFilename", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notes", "NoteImageFilename");
        }
    }
}
