using TaskManagement.Infrastructure.Persistence;

namespace TaskManagement.Infrastructure.Database;

public static class DatabaseInitializer
{
    public static async Task WaitForDatabaseAsync(AppDbContext db, int retries = 10)
    {
        for (int i = 0; i < retries; i++)
        {
            try
            {
                if (await db.Database.CanConnectAsync())
                    return;
            }
            catch
            {
                // ignored
            }

            await Task.Delay(2000);
        }

        throw new Exception("Database did not become available in time.");
    }
}
