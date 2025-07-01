using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    // we use a reference to the player to change any variables within that script
    public PlayerController player;

    void Start()
    {
        // get a reference to the player 
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void OnTriggerEnter(Collider other)
    {
        // if the player collides with the player, the player's coin score will increase, and we destroy the coin object
        if (other.name == "Player")
        {
            player.coinCount++;
            Destroy(this.gameObject);
        }
    }
}