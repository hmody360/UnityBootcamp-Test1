using UnityEngine;

public class Respawner : MonoBehaviour
{
    public Transform moveTo;



    private void OnTriggerEnter(Collider other) // if either the player or enemy fell off the map, they will respawn at the movePoint Transform.
    {
        if(other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            other.transform.position = moveTo.position;
        }
    }

}
