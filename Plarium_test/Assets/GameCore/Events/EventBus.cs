﻿using System;
using System.Collections.Generic;

namespace Plarium.Assets.GameCore.Events
{
    public enum GameplayEvent
    {
        GameStart, GameEnd, Error
    }

    public class EventBus
    {
        private readonly Dictionary<GameplayEvent, List<Action<BaseEventParams>>> _subscription = new();

        public void Subscribe(GameplayEvent eventType, Action<BaseEventParams> handler)
        {
            var handlerList = _subscription[eventType];
            if (handlerList == null)
            {
                handlerList = new List<Action<BaseEventParams>>();
                _subscription.Add(eventType, handlerList);
            }

            if (handlerList.Contains(handler) == false)
                handlerList.Add(handler);
        }

        public void Unsubscribe(GameplayEvent eventType, Action<BaseEventParams> handler)
        {
            _subscription[eventType]?.Remove(handler);
        }

        public void Publish(GameplayEvent eventType, BaseEventParams eventParams)
        {
            var handlerList = _subscription[eventType];
            if (handlerList == null)
                return;

            foreach (var handler in handlerList)
                handler?.Invoke(eventParams);
        }
    }
}
