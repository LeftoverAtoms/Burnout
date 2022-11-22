using UnityEngine;

namespace StuntGame
{
    [System.Serializable]
    public struct Spring
    {
        [HideInInspector] public float minRange => Range.x;
        [HideInInspector] public float maxRange => Range.y;
        [HideInInspector] public float length;

        public Vector2 Range;
        public float damping, stiffness, restLength;
        //public bool isCompressed;
    }
}