using FluentValidation;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Application.Validators
{
    public class AutorValidator : AbstractValidator<Autor>
    {
        public AutorValidator()
        {
            RuleFor(autor => autor.Nome)
                .NotEmpty()
                .WithMessage("Nome deve ser preenchido");
            RuleFor(autor => autor.Nome)
                .MinimumLength(4)
                .WithMessage("Nome deve conter mais de 4 caracteres")
                .When(autor => !string.IsNullOrEmpty(autor.Nome));

            RuleFor(autor => autor.Sobrenome)
                .NotEmpty()
                .WithMessage("Sobrenome deve ser preenchido");
            RuleFor(autor => autor.Sobrenome)
                .MinimumLength(4)
                .WithMessage("Sobrenome deve conter mais de 4 caracteres")
                .When(autor => !string.IsNullOrEmpty(autor.Sobrenome));

            RuleFor(autor => autor.Email)
                .EmailAddress()
                .WithMessage("Endereço de email inválido");
        }
    }
}
