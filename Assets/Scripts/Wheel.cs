using UnityEngine;

namespace StuntGame
{
    [System.Serializable]
    public struct Wheel
    {
        [HideInInspector]
        public string name;

        public GameObject attachPoint;
    }
}