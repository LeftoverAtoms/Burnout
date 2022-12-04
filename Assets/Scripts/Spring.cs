using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public struct Spring
    {
        [HideInInspector] public float maxRange => Range.x;
        [HideInInspector] public float minRange => Range.y;
        [HideInInspector] public float force, offset;

        public Vector2 Range;
        public float strength, damping, restLength;
    }
}