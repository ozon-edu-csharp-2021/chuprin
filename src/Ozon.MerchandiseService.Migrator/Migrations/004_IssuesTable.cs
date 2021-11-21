using FluentMigrator;

namespace Ozon.MerchandiseService.Migrator.Migrations
{
    [Migration(4)]
    public class IssuesTable: Migration {
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists issues(
                    id BIGSERIAL PRIMARY KEY,
                    employee_id bigint NOT NULL,
                    merch_type_id smallint NOT NULL,
                    date_create timestamp,
                    status_id smallint NOT NULL)");
            
            Create
                .Index("issues_employee_id_idx")
                .OnTable("issues")
                .InSchema("public")
                .OnColumn("employee_id");
            
            Create
                .Index("merch_type_id_idx")
                .OnTable("issues")
                .InSchema("public")
                .OnColumn("merch_type_id");
            
            Create
                .Index("status_id_idx")
                .OnTable("issues")
                .InSchema("public")
                .OnColumn("status_id");
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists issues;");
        }
    }
}