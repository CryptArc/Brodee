using System;
using System.Collections.Generic;
using Brodee.Handlers;
using Brodee.Triggers;
using UnityEngine;

namespace Brodee
{
    public class HandlerItem
    {
        public IHandler Handler { get; }
        public Handlers.Scene Scene { get; }

        public HandlerItem(IHandler handler, Handlers.Scene scene)
        {
            Handler = handler;
            Scene = scene;
        }
    }

    public class HandlerHub
    {
        private readonly GameObject _parent;
        private readonly Dictionary<Type, List<HandlerItem>> _triggerHandlers = new Dictionary<Type, List<HandlerItem>>();
        private readonly List<HandlerItem> _perSecondHandlers = new List<HandlerItem>();
        private readonly List<HandlerItem> _perFrameHandlers = new List<HandlerItem>();
        private readonly Queue<object> _triggersQueue = new Queue<object>();

        private DateTime _nextPerSecondTime = DateTime.MinValue;

        public HandlerHub(GameObject parent)
        {
            _parent = parent;
        }

        public void RegisterOnTrigger<T>(Handler handler, Handlers.Scene scene) where T : Trigger
        {
            var type = typeof(T);
            if (!_triggerHandlers.ContainsKey(type))
            {
                _triggerHandlers.Add(type, new List<HandlerItem>());
            }
            handler.Setup(_parent);
            _triggerHandlers[type].Add(new HandlerItem(handler, scene));
        }

        public void Register(Handler handler, HowOftenToProcess howOftenToProcess, Handlers.Scene scene)
        {
            handler.Setup(_parent);
            if (howOftenToProcess == HowOftenToProcess.Never || scene == Handlers.Scene.None)
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

        public void AddTrigger(object trigger)
        {
            _triggersQueue.Enqueue(trigger);
        }

        public void ProcessActions(GameState previous, GameState next)
        {
            List<HandlerItem> handlerList;
            while (_triggersQueue.Count > 0) // Triggers
            {
                var trigger = _triggersQueue.Dequeue();
                var type = trigger.GetType();
                Logger.AppendLine($"Attempting to trigger type:{type}");
                if (_triggerHandlers.TryGetValue(type, out handlerList))
                {
                    foreach (var handlerItem in handlerList)
                    {
                        Logger.AppendLine($"Attempting to handler:{handlerItem.Handler.GetType()}");
                        //if (handlerItem.Scene.IsSet(next.Mode)) //TODO: Fix
                        {
                            Logger.AppendLine($"Executing handler:{handlerItem.Handler.GetType()}");
                            ExecuteHandler(handlerItem, previous, next);
                        }
                    }
                }
            }
            if (DateTime.UtcNow > _nextPerSecondTime) // PerSecond Handlers
            {
                foreach (var perSecondHandler in _perSecondHandlers)
                {
                    ExecuteHandler(perSecondHandler, previous, next);
                }
                _nextPerSecondTime = DateTime.UtcNow + TimeSpan.FromSeconds(1);
            }
            foreach (var perFrameHandler in _perFrameHandlers)// PerFrame Handlers
            {
                ExecuteHandler(perFrameHandler, previous, next);
            }
        }

        private void ExecuteHandler(HandlerItem handlerItem, GameState previous, GameState next)
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
    }
}