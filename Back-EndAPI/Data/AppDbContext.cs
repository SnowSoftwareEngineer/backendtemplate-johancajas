using System.Security.Cryptography.X509Certificates;

namespace Back_EndAPI.Data
{
    public class TestService
    {
        public TestService()
        {
           public Dbset<test> Test => Set<Test>();
        public TestService(TestService<TestService> Options) : base(Options);// Constructor logic can be added here if needed
        }
        // Define DbSets for your entities here
        // public DbSet<YourEntity> Yo
    }
}
