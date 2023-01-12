using System;
using UnityEngine;

namespace Burnout
{
    [Serializable]
    public struct Spring
    {
        [HideInInspector]
        public float Force, Offset;

        public Vector2 Range;
        public float MaxRange => Range.x;
        public float MinRange => Range.y;

        public float Strength, Damping, RestLength;
    }
}