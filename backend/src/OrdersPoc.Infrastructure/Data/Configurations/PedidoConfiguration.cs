using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersPoc.Domain.Entities;
using OrdersPoc.Domain.Enums;

namespace OrdersPoc.Infrastructure.Data.Configurations;

public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
{
    public void Configure(EntityTypeBuilder<Pedido> builder)
    {
        builder.ToTable("Pedidos");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(p => p.NumeroPedido)
            .HasColumnName("NumeroPedido")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(p => p.ClienteId)
            .HasColumnName("ClienteId")
            .IsRequired();

        builder.Property(p => p.DataPedido)
            .HasColumnName("DataPedido")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasColumnName("Status")
            .HasConversion<int>()
            .IsRequired();

        builder.OwnsOne(p => p.ValorTotal, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("ValorTotal")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("Moeda")
                .HasMaxLength(3)
                .IsRequired()
                .HasDefaultValue("BRL");
        });

        builder.Property(p => p.Observacoes)
            .HasColumnName("Observacoes")
            .HasMaxLength(500);

        builder.Property(p => p.CriadoEm)
            .HasColumnName("CriadoEm")
            .IsRequired();

        builder.Property(p => p.AtualizadoEm)
            .HasColumnName("AtualizadoEm");

        builder.Property(p => p.Ativo)
            .HasColumnName("Ativo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(p => p.Cliente)
            .WithMany(c => c.Pedidos)
            .HasForeignKey(p => p.ClienteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Itens)
            .WithOne(i => i.Pedido)
            .HasForeignKey(i => i.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.NumeroPedido)
            .HasDatabaseName("IX_Pedidos_NumeroPedido")
            .IsUnique();

        builder.HasIndex(p => p.ClienteId)
            .HasDatabaseName("IX_Pedidos_ClienteId");

        builder.HasIndex(p => p.Status)
            .HasDatabaseName("IX_Pedidos_Status");

        builder.HasIndex(p => p.DataPedido)
            .HasDatabaseName("IX_Pedidos_DataPedido");
    }
}
