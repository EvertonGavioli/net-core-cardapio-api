using CD.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CD.Infra.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CEP)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.Property(p => p.Numero)
                .IsRequired()
                .HasColumnType("varchar(20)");

            builder.ToTable("Enderecos");
        }
    }
}
