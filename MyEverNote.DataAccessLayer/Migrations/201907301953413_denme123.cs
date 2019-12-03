namespace MyEverNote.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class denme123 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EvernoteUsers", "Name", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EvernoteUsers", "Name", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
