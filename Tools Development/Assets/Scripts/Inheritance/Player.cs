using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private void OnTriggerEnter( Collider other )
    {
        if( other.CompareTag("Items"))
        {
            Debug.Log( "something" );
            other.GetComponent<Items>().PickUp();
        }
    }

}
