using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Data.Config
{
    public class LivroConfiguration : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable("PL.Livro");

            builder.HasKey(livro => livro.Id);

            builder.Property(livro => livro.Titulo)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(livro => livro.Descricao)
                .IsRequired()
                .HasColumnType("varchar (MAX)");

            builder.Property(livro => livro.Valor)
                .IsRequired()
                .HasColumnType("decimal (5,2)");

            builder.Property(livro => livro.ISBN_10)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(livro => livro.Edicao)
                .HasDefaultValue(1);

            builder.Property(livro => livro.DataPublicacao)
                .IsRequired();

            builder.Property(livro => livro.Idioma)
                .IsRequired(false)
                .HasMaxLength(50)
                .HasDefaultValue("Português");
        }
    }
}
