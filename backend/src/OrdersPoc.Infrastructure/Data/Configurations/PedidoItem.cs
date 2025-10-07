using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrdersPoc.Domain.Entities;

namespace OrdersPoc.Infrastructure.Data.Configurations;

public class PedidoItemConfiguration : IEntityTypeConfiguration<PedidoItem>
{
    public void Configure(EntityTypeBuilder<PedidoItem> builder)
    {
        builder.ToTable("PedidoItens");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(i => i.PedidoId)
            .HasColumnName("PedidoId")
            .IsRequired();

        builder.Property(i => i.Produto)
            .HasColumnName("Produto")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(i => i.Quantidade)
            .HasColumnName("Quantidade")
            .IsRequired();

        builder.OwnsOne(i => i.PrecoUnitario, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("PrecoUnitario")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("MoedaPreco")
                .HasMaxLength(3)
                .IsRequired()
                .HasDefaultValue("BRL");
        });

        builder.OwnsOne(i => i.Subtotal, money =>
        {
            money.Property(m => m.Amount)
                .HasColumnName("Subtotal")
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            money.Property(m => m.Currency)
                .HasColumnName("MoedaSubtotal")
                .HasMaxLength(3)
                .IsRequired()
                .HasDefaultValue("BRL");
        });

        builder.Property(i => i.CriadoEm)
            .HasColumnName("CriadoEm")
            .IsRequired();

        builder.Property(i => i.AtualizadoEm)
            .HasColumnName("AtualizadoEm");

        builder.Property(i => i.Ativo)
            .HasColumnName("Ativo")
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasOne(i => i.Pedido)
            .WithMany(p => p.Itens)
            .HasForeignKey(i => i.PedidoId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(i => i.PedidoId)
            .HasDatabaseName("IX_PedidoItens_PedidoId");
    }
}
