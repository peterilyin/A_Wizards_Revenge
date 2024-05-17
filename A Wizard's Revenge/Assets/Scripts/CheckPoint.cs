using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public GameObject redFlag;
    public GameObject greenFlag;

    public CharacterInfo player;

    public bool alreadyTriggered;

    void Start()
    {
        greenFlag.SetActive(false);
        player = FindObjectOfType<CharacterInfo>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!alreadyTriggered)
            {
                redFlag.SetActive(false);
                player.respawnPos = transform.position;
                greenFlag.SetActive(true);
                alreadyTriggered = true;
            }
        }
    }
}
