using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StajApiDersi.Models.Concrete;

namespace StajApiDersi.Infrastructure.SeedData
{
    public class CategorySeeding : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { ID = 1, Name = "Gıda" },
                new Category { ID = 2, Name = "Beyaz Eşya" },
                new Category { ID = 3, Name = "Küçük Ev Aletleri" },
                new Category { ID = 4, Name = "Aksesuar" },
                new Category { ID = 5, Name = "Kıyafet" }
                );
        }
    }
}
