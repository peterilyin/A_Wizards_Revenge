using UnityEngine;
using System.Collections;

using Assets.PixelFantasy.PixelHeroes.Common.Scripts.CharacterScripts;
using Assets.PixelFantasy.PixelHeroes.Common.Scripts.ExampleScripts;
using Assets.PixelFantasy.Common.Scripts;

public class CharacterInfo : MonoBehaviour
{
    public float groundCheckRadius;
    public LayerMask whatLayerIsGround;
    public Transform groundCheckSpot;

    private float knockbackCounter;

    public float knockbackForce;
    public float knockbackFrames;
    public float iFrameFrames;
    public float iFrameCounter;
    public Rigidbody2D myRB;
    public LevelManager levelManager;
    public GameObject bounceBox;
    public bool bBoxActive;
    public Vector3 respawnPos;
    public bool invincible;

    public bool isGrounded;

    public int startHp;
    public int currentHp;


    public bool keyPickUp;
    public GameObject keyObject;

    private CharacterAnimation _animation;

    private Animator myAnimator;

    private BoxCollider2D myBC;

    private CameraController cameraController;

    


    void Start()
    {
        respawnPos = transform.position; 

        levelManager = FindObjectOfType<LevelManager>();

        bounceBox = GameObject.FindWithTag("BounceBox");
        bounceBox.SetActive(false);

        currentHp = startHp;

        myAnimator = GetComponent<Animator>();
        _animation = GetComponent<CharacterAnimation>();

        myBC = gameObject.GetComponent<BoxCollider2D>();

        cameraController = GetComponent<CameraController>();

        keyPickUp = false;
    }

    
    void Update()
    {

        
        isGrounded = Physics2D.OverlapCircle(
               groundCheckSpot.position,
               groundCheckRadius,
               whatLayerIsGround);

        if (knockbackCounter > 0f)
        {
            //want to tick towards 0
            knockbackCounter -= Time.deltaTime;

            //need to know which direction we
            //are facing so that we knockack 
            //the right way
            if (transform.localScale.x > 0f)
            {
                myRB.velocity = new Vector3(-knockbackForce, knockbackForce, 0f);
            }
            else
            {
                myRB.velocity = new Vector3(knockbackForce, knockbackForce, 0f);
            }
        }

        if (myRB.velocity.y < -0.5f)
        {
            //want to activate the BouceBox if we are falling down

            bounceBox.SetActive(true);
            bBoxActive = true;
        }
        else 
        {
            Debug.Log("BounceBox False");
            //any other time we do not want the bouce box active
            bounceBox.SetActive(false);
            bBoxActive = false;
        }

        if (iFrameCounter > 0)
        {
            iFrameCounter -= Time.deltaTime;
            levelManager.invincibilityFrames = true;
        }
        if (iFrameCounter <= 0)
        {
            levelManager.invincibilityFrames = false;
        }

        if (keyPickUp)
        {
            keyObject.SetActive(true);
        } else if (!keyPickUp)
        {
            keyObject.SetActive(false);
        }
    }

    public void Knockback()
    {
        //want the counter to be set to
        //however long we want our knockbackFrames
        knockbackCounter = knockbackFrames;

        //if we start this procedure
        //we should not take damage until
        //our counter ends

        iFrameCounter = iFrameFrames;

        //>> need more kb for bBear
    }

    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.TryGetComponent(out PigInfo currentPig))
        {
            if (bBoxActive)
            {
                Debug.Log("Executing touchPlayer");
                bounceBox.SetActive(false);
                currentPig.takingDamage = true;
                currentPig.currLives -= 1;
                Knockback();
                StartCoroutine("PigAttackDelay", currentPig);
            }

        }

        if (other.transform.TryGetComponent(out BlackBearManager currentMonster))
        {
            if (bBoxActive)
            {
                Debug.Log("Executing touchPlayer");
                bounceBox.SetActive(false);
                currentMonster.takingDamage = true;
                currentMonster.currLives -= 1;
                Knockback();
                //StartCoroutine("PigAttackDelay", currentMonster);
            }

        }
    }

    IEnumerator PigAttackDelay(PigInfo currentPig)
    {
        //Wait for 0.5 seconds

        yield return new WaitForSeconds(0.5f);
        currentPig.takingDamage = false;
        bounceBox.SetActive(false);
        Debug.Log("Ended pigattackdelay");
    }


    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "AttackBox")
        {
            if (!invincible)
            {
                takeDamage(1);
            }
        }

        if (other.tag == "Spike")
        {
            if (!invincible)
            {
                takeDamage(1);
            }
        }

        if (other.tag == "CircularSaw")
        {
            if (!invincible)
            {
                takeDamage(1);
            }
        }

        if (other.tag == "KillPlane")
        {
            takeDamage(999);
        }

        if (other.tag == "IronCoin")
        {
            levelManager.coins += 1;
        } else if (other.tag == "GoldCoin")
        {
            levelManager.coins += 3;
        }

        if (other.tag == "smallSilverKeyPickUp")
        {
            Debug.Log("keyinsideCI");
            keyPickUp = true;
        }

        if (other.tag == "Cage")
        {
            if (keyPickUp)
            {
                keyPickUp = false;
            }
        }
    }

    public void takeDamage(int amount)
    {
        invincible = true;
        currentHp -= amount;

        if (currentHp <= 0)
        {
            StartCoroutine("Death");
        } else
        {
            StartCoroutine("iFrames");
        }
    }

    public void setCurrentHp(int amount)
    {
        currentHp = amount;
    }


    public IEnumerator iFrames()
    {
        Knockback();
        _animation.Hit();
        //myBC.enabled = false;
        
        //myBC.enabled = true;
        yield return new WaitForSeconds(iFrameCounter);

        invincible = false;
    }

    public IEnumerator Death()
    {
        _animation.Die();
        transform.position = respawnPos;
        currentHp = startHp;
        
        yield return new WaitForSeconds(iFrameCounter);
        

        invincible = false;
    }

    public bool deActivateKey()
    {
        keyPickUp = false;
        Debug.Log("keypickup from deK is: " + keyPickUp);
        return keyPickUp;
    }

}
