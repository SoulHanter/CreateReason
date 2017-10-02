using Ascon.Pilot.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ascon.ManagerEdition.Common.PilotIceCommon.Observers
{
    public class InfoLoader : IObserver<IDataObject>
    {
        private readonly IObjectsRepository _repository;

        private Action<IList<IDataObject>> _onLoadedAction;

        private IDisposable _subscription;

        private List<IDataObject> _objects;

        private Guid[] _ids;

        public InfoLoader(IObjectsRepository repository)
        {
            _repository = repository;
        }

        public void Load(Action<IList<IDataObject>> onLoadedAction, params Guid[] ids)
        {
            _objects = new List<IDataObject>();
            _ids = ids;
            _onLoadedAction = onLoadedAction;
            _subscription = _repository.SubscribeObjects(ids).Subscribe(this);
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(IDataObject value)
        {
            if (value.State == DataState.Loaded)
            {
                _objects.Add(value);
                if (_objects.Count == _ids.Count())
                    _onLoadedAction(_objects);
            }
        }
    }
}
