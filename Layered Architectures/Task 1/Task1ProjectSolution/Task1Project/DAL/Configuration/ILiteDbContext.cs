using LiteDB;

namespace Task1Project.DAL.Configuration
{
    public interface ILiteDbContext
    {
        LiteDatabase Database { get; }
    }
}
