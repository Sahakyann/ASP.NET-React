using Microsoft.EntityFrameworkCore;

namespace aspnetserver.Data
{
    internal sealed class AppDBContext : DbContext
    {
        public DbSet<Post> Posts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder) => dbContextOptionsBuilder.UseSqlite("Data Source=./Data/AppDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User[] usersToSeed = new User[6];

            for (int i = 1; i <= usersToSeed.Length; i++)
            {
                usersToSeed[i - 1] = new User
                {
                    userID = i,
                    username = $"User{i}",
                    password = $"123"
                };
            }
            modelBuilder.Entity<User>().HasData(usersToSeed);
            Post[] postsToSeed = new Post[6];

            for (int i = 1; i <= postsToSeed.Length; i++)
            {
                postsToSeed[i - 1] = new Post
                {
                    PostId = i,
                    Title = $"Post {i}",
                    Content = $"This is post {i} and it has some very interesting content"
                };
            }
            modelBuilder.Entity<Post>().HasData(postsToSeed);
        }
    }
}
