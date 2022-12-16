using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public struct Spring
    {
        [HideInInspector] public float force, offset;

        public Vector2 range;
        public float maxRange => range.x;
        public float minRange => range.y;

        public float strength, damping, restLength;
    }
}