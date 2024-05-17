using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    void Start()
    {
        StartCoroutine("destroyOrb");
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out PigInfo currentMonster))
        {
            Debug.Log("pig should be cooked");
            currentMonster.takingDamage = true;
            currentMonster.currLives -= 1;

        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PinkPig")
        {
            
            PigInfo currentPig = other.gameObject.GetComponent<PigInfo>();
            //currentPig.takingDamage = true;
            currentPig.currLives -= 1;
            Destroy(gameObject);
        }

        if (other.tag == "BlackBear")
        {
            
            BlackBearManager currentBear = other.gameObject.GetComponent<BlackBearManager>();
            //currentPig.takingDamage = true;
            currentBear.currLives -= 1;
            Destroy(gameObject);
        }
    }


    IEnumerator destroyOrb()
    {
        Debug.Log(0.8f + PlayerPrefs.GetInt("DistanceUpgrade"));
        Debug.Log(PlayerPrefs.GetInt("DistanceUpgrade"));
        yield return new WaitForSeconds(0.8f + PlayerPrefs.GetInt("DistanceUpgrade"));
        Debug.Log("orb destoryed");
        Destroy(gameObject);
    }

}
