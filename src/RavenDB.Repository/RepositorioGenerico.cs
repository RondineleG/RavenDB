using Raven.Client.Documents;
using Raven.Client.Documents.Session;
using RavenDB.Model;
using System.Collections.Generic;
using System.Linq;
namespace RavenDB.Repository
{
    public class RepositorioGenerico<T> where T : ModelBase, new()
    {
        public DocumentStoreBase documentStore { get; set; }

        public RepositorioGenerico()
        {
            DataContext.Store.Initialize();
        }

        public virtual string Cadastrar(T item)
        {
            item.Id = null;
            return this.Salvar(item);
        }

        public virtual string Atualizar(T item)
        {
            return this.Salvar(item);
        }

        public virtual string Salvar(T item)
        {
            using (var session = documentStore.OpenSession())
            {
                session.Store(item);
                session.SaveChanges();
            }

            return item.Id;
        }

        public virtual T Consulte(string idDoItem)
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Load<T>(idDoItem);
            }
        }

        public List<T> ConsultePorTermo(string termo)
        {
            using (var session = documentStore.OpenSession())
            {
                return session
                    .Advanced
                    .DocumentQuery<T>()
                    .WhereEquals(x => x.Nome, $"Nome:{termo}").Boost(1000)
                    .WhereEquals(x => x.Nome, $"Nome:{termo}*").Boost(100)
                    .WhereEquals(x => x.Nome, $"Nome:*{termo}*").Boost(10)
                    .ToList();

            }
        }

        public virtual List<T> Liste()
        {
            using (var session = documentStore.OpenSession())
            {
                return session.Query<T>().ToList();
            }
        }

        public virtual List<T> Liste(int pagina, int elemtosPorPagina, out QueryStatistics estatisticas)
        {
            var quantidadeAPular = (pagina - 1) * elemtosPorPagina;

            using (IDocumentSession session = documentStore.OpenSession())
            {
                return session
                    .Query<T>()
                    .Statistics(out estatisticas)
                    .OrderBy(x => x.Nome)
                    .Skip(quantidadeAPular)
                    .Take(elemtosPorPagina)
                    .ToList();
            }
        }

        public virtual void Remover(string idDoItemSalvo)
        {
            using (var session = this.documentStore.OpenSession())
            {
                session.Delete(idDoItemSalvo);
                session.SaveChanges();
            }
        }
    }
}
