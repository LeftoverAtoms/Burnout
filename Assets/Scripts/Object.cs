using UnityEngine;

namespace Burnout
{
    /// <summary> Base class for all objects. </summary>
    public class Object
    {
        [HideInInspector]
        public Vector3 Position;

        public Vector3 LocalPosition;
    }
}