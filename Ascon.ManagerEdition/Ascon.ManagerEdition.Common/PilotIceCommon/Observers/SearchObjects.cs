using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Common.PilotIceCommon.Observers
{
    public class SearchObjects : IObserver<ISearchResult>
    {
        private IDisposable _subscription;
        private List<IDataObject> _objects;
        private ISearchService _searchService;
        private Action<List<Guid>> _onLoadedAction;


        public SearchObjects(ISearchService searchService)
        {
            _searchService = searchService;
            _objects = new List<IDataObject>();
        }

        public void Search(Action<List<Guid>> onLoadedAction, int typeId, string attribute)
        {
            _onLoadedAction = onLoadedAction;
            var queryBuilder = GetQueryBuilder(typeId, attribute);
            _subscription = _searchService.Search(queryBuilder).Subscribe(this);
        }

        private IQueryBuilder GetQueryBuilder(int typeId, string attribute)
        {
            var builder = _searchService.GetEmptyQueryBuilder();
            builder.Must(ObjectFields.TypeId.Be(typeId));
            builder.Must(ObjectFields.AllText.Be($"{attribute}*"));
            return builder;
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(ISearchResult value)
        {
            _onLoadedAction(value.Result.ToList());
            _subscription?.Dispose();
        }
    }
}
