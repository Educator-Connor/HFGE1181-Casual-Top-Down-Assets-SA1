using System;
using UnityEngine;

public class FoodSizeHandler : SizeHandler
{
   private void OnDestroy()
   {
      SpawnManager.Instance.CleanFoodArray(this);
   }
}
