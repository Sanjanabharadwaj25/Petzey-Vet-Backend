namespace PetzyVet.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        AddressId = c.Int(nullable: false, identity: true),
                        City = c.String(),
                        Street = c.String(),
                        ZipCode = c.String(),
                        State = c.String(),
                    })
                .PrimaryKey(t => t.AddressId);
            
            CreateTable(
                "dbo.Vets",
                c => new
                    {
                        VetId = c.Int(nullable: false, identity: true),
                        LName = c.String(),
                        FName = c.String(),
                        NPINumber = c.String(),
                        Username = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Speciality = c.String(),
                        ShortBio = c.String(),
                        Status = c.Boolean(nullable: false),
                        Photo = c.String(),
                        Gender = c.String(),
                        DOB = c.DateTime(nullable: false),
                        Rating = c.Double(nullable: false),
                        AddressId = c.Int(nullable: false),
                        Counter = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VetId)
                .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
                .Index(t => t.AddressId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vets", "AddressId", "dbo.Addresses");
            DropIndex("dbo.Vets", new[] { "AddressId" });
            DropTable("dbo.Vets");
            DropTable("dbo.Addresses");
        }
    }
}
