﻿using UnityEngine;

namespace CustomSabersLite.Utilities.Extensions
{
    internal static class GameObjectExtensions
    {
        public static T TryGetComponentOrDefault<T>(this GameObject obj) where T : MonoBehaviour => 
            obj.GetComponent<T>() ?? obj.AddComponent<T>();
    }
}
