using System;
using UnityEngine;

namespace Stunt
{
    [Serializable]
    public struct Spring
    {
        [HideInInspector] public float maxRange => Range.x;
        [HideInInspector] public float minRange => Range.y;
        [HideInInspector] public float offset;

        public Vector2 Range;
        public float strength, damping, restLength;
    }
}