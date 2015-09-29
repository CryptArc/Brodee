using System;
using System.Collections.Generic;
using BrodeStone.Handlers;
using BrodeStone.Triggers;
using UnityEngine;

namespace BrodeStone
{
    public class HandlerHub
    {
        private readonly GameObject _parent;
        private readonly Dictionary<Type, List<IHandler>> _handlers = new Dictionary<Type, List<IHandler>>();

        public HandlerHub(GameObject parent)
        {
            _parent = parent;
        }

        public void RegisterOnTrigger<T>(Handler handler, ScenesToProcessOn scenesToProcessOn) where T : Trigger
        {
            var type = typeof(T);
            if (!_handlers.ContainsKey(type))
            {
                _handlers.Add(type, new List<IHandler>());
            }
            handler.Setup(_parent);
            _handlers[type].Add(handler);
        }

        public void Register(Handler handler, HowOftenToProcess howOftenToProcess, ScenesToProcessOn scenesToProcessOn)
        {
            
        }

        public void ProcessActions(GameState previous, GameState next)
        {
            _handlers.ForEach(type =>
            {
                foreach (var handler in type.Value)
                {
                    try
                    {
                        handler.SpecificHandle(previous, next);
                    }
                    catch (Exception e)
                    {
                        Logger.AppendLine($"Error handling type:{type.ToString()} handler:{handler.GetType()}");
                        Logger.AppendLine(e.ToString());
                    }
                }
            });
        }
    }
}