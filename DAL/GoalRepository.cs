using SQLite;

namespace ProductivityApp.DAL;


/// <summary>
/// GoalRepository facilitates database access with standard initialization and get/add/update/delete functions
/// </summary>
public class GoalRepository : IGoalRepository
{

    //—---------------------------------------------------------------------------
    // Init database
    SQLiteAsyncConnection database;

    /// <summary>
    /// Initializes database connection
    /// </summary>
    /// <returns>null if connection already initialized</returns>
    async Task Init()
    {
        // This is the guard clause. If the database connection 
        // has already been created, the method exits immediately
        if (database is not null)
            return;

        // Creates the actual connection to the database
        database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

        // Create Goal Table
        // This line creates the Goals table in the database
        // but only if it doesn't already exist. 
        // SQLite-net handles this automatically by reading the GoalEntity class 
        // and mapping its properties to table columns.
        var result = await database.CreateTableAsync<GoalEntity>();
    }

    //—---------------------------------------------------------------------------
    // Function definitions


/// <summary>
/// Initializes the database if uninitalized and retrieves all entries
/// </summary>
/// <returns>List of GoalEntities</returns>
    public async Task<List<GoalEntity>> GetItemsAsync()
    {
        // Get all Goal items, use async 
        await Init();
        return await database.Table<GoalEntity>().ToListAsync();
    }

    /// <summary>
    /// Retrieves a GoalEntity from the database by ID
    /// </summary>
    /// <param name="id"> Primary key Id </param>
    /// <returns>First GoalEntity where Id = id or null if none</returns>
    public async Task<GoalEntity> GetItemAsync(int id)
    {
        // Get specific item by Id -  
        // FirstOrDefaultAsync -> if there is more than one object, 
        // it will return with the first, or null
        await Init();
        return await database.Table<GoalEntity>().Where(entity => entity.Id == id).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Saves goal to the database, makes new record if not already present
    /// </summary>
    /// <param name="goal"> Goal to be saved </param>
    /// <returns> Error message if unable to insert or update record </returns>
    public async Task<int> SaveItemAsync(GoalEntity goal)
    {
        // Check if it is a new object
        await Init();
        if (goal.Id != 0)
        {
            // Update data 
            return await database.UpdateAsync(goal);
        }
        else
        {
            // Save new goal
            return await database.InsertAsync(goal);
        }
    }

    /// <summary>
    /// Deletes Goal from database
    /// </summary>
    /// <param name="goal"> Goal to be deleted </param>
    /// <returns> Error message if unable to delete </returns>
    public async Task<int> DeleteItemAsync(GoalEntity goal)
    {
        // Remove item from database 
        await Init();
        return await database.Table<GoalEntity>().DeleteAsync(entity => entity.Id == goal.Id);
    }

}

