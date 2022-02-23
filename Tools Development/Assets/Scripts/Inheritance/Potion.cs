using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Items
{
   public override void PickUp()
    {
        base.PickUp();
        Debug.Log( "This is potion" );
    }
}
