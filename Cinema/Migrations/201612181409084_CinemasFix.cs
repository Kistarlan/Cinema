namespace Cinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CinemasFix : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Halls", "CinemaId", "dbo.Cinemas");
            DropForeignKey("dbo.Sessions", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Sessions", "filmId", "dbo.Films");
            DropIndex("dbo.Halls", new[] { "CinemaId" });
            DropIndex("dbo.Sessions", new[] { "filmId" });
            DropIndex("dbo.Sessions", new[] { "HallId" });
            AlterColumn("dbo.Halls", "CinemaId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sessions", "filmId", c => c.Int(nullable: false));
            AlterColumn("dbo.Sessions", "HallId", c => c.Int(nullable: false));
            CreateIndex("dbo.Halls", "CinemaId");
            CreateIndex("dbo.Sessions", "filmId");
            CreateIndex("dbo.Sessions", "HallId");
            AddForeignKey("dbo.Halls", "CinemaId", "dbo.Cinemas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sessions", "HallId", "dbo.Halls", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sessions", "filmId", "dbo.Films", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "filmId", "dbo.Films");
            DropForeignKey("dbo.Sessions", "HallId", "dbo.Halls");
            DropForeignKey("dbo.Halls", "CinemaId", "dbo.Cinemas");
            DropIndex("dbo.Sessions", new[] { "HallId" });
            DropIndex("dbo.Sessions", new[] { "filmId" });
            DropIndex("dbo.Halls", new[] { "CinemaId" });
            AlterColumn("dbo.Sessions", "HallId", c => c.Int());
            AlterColumn("dbo.Sessions", "filmId", c => c.Int());
            AlterColumn("dbo.Halls", "CinemaId", c => c.Int());
            CreateIndex("dbo.Sessions", "HallId");
            CreateIndex("dbo.Sessions", "filmId");
            CreateIndex("dbo.Halls", "CinemaId");
            AddForeignKey("dbo.Sessions", "filmId", "dbo.Films", "Id");
            AddForeignKey("dbo.Sessions", "HallId", "dbo.Halls", "Id");
            AddForeignKey("dbo.Halls", "CinemaId", "dbo.Cinemas", "Id");
        }
    }
}
