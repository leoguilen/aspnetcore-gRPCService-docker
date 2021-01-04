using FluentValidation;
using PrateleiraLivros.Dominio.Entidades;

namespace PrateleiraLivros.Application.Validators
{
    public class EditoraValidator : AbstractValidator<Editora>
    {
        public EditoraValidator()
        {
            RuleFor(editora => editora.Nome)
                .NotEmpty()
                .WithMessage("Nome deve ser preenchido");
            RuleFor(editora => editora.Nome)
                .MinimumLength(4)
                .WithMessage("Nome deve conter mais de 4 caracteres")
                .When(editora => !string.IsNullOrEmpty(editora.Nome));

            RuleFor(editora => editora.Endereco)
                .NotEmpty()
                .WithMessage("Endereço deve ser preenchido");
        }
    }
}
