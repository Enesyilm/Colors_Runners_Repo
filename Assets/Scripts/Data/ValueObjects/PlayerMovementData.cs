using System;

namespace Data.ValueObjects
{
    [Serializable]
    public class PlayerMovementData
    {
        public float ForwardSpeed = 5;
        public float SidewaysSpeed = 3.5f;
        public float IdleForwardSpeed = 3f;  //YENi
        public float IdleSidewaysSpeed = 3f; //Yeni
    }
}


