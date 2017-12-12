namespace Infra.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PrimeiraMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "mc.Historico",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Venceu = c.Boolean(nullable: false),
                        PecasRestantes = c.Int(nullable: false),
                        PecasElimandas = c.Int(nullable: false),
                        Pontos = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("mc.Usuario", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "mc.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(nullable: false, maxLength: 128),
                        Email = c.String(nullable: false, maxLength: 128),
                        Senha = c.String(nullable: false, maxLength: 256),
                        GravatarHash = c.String(nullable: false, maxLength: 256),
                        UserHash = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Login, unique: true)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropForeignKey("mc.Historico", "UsuarioId", "mc.Usuario");
            DropIndex("mc.Usuario", new[] { "Email" });
            DropIndex("mc.Usuario", new[] { "Login" });
            DropIndex("mc.Historico", new[] { "UsuarioId" });
            DropTable("mc.Usuario");
            DropTable("mc.Historico");
        }
    }
}
