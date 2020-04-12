using PizzaOrder.Business.Models;

using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PizzaOrder.Business.Services
{
    public interface IEventService
    {
        IObservable<EventDataModel> OnCreateObservable();

        void CreateOrderEvent(EventDataModel orderEvent);
        void StatusUpdateEvent(EventDataModel orderEvent);
        IObservable<EventDataModel> OnStatusUpdateObservable();
    }

    public class EventService : IEventService
    {
        private readonly ISubject<EventDataModel> onCreateSubject = new ReplaySubject<EventDataModel>(1);

        public void CreateOrderEvent(EventDataModel orderEvent) => onCreateSubject.OnNext(orderEvent);

        public IObservable<EventDataModel> OnCreateObservable() => onCreateSubject.AsObservable();


        private readonly ISubject<EventDataModel> onStatusUpdateSubject = new ReplaySubject<EventDataModel>(1);

        public void StatusUpdateEvent(EventDataModel orderEvent) => onStatusUpdateSubject.OnNext(orderEvent);

        public IObservable<EventDataModel> OnStatusUpdateObservable() => onStatusUpdateSubject.AsObservable();
    }
}
