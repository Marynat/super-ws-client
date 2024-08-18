namespace super_ws.database.Repository;

public class SuperDbContextRepository : ISuperDbContextRepository
{
    private readonly SuperDbContext _context;
    public SuperDbContextRepository(SuperDbContext context) => _context = context;
}

public interface ISuperDbContextRepository
{
    //TODO: Add methotds to save and fetch data from db
}