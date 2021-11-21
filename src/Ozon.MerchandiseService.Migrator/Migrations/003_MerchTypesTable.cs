using FluentMigrator;

namespace Ozon.MerchandiseService.Migrator.Migrations
{
    [Migration(3)]
    public class MerchTypesTable: Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists merch_types(
                    id smallserial PRIMARY KEY,
                    name varchar NOT NULL)");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists merch_types;");
        }
    }
}