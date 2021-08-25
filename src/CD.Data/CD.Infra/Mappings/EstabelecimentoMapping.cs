using CD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CD.Infra.Mappings
{
    public class EstabelecimentoMapping : IEntityTypeConfiguration<Estabelecimento>
    {
        public void Configure(EntityTypeBuilder<Estabelecimento> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.UsuarioId)
                .IsRequired();

            builder.Property(p => p.Telefone)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.QRCode)
                .IsRequired()
                .HasColumnType("mediumtext");

            builder.HasOne(e => e.Endereco)
                .WithOne(f => f.Estabelecimento);

            builder.HasMany(e => e.Categorias)
                .WithOne(p => p.Estabelecimento)
                .HasForeignKey(q => q.EstabelecimentoId);

            builder.ToTable("Estabelecimentos");
        }
    }
}
