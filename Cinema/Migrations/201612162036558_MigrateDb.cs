namespace Cinema.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MigrateDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Surname = c.String(nullable: false, maxLength: 50),
                        Year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Films",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(),
                        Director = c.String(nullable: false, maxLength: 50),
                        year = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreFilm",
                c => new
                    {
                        GenreId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GenreId, t.FilmId })
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.FilmId);
            
            CreateTable(
                "dbo.ActorFilm",
                c => new
                    {
                        ActorId = c.Int(nullable: false),
                        FilmId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ActorId, t.FilmId })
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Films", t => t.FilmId, cascadeDelete: true)
                .Index(t => t.ActorId)
                .Index(t => t.FilmId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ActorFilm", "FilmId", "dbo.Films");
            DropForeignKey("dbo.ActorFilm", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.GenreFilm", "FilmId", "dbo.Films");
            DropForeignKey("dbo.GenreFilm", "GenreId", "dbo.Genres");
            DropIndex("dbo.ActorFilm", new[] { "FilmId" });
            DropIndex("dbo.ActorFilm", new[] { "ActorId" });
            DropIndex("dbo.GenreFilm", new[] { "FilmId" });
            DropIndex("dbo.GenreFilm", new[] { "GenreId" });
            DropTable("dbo.ActorFilm");
            DropTable("dbo.GenreFilm");
            DropTable("dbo.Genres");
            DropTable("dbo.Films");
            DropTable("dbo.Actors");
        }
    }
}
