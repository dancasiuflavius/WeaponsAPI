using FluentMigrator;

namespace WeaponsAPI.Data.Migrations;

[Migration(03032024)]

public class CreateSchema:Migration
{
    public override void Up()
    {
        Create.Table("weapons")
            .WithColumn("id").AsInt32().PrimaryKey().Identity()
            .WithColumn("name").AsString(128).NotNullable()
            .WithColumn("category").AsString(128).NotNullable()
            .WithColumn("price").AsDouble().NotNullable();        
    }
    public override void Down()
    {

    }
}
