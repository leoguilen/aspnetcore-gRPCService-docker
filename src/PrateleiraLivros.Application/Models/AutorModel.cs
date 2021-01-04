namespace PrateleiraLivros.Application.Models
{
    public class AutorModel : BaseModel
    {
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Bio { get; set; }
        public string Avatar { get; set; }
    }
}
