namespace Cinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CinemasFix1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Cinemas", newName: "MCinemas");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.MCinemas", newName: "Cinemas");
        }
    }
}
