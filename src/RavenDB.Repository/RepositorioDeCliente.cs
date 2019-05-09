using Raven.Client.Documents.Session;
using RavenDB.Model;
using System.Collections.Generic;
using System.Linq;

namespace RavenDB.Repository
{
    public class RepositorioDeCliente : RepositorioGenerico<Cliente>
    {
        public override Cliente Consulte(string idDoItem)
        {
            using (IDocumentSession session = documentStore.OpenSession())
            {
                var cliente = session.Load<Cliente>(idDoItem);
                if (cliente.IndicadorId != null)
                {
                    cliente.Indicador = session.Load<Cliente>(cliente.IndicadorId);
                }
                return cliente;
            }
        }

        public List<Cliente> ConsultaPorIdade(int idade)
        {
            using (IDocumentSession session = documentStore.OpenSession())
            {
                return session.Query<Cliente>().Where(x => x.Idade > idade).ToList();
            }
        }
    }
}
