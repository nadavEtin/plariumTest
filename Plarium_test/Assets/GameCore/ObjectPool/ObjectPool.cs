﻿using System;
using System.Collections.Generic;
using GameCore.Factories;
using UnityEngine;

namespace Assets.Scripts.Utility
{
    public enum ObjectTypes
    {
        GenericObject
    }

    public class ObjectPool : IObjectPool
    {
        private readonly Dictionary<ObjectTypes, List<GameObject>> _objectPool;
        private readonly Dictionary<ObjectTypes, BaseGameObjectFactory> _objectFactory;

        public ObjectPool()
        {
            _objectPool = new Dictionary<ObjectTypes, List<GameObject>>();
            _objectFactory = new Dictionary<ObjectTypes, BaseGameObjectFactory>();
        }

        public void AddObjectToPool(GameObject obj, ObjectTypes type)
        {
            if (_objectPool.ContainsKey(type) == false)
                _objectPool.Add(type, new List<GameObject>());

            obj.SetActive(false);
            _objectPool[type].Add(obj);
            obj.SetActive(false);
        }

        public GameObject GetObjectFromPool(ObjectTypes type)
        {
            if (_objectPool.ContainsKey(type) && _objectPool[type].Count > 0)
            {
                var returnObj = _objectPool[type][0];
                _objectPool[type].RemoveAt(0);
                returnObj.SetActive(true);
                return returnObj;
            }

            //TODO: set this up
            /*else if (_assetRefs.PrefabTypes.ContainsKey(type))
            {
                return CreateObject(type);
            }*/
            else
            {
                Debug.LogError("incorrect prefab type: " + Enum.GetName(typeof(ObjectTypes), type));
                return null;
            }
        }

        private GameObject CreateObject(ObjectTypes type)
        {
            //TODO: set this up
            return null;
            //return Object.Instantiate(_assetRefs.PrefabTypes[type]);
        }
    }
}
