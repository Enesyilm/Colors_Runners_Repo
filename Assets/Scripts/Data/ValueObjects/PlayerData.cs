using System;
using Data.ValueObjects;

namespace Data.ValueObjects
{
    [Serializable]
    public class PlayerData
    {
        public PlayerMovementData PlayerMovementData;
        public float ScaleCounter = 1.5f;
    }
}


