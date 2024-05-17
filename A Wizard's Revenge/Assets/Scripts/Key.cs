using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public CharacterInfo player;
    public GameObject pickUp;

    void Start()
    {
        pickUp.SetActive(false);
        StartCoroutine("pickupDelay");
    }

    public void keyPickup()
    {
        Debug.Log("executing keypickup");
        player.keyPickUp = true;
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("keyPickUp");
            player.keyPickUp = true;
            Destroy(gameObject);
        }
    }

    IEnumerator pickupDelay()
    {
        yield return new WaitForSeconds(0.8f);
        pickUp.SetActive(true);
    }

}
