namespace Back_EndAPI.Data
{
    public class TestService
    {
        private readonly AppDbContext _db;

        public TestService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<test>> GetAllTestsAsync()
        {
            var tests = await _db.Test.ToListAsync();
            return tests.Select(t => new TestDTO
            {
                Id = t.Id,
                Name = t.Name,
                Email = t.Email
            }).ToList();




        }

        internal async Task GetTestDataAsync()
        {
            throw new NotImplementedException();
        }
    }
}
