using Raven.Client.Documents;
using System;

namespace RavenDB.Repository
{
    public class DataContext
    {
        private static Lazy<IDocumentStore> store = new Lazy<IDocumentStore>(CreateStore);

        public static IDocumentStore Store => store.Value;

        private static IDocumentStore CreateStore()
        {
            IDocumentStore store = new DocumentStore()
            {
                Urls = new[] { "http://localhost:8080" },
                Database = "Northwind"
            }.Initialize();

            return store;
        }
    }
}
