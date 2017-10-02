using Ascon.ManagerEdition.Wizard.Models;
using Ascon.ManagerEdition.Wizard.ViewModel.Template;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Wizard.Utils
{
    public static class DocumentExtensions
    {
        public static ObservableCollection<DocumentModel> MapToDocument(this Documents source)
        {
            ObservableCollection<DocumentModel> result = new ObservableCollection<DocumentModel>();

            if (source.DocumentList.Any())
            {
                foreach (var item in source.DocumentList)
                {
                    result.Add(new DocumentModel(item, result));
                }
            }

            return result;
        }

        public static DocumentModel MapToDocumentModel(this ProjectSection source, ObservableCollection<DocumentModel> objects)
        {
            var doc = new Document { Id = source.Id, Title = source.Name, Link = new Uri($@"piloturi://{source.Id}") };
            return new DocumentModel(doc, objects);
        }

        public static List<Document> MapToDocuments(this ObservableCollection<DocumentModel> source)
        {
            var documents = new List<Document>();

            if (source.Any())
            {
                documents.AddRange(source.Select(x => x.Document));
            }

            return documents;
        }
    }
}
