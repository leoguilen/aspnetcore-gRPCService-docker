using PrateleiraLivros.Dominio.Interfaces;

namespace PrateleiraLivros.Dominio.Entidades
{
    public class BaseEntity<TId> : IBaseEntity<TId>
    {
        public virtual TId Id { get; protected set; }
    }
}
