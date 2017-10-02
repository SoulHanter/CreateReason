using System;
using System.Collections.Generic;
using System.Linq;
using Ascon.Pilot.SDK;

namespace Ascon.ManagerEdition.Common.PilotIceCommon.Observers
{
    public class ObjectLoader : IObjectsLoader
    {
        #region Private

        private readonly IObjectsRepository _repository;

        private Action<IList<IDataObject>> _onLoadedAction;

        private Func<IType, bool> _typesFilter;

        private List<IDisposable> _subscriptions;

        private List<IDataObject> _objects;

        private Guid[] _ids;

        private int _counterCurrent = 0;

        private int _counterAll = 1;

        private bool _loaded = false;

        #endregion

        public ObjectLoader(IObjectsRepository repository)
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
                _objects.Add(value);
                _counterCurrent++;

                if (value.Children.Count != 0)
                {
                    var filteredChildren = value.TypesByChildren.Where(x => _typesFilter(_repository.GetType(x.Value)))
                                                                .Select(x => x.Key)
                                                                .ToList();

                    if (filteredChildren.Count != 0)
                    {
                        _counterAll += filteredChildren.Count;
                        _subscriptions.Add(_repository
                            .SubscribeObjects(filteredChildren)
                            .Subscribe(this));
                    }
                }

                if (_counterAll == _counterCurrent)
                {
                    _loaded = true;

                    if (_subscriptions.Count != 0)
                    {
                        foreach (var subscription in _subscriptions)
                        {
                            subscription?.Dispose();
                        }

                        _subscriptions.Clear();
                    }

                    _onLoadedAction(_objects);

                    _counterCurrent = 0;
                    _counterAll = 1;
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
