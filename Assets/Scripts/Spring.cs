using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public struct Spring
    {
        [HideInInspector] public float force, offset;

        public Vector2 Range;
        public float maxRange => Range.x;
        public float minRange => Range.y;

        public float strength, damping, restLength;
    }
}