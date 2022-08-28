using System;

namespace Data.ValueObjects
{
    [Serializable]
    public class SideObjectData
    {
      public int PayedAmount;
      public int TotalRequiredAmount;
      public bool IsCompleted;
    }
}