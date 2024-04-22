using SkeletonApi.IotHub.Model;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace SkeletonApi.IotHub.Services.Handler
{
    public class IoTHubTaskEventHandler : IIoTHubEventHandler<IEnumerable<TaskModel>>, IDisposable
    {
        private readonly BehaviorSubject<IEnumerable<TaskModel>> _task;
        private readonly Dictionary<string, IDisposable> _subscribers;

        public IoTHubTaskEventHandler()
        {
            _task = new BehaviorSubject<IEnumerable<TaskModel>>(new List<TaskModel>());
            _subscribers = new Dictionary<string, IDisposable>();
        }

        public void Dispatch(IEnumerable<TaskModel> eventMessage)
        {
            _task.OnNext(eventMessage);
        }

        public void Subscribe(string subscriberName, Action<IEnumerable<TaskModel>> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _task.Subscribe(action));
            }
        }

        public IObservable<IEnumerable<TaskModel>> Observe()
        {
            return _task;
        }

        public void Subscribe(string subscriberName, Func<IEnumerable<TaskModel>, bool> predicate, Action<IEnumerable<TaskModel>> action)
        {
            if (!_subscribers.ContainsKey(subscriberName))
            {
                _subscribers.Add(subscriberName, _task.Where(predicate).Subscribe(action));
            }
        }

        public void Dispose()
        {
            if (_task != null)
            {
                _task.Dispose();
            }

            foreach (var subscriber in _subscribers)
            {
                subscriber.Value.Dispose();
            }
        }
    }
}