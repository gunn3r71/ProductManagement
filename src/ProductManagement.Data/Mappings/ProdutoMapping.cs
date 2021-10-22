using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Business.Models;

namespace ProductManagement.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(p => p.Nome).HasMaxLength(70).IsRequired();
            builder.Property(p => p.Descricao).HasMaxLength(400).IsRequired();
            builder.Property(p => p.Valor).HasPrecision(10, 2).IsRequired();
            builder.Property(p => p.Imagem).HasMaxLength(100).IsRequired();
            builder.Property(p => p.DataCadastro).IsRequired();
        }
    }
}