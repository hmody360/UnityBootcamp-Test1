using UnityEngine;

public class FollowPlayerCam : MonoBehaviour
{
    public Transform target; //GameObj's transform to Follow

    [SerializeField] private Vector3 cameraOffset = new Vector3(0f, 10f, -8f); //offset from target


    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = target.position + cameraOffset; // Updating the Camera's Postion based on player and offset from player.
    }

    private void OnDrawGizmosSelected() //Gizmo to draw camera position when selected.
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(target.position + cameraOffset, 0.4f);
    }
}
