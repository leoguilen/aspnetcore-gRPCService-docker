using System;

namespace PrateleiraLivros.Dominio.Entidades
{
    public class Entity : BaseEntity<Guid>
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
            DataCadastro = DateTime.Now;
            DataAtualizacao = DateTime.Now;
        }

        public DateTime DataCadastro { get; protected set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
