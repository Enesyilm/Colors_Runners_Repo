using System;
using UnityEngine;

namespace Data.ValueObjects
{
    [Serializable]
    public class InputData
    {
        public float HorizontalInputSpeed = 2f;
        public float InputSpeed = 2f;
        public Vector2 ClampSides = new Vector2(-3, 3);
        public float ClampSpeed = 0.007f;
        public float IdleInputSpeed = 1.161f;
    }
}