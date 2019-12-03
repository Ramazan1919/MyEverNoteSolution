namespace MyEverNote.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.EvernoteUsers", "UserName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.EvernoteUsers", "UserName", c => c.String());
        }
    }
}
