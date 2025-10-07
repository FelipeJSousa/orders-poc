using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersPoc.Domain.Entities;

namespace OrdersPoc.Infrastructure.Data.Configurations;

public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
{
    public void Configure(EntityTypeBuilder<Cliente> builder)
    {
        builder.ToTable("Clientes");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(c => c.Nome)
            .HasColumnName("Nome")
            .HasMaxLength(200)
            .IsRequired();

        builder.OwnsOne(c => c.Email, email =>
        {
            email.Property(e => e.Address)
                .HasColumnName("Email")
                .HasMaxLength(255)
                .IsRequired();

            email.HasIndex(e => e.Address)
                .HasDatabaseName("IX_Clientes_Email")
                .IsUnique();
        });

        builder.Property(c => c.TipoPessoa)
            .HasColumnName("TipoPessoa")
            .HasConversion<int>()
            .IsRequired();

        builder.Property(c => c.CpfCnpj)
            .HasColumnName("CpfCnpj")
            .HasMaxLength(14);

        builder.Property(c => c.Telefone)
            .HasColumnName("Telefone")
            .HasMaxLength(20);

        builder.Property(c => c.Endereco)
            .HasColumnName("Endereco")
            .HasMaxLength(300);

        builder.Property(c => c.Cidade)
            .HasColumnName("Cidade")
            .HasMaxLength(100);

        builder.Property(c => c.Estado)
            .HasColumnName("Estado")
            .HasMaxLength(2);

        builder.Property(c => c.Cep)
            .HasColumnName("Cep")
            .HasMaxLength(8);

        builder.Property(c => c.CriadoEm)
            .HasColumnName("CriadoEm")
            .IsRequired();

        builder.Property(c => c.AtualizadoEm)
            .HasColumnName("AtualizadoEm");

        builder.Property(c => c.Ativo)
            .HasColumnName("Ativo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasMany(c => c.Pedidos)
            .WithOne(p => p.Cliente)
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.CpfCnpj)
            .HasDatabaseName("IX_Clientes_CpfCnpj");

        builder.HasIndex(c => c.Ativo)
            .HasDatabaseName("IX_Clientes_Ativo");
    }
}