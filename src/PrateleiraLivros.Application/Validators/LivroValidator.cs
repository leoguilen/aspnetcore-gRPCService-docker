using FluentValidation;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Application.Validators
{
    public class LivroValidator : AbstractValidator<Livro>
    {
        public LivroValidator()
        {

        }
    }
}
