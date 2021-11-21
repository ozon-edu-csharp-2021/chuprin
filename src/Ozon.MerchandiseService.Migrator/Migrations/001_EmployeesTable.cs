using FluentMigrator;

namespace Ozon.MerchandiseService.Migrator.Migrations
{
    [Migration(1)]
    public class EmployeeTable: Migration {
        
        public override void Up()
        {
            Execute.Sql(@"
                    CREATE TABLE if not exists employees(
                    id BIGSERIAL PRIMARY KEY,
                    employee_id BIGINT);"
            );
        }

        public override void Down()
        {
            Execute.Sql("DROP TABLE if exists employees;");
        }
    }
}