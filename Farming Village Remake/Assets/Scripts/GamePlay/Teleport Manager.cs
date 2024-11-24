using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public GameObject Player;

    [Header("Teleport Points")]
    public GameObject Enter;
    public GameObject Exit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //Debug.Log("Trigger detected");

            // Check if the teleport trigger is the Enter or Exit point
            if (this.gameObject == Enter)
            {
                Debug.Log("Player entered. Teleporting to Exit.");
                Teleport(Player, Exit, 2);
            }
            else if (this.gameObject == Exit)
            {
                Debug.Log("Player exited. Teleporting to Enter.");
                Teleport(Player, Enter, -2);
            }
        }
    }

    private void Teleport(GameObject player, GameObject destination, float yOffset)
    {
        player.transform.position = new Vector3(destination.transform.position.x, destination.transform.position.y + yOffset, destination.transform.position.z);
        Debug.Log($"Player teleported to: {destination.name} with yOffset: {yOffset}");
    }
}
