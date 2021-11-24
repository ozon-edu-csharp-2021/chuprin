using FluentMigrator;

namespace Ozon.MerchandiseService.Migrator.Migrations
{
    [Migration(2)]
    public class IssueStatusesTable: Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists issue_statuses(
                    id smallserial PRIMARY KEY,
                    name varchar NOT NULL)");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists issue_statuses;");
        }
    }
}