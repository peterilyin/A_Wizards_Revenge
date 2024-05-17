using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cage : MonoBehaviour
{
    public CharacterInfo player;
    public float fadeTime;
    public SpriteRenderer mySR;

    void Start()
    {
        //mySR = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "smallSilverKey")
        {
            bool yes = player.deActivateKey();
            Debug.Log("yes is: " + yes); 
            StartCoroutine("FadeAway");
        }
    }

    IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(0.2f);
        
        

        while (fadeTime > 0f)
        {
            mySR.color = new Color(1f, 1f, 1f, fadeTime / 100);

            yield return new WaitForSeconds(0.01f);
            fadeTime -= 1;
        }
        
        Destroy(gameObject);

    }
}
