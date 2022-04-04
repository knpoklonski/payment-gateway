using FluentMigrator;

namespace PaymentGateway.Api.Infrastructure.Migrations;

[Migration(202202042132)]
public class Migration202202042132 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("payments")
            .WithColumn("id").AsInt32().Identity().PrimaryKey()
            .WithColumn("payment_id").AsString(38).NotNullable().Unique().Indexed("payment_id_index")
            .WithColumn("json_data").AsString(int.MaxValue).NotNullable();
    }
}