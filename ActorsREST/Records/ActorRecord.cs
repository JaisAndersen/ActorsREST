using ActorRepositoryLibrary;
using System.ComponentModel;

namespace ActorsREST.Records
{
    public record ActorRecord(int? Id, string? Name, int? BirthYear);

    public static class RecordHelper
    {
        public static Actor ConvertActorRecord(ActorRecord record)
        {
            if (record.Id == null)
            {
                throw new ArgumentNullException("" + record.Id);
            }
            if (record.BirthYear == null)
            {
                throw new ArgumentNullException("" + record.BirthYear);
            }
            return new Actor() { Id = (int)record.Id, Name = record.Name, BirthYear = (int)record.BirthYear};
        }
    }
}
