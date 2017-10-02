using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Common.PilotIceCommon.Observers
{
    public class ParentLoader : IObjectsLoader
    {
        #region Private

        private readonly IObjectsRepository _repository;

        private Action<IList<IDataObject>> _onLoadedAction;

        private List<IDisposable> _subscriptions;

        private List<IDataObject> _objects;

        private Guid[] _ids;

        private bool _loaded = false;
        private Func<IType, bool> _typesFilter;

        #endregion

        public ParentLoader(IObjectsRepository repository)
        {
            _repository = repository;
            _subscriptions = new List<IDisposable>();
            _objects = new List<IDataObject>();
        }

        /// <summary>
        /// Load objects tree
        /// </summary>
        /// <param name="onLoadedAction">Complete loading objects</param>
        /// <param name="typesFilter"> Objects type filter</param>
        /// <param name="ids">Id or Ids objects</param>
        public void Load(Action<IList<IDataObject>> onLoadedAction, Func<IType, bool> typesFilter, params Guid[] ids)
        {
            _ids = ids;
            _typesFilter = typesFilter;
            _onLoadedAction = onLoadedAction;
            _subscriptions.Add(_repository.SubscribeObjects(ids).Subscribe(this));
        }

        public void OnNext(IDataObject value)
        {
            if (!_loaded && value.State == DataState.Loaded)
            {

                _subscriptions.Add(_repository
                                   .SubscribeObjects(new Guid[] { value.ParentId })
                                   .Subscribe(this));

                if (_typesFilter(value.Type))
                {
                    _loaded = true;
                    _objects.Add(value);

                    if (_subscriptions.Count != 0)
                    {
                        foreach (var subscription in _subscriptions)
                        {
                            subscription?.Dispose();
                        }

                        _subscriptions.Clear();
                    }

                    _onLoadedAction(_objects);
                }
            }
        }

        public void OnError(Exception error)
        {
        }

        public void OnCompleted()
        {
        }
    }
}
