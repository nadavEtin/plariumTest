﻿using Plarium.Assets.GameCore.Events;
using UnityEngine;

namespace GameCore.Factories
{
    public class GenericObjectFactory : BaseGameObjectFactory
    {
        public GenericObjectFactory(EventBus eventBus)
        {
            
        }
        
        public override GameObject Create()
        {
            throw new System.NotImplementedException();
        }
    }
}