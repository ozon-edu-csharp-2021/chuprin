using FluentMigrator;

namespace Ozon.MerchandiseService.Migrator.Migrations
{
    [Migration(5)]
    public class FillEnums: ForwardOnlyMigration
    {
        public override void Up()
        {
            Execute.Sql(@"
                INSERT INTO issue_statuses (id, name)
                VALUES 
                    (1, 'IsCreated'),
                    (2, 'InQueue'),
                    (3, 'IsPending'),
                    (4, 'IsIssued')
                ON CONFLICT DO NOTHING
            ");

            Execute.Sql(@"
                INSERT INTO merch_types (id, name)
                VALUES 
                    (1, 'WelcomePack'),
                    (2, 'VeteranPack'),
                    (3, 'ConferenceSpeakerPack'),
                    (4, 'ConferenceListenerPack'),
                    (5, 'ProbationPeriodEndingPack')
                ON CONFLICT DO NOTHING
            ");
        }
    }
}