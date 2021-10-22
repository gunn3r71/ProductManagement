using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Business.Models;

namespace ProductManagement.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(f => f.Id).ValueGeneratedOnAdd().IsRequired();
            builder.Property(f => f.Nome).IsRequired().HasMaxLength(100);
            builder.Property(f => f.Documento).IsRequired().HasMaxLength(14);
            builder.Property(f => f.TipoFornecedor).IsRequired().HasMaxLength(1);
            builder.Property(f => f.DataCadastro).IsRequired();

            builder.HasOne(f => f.Endereco)
                .WithOne(e => e.Fornecedor);

            builder.HasMany(f => f.Produtos)
                .WithOne(p => p.Fornecedor)
                .HasForeignKey(p => p.FornecedorId);
        }
    }
}