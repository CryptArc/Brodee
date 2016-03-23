using System;
using System.Collections.Generic;
using Brodee.Components;
using Brodee.Triggers;
using UnityEngine;

namespace Brodee
{
    public class HandlerItem
    {
        public IHandler Handler { get; }
        public Scene Scene { get; }

        public HandlerItem(IHandler handler, Scene scene)
        {
            Handler = handler;
            Scene = scene;
        }
    }

    public interface IHandlerHub
    {
        void RegisterOnTrigger<T>(TriggerHandler<T> handler) where T : Trigger;
        void Register(Handler handler, HowOftenToProcess howOftenToProcess, Scene scene);
    }

    public class HandlerHub : IHandlerHub
    {
        private readonly Dictionary<Type, List<ITriggerHandler>> _triggerHandlers = new Dictionary<Type, List<ITriggerHandler>>();
        private readonly List<HandlerItem> _perSecondHandlers = new List<HandlerItem>();
        private readonly List<HandlerItem> _perFrameHandlers = new List<HandlerItem>();
        private readonly Queue<Trigger> _triggersQueue = new Queue<Trigger>();

        private DateTime _nextPerSecondTime = DateTime.MinValue;

        public void RegisterOnTrigger<T>(TriggerHandler<T> handler) where T : Trigger
        {
            var type = typeof(T);
            if (!_triggerHandlers.ContainsKey(type))
            {
                _triggerHandlers.Add(type, new List<ITriggerHandler>());
            }
            _triggerHandlers[type].Add(handler);
        }

        public void Register(Handler handler, HowOftenToProcess howOftenToProcess, Scene scene)
        {
            if (howOftenToProcess == HowOftenToProcess.Never || scene == Scene.None)
                return; //Protect against default enums

            switch (howOftenToProcess)
            {
                case HowOftenToProcess.EverySecond:
                    _perSecondHandlers.Add(new HandlerItem(handler, scene));
                    break;
                case HowOftenToProcess.EveryFrame:
                    _perFrameHandlers.Add(new HandlerItem(handler, scene));
                    break;
            }
        }

        public void AddTrigger(Trigger trigger)
        {
            _triggersQueue.Enqueue(trigger);
        }

        public void ProcessActions(IGameState previous, IGameState next)
        {
            List<ITriggerHandler> handlerList;

            foreach (var perFrameHandler in _perFrameHandlers)// PerFrame Handlers
            {
                if (perFrameHandler.Scene.IsSet(next.Mode))
                {
                    ExecuteHandler(perFrameHandler, previous, next);
                }
            }

            while (_triggersQueue.Count > 0) // Triggers
            {
                var trigger = _triggersQueue.Dequeue();
                var type = trigger.GetType();
                Logger.AppendLine($"Attempting to trigger type:{type}");
                if (_triggerHandlers.TryGetValue(type, out handlerList))
                {
                    foreach (var handlerItem in handlerList)
                    {
                        ExecuteTriggerHandler(handlerItem, trigger, next);
                    }
                }
            }
            if (DateTime.UtcNow > _nextPerSecondTime) // PerSecond Handlers
            {
                foreach (var perSecondHandler in _perSecondHandlers)
                {
                    if (perSecondHandler.Scene.IsSet(next.Mode))
                        ExecuteHandler(perSecondHandler, previous, next);
                }
                _nextPerSecondTime = DateTime.UtcNow + TimeSpan.FromSeconds(1);
            }

        }

        private void ExecuteHandler(HandlerItem handlerItem, IGameState previous, IGameState next)
        {
            try
            {
                handlerItem.Handler.SpecificHandle(previous, next);
            }
            catch (Exception e)
            {
                Logger.AppendLine($"Error handling handler:{handlerItem.Handler.GetType()}");
                Logger.AppendLine(e.ToString());
            }
        }
        private void ExecuteTriggerHandler(ITriggerHandler triggerHandler, Trigger trigger, IGameState next)
        {
            try
            {
                triggerHandler.Handle(trigger, next);
            }
            catch (Exception e)
            {
                Logger.AppendLine($"Error handling triggerHandler:{triggerHandler.GetType()}");
                Logger.AppendLine(e.ToString());
            }
        }
    }
}