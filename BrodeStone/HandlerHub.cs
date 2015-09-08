using System;
using System.Collections.Generic;
using UnityEngine;

namespace BrodeStone
{
    public class HandlerHub
    {
        private readonly Dictionary<HandlerType, IHandler> _handlers = new Dictionary<HandlerType, IHandler>();
        private readonly HashSet<HandlerType> _typeSet = new HashSet<HandlerType>();

        public bool HasActions()
        {
            return _typeSet.Count > 0;
        }

        public void Register(HandlerType type, IHandler handler)
        {
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, handler);
            }
        }

        public void ProcessType(HandlerType type)
        {
            _typeSet.Add(type);
        }

        public void ProcessActions(GameObject component, GameState previous, GameState next)
        {
            IHandler handler;
            _typeSet.ForEach(type =>
            {
                try
                {
                    if (_handlers.TryGetValue(type, out handler))
                    {
                        Logger.AppendLine(string.Format("HandlerHub.ProcessAction.{0}", type.ToString()));
                        handler.Handle(component, previous, next);
                    }
                }
                catch (Exception e)
                {
                    Logger.AppendLine(string.Format("Error handling type:{0}", type.ToString()));
                    Logger.AppendLine(e.ToString());
                }
            });
            _typeSet.Clear();
        }
    }
}