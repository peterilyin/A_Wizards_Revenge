using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public CharacterInfo player;
    public GameObject pickUp;

    void Start()
    {
        player = FindObjectOfType<CharacterInfo>();
        pickUp.SetActive(false);

        StartCoroutine("pickupDelay");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

    IEnumerator pickupDelay()
    {
        yield return new WaitForSeconds(0.8f);
        pickUp.SetActive(true);
    }
}
