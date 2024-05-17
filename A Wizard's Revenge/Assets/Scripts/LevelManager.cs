using System.Collections.Generic;
using System.Collections;
using System.Linq;
using TMPro;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    //want a timer to know
    //when to respawn

    //want the object that we are respawning
    public float respawnCountdown;

    //we need a GameObject that can hold our
    //particle system so that we can use it
    //in the world
    public GameObject deathExplosion;


    //public TextMeshProUGUI <<<<<

    //want to keep track of how many
    //gems we colleced
    //public int gemAmount;


    //want the text component
    public TextMeshProUGUI coinText;


    //want to modify health
    //want to keep track of health as as number
    public int maxHealth;
    public float currentHealth;

    //keep track of if we are
    //currently invincible
    //do not want to keep taking damage
    public bool invincibilityFrames;

    //want control of lives
    //want control of text on screen
    public int startLivesCount;
    public int currentLivesCount;
    //public Text livesText;

    //public GameObject gameOverScreen;

    //public Sprite spriteMaxHealth;
    //public Sprite spriteMidHealth;
    //public Sprite spriteEndHealth;

    //public GameObject playerIcon;

    //we want a threshold for gems to give us an extra life
    //we want a hidden counter that keep track if the threshold has been reached
    public int bonusLifeThreshold;

    //want a reference to the sound
    //public AudioSource gemSound;

    //want level audio, want game over audio
    //public AudioSource levelMusic;
    //public AudioSource gameOverMusic;

    //public AudioSource endOfLevelMusic;
    //public AudioSource victoryMusic;

    //we want to know if our coroutine is respawning or not
    public bool respawnActive;

    public SpriteRenderer mySR;

    private int gemExtraLifeCounter;

    //public Image playerIconImage;



    //want to get our dying animation



    public CharacterInfo player; //gives us access to all variables

    //want to know health object
    //want to know if we can respawn 
    //public HealthBar healthBar;
    private bool canRespawn;

    //we want to get our KillPlane
    private GameObject killPlane;


    //we want a List of the objects that have the reset script
    //private List<ResetOnPlayerDeath> objectsResetting;


    public GameObject heartIcon1;
    public GameObject heartIcon2;
    public GameObject heartIcon3;
    public Image heartImage1;
    public Image heartImage2;
    public Image heartImage3;

    public GameObject coinTextHolder;
    

    public int coins;

    public GameObject pauseScreen;

    public bool giveCoins;
    

    void Start()
    {
        //if (PlayerPrefs.HasKey("GemAmount"))
        //{
        //    gemAmount = PlayerPrefs.GetInt("GemAmount");
        //}
        //else
        //{
        //    gemAmount = 0;
        //}

        //if (PlayerPrefs.HasKey("PlayerLives"))
        //{
        //    currentLivesCount = PlayerPrefs.GetInt("PlayerLives");
        //}
        //else
        //{
        //    currentLivesCount = startLivesCount;
        //}
        //we need to find
        //the object which has this script
        //on it
        //cannot just do normal GetComponent
        //becuase that only grabs components
        //that are apart of ourselves
        player = FindObjectOfType<CharacterInfo>();

        //finds us the GameObject in the scene with specified tag
        //there is a plural version of this method that we will use
        //later in our game
        killPlane = GameObject.FindGameObjectWithTag("KillPlane");


        //want to have 0 intead of X's
        //gemText.text = "gems: " + gemAmount;

        //set up new instance variables
        //currentHealth = maxHealth;
        //healthBar = GameObject.FindObjectOfType<HealthBar>();
        //healthBar.SetMaxHealth(maxHealth);
        canRespawn = true;

        //we want to fill the List at the start of the world
        //we want to find all the Objects that have the script
        //the FindObjects returns an array so we need that and the nconvert that to a list
        //objectsResetting = FindObjectsOfType<ResetOnPlayerDeath>().ToList();


        //livesText.text = "x: " + currentLivesCount;

        //playerIcon = GameObject.FindGameObjectWithTag("PlayerIcon");
        //playerIconImage = playerIcon.GetComponent<Image>();

        //mySR = player.GetComponent<SpriteRenderer>();

        currentHealth = player.currentHp;
        heartImage1 = heartIcon1.GetComponent<Image>();
        heartImage2 = heartIcon2.GetComponent<Image>();
        heartImage3 = heartIcon3.GetComponent<Image>();

        coins = PlayerPrefs.GetInt("Coins");

        coinTextHolder = GameObject.FindWithTag("CoinText");
        //coinText = coinTextHolder.GetComponent<TextMeshProUGUI>();

        //pauseScreenScript = pauseScreen.GetComponent<PauseScreen>();

    }

    void Update()
    {
        currentHealth = player.currentHp;
        if (currentHealth <= 0)
        {
            //Respawn();
        }

        if (currentHealth == 3)
        {
            heartImage1.enabled = true;
            heartImage2.enabled = true;
            heartImage3.enabled = true;
        }
        else if (currentHealth == 2)
        {
            heartImage1.enabled = true;
            heartImage2.enabled = true;
            heartImage3.enabled = false;
        }
        else if (currentHealth == 1)
        {
            heartImage1.enabled = true;
            heartImage2.enabled = false;
            heartImage3.enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !pauseScreen.activeSelf)
        {
            pauseScreen.SetActive(true);

            PauseScreen pauseScreenScript = pauseScreen.GetComponent<PauseScreen>();

            if (Time.timeScale == 0)
            {
                pauseScreenScript.ResumeGame();
            }
            else
            {
                pauseScreenScript.PauseGame();
            }
        }



        //if (currentLivesCount >= 3)
        //{
        //    playerIconImage.overrideSprite = spriteMaxHealth;
        //}
        //else if (currentLivesCount == 2)
        //{
        //    playerIconImage.overrideSprite = spriteMidHealth;
        //}
        //else if (currentLivesCount < 2)
        //{
        //    playerIconImage.overrideSprite = spriteEndHealth;
        //}

        if (gemExtraLifeCounter >= bonusLifeThreshold)
        {
            //we want to give ourselves an extra life 
            //update our display to reflect this
            currentLivesCount++;
            //livesText.text = "x: " + currentLivesCount;

            //we should then subtract from our hidden counter
            //we don't reset to 0 in case there is an overflow from picking up
            //a x3 gem or something
            gemExtraLifeCounter -= bonusLifeThreshold;
        }

        coins = PlayerPrefs.GetInt("Coins");
        coinText.text = "x" + coins;


        if (giveCoins)
        {
            PlayerPrefs.SetInt("Coins", PlayerPrefs.GetInt("Coins") + 1);
        }
    }


    //we need to give ourselves
    //the ability to respawn
    public void Respawn()
    {
        if (canRespawn)
        {
            currentLivesCount -= 1;
            //livesText.text = "x: " + currentLivesCount;

            if (currentLivesCount > 0)
            {
                canRespawn = false;
                StartCoroutine("RespawnCoroutine");
            }
            else
            {
                //this means we do not have enough lives
                //to respawn and we should game
                //over
                player.gameObject.SetActive(false);
                //livesText.text = "x: " + 0;
                //gameOverScreen.SetActive(true);
                //want to stop playing level music
                //play gameover instead
                //levelMusic.Stop();
                //gameOverMusic.Play();

            }
        }

    }


    //coroutines do things in a seperate
    //timeline from the game world
    //we have an IEnumerator return type
    //  gives us support for simple
    //  iteration over non generic collections
    //  we can read data in a collection
    //  but cannot modify it
    //we have a "yield return" statement
    //  which means pause happens and
    //  then resumes on the next frame
    public IEnumerator RespawnCoroutine()
    {

        //know that we are active respawn
        respawnActive = true;

        //want this to handle our respawning

        //specifically working with
        //the playercontroller
        //so if we want to impact the
        //entire player object
        //we need to say gameObject
        //so we are not just the script
        player.gameObject.SetActive(false);

        //right ater we become deactivated we want
        //our explosion to go off
        //we are using Instantiate, which essentially
        //makes a copy of an object for us and
        //allows us to place it anywehre we want in 
        //the world (this is done a majority of the time
        //with multiple game objects, we may use it again)
        //with Instantiate we need to give three arguments
        //  tell it what object we want to copy
        //  tell it what position to go to
        //  give it a rotation

        //we want to deactivate the KillPlane
        //so when we move the camera we don't kill the player
        //we could probably avoid doing this if we
        //reordered our Coroutine to when things happen
        killPlane.SetActive(false);


        //Instantiate(deathExplosion, player.transform.position, player.transform.rotation);

        //before we do movements
        //we have two options
        //  move the player then wait
        //  OR
        //  wait and then move and reactivate
        yield return new WaitForSeconds(respawnCountdown);

        //once we make it here, we should be respawned
        respawnActive = false;

        //want to move the player to the respawn
        //position
        player.transform.position = player.respawnPos;

        //beofre we become active
        //reset our health and allow
        //us to respawn again if needed
        currentHealth = maxHealth;
        canRespawn = true;
        //healthBar.SetCurrentHealth(maxHealth);


        //need to walk through the list one by one and tell 
        //everything to become active again at the right spot
        //foreach (ResetOnPlayerDeath obj in objectsResetting)
        //{
            //make object active and then move them to position
            //we need to make active first before we can
            //move them, because when they are not active we
            //cannot use their script methods
        //    obj.gameObject.SetActive(true);
        //    obj.ResetAfterDeath();
        //}

        //we want the to reset the gem count back to 0 if we die
        //gemAmount = 0;
        //gemText.text = "gems: " + gemAmount;


        //we can now reactivate ourselves
        //since we have moved to the correct positon
        player.gameObject.SetActive(true);


        //this will force the camera to move back to where
        //the player is
        //we do this in case we die while on a vertical platform
        //the camera would not change its Y position and it would be messy
        //camera should always be at -10f for starters in our game
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f); //(why cant i 
        //.lerp it doesnt work)

        //pauase the coroutine
        //to make sure everything is where it needs to be
        //the reactivate the killPlane
        //this should give us enough time to move the camera and get
        //back to the player without the killPlane killing them
        //we could increase this number if needed
        yield return new WaitForSeconds(1);
        killPlane.SetActive(true);

        //we want our counter to reset if we die too
        gemExtraLifeCounter = 0;
        //gemText.text = "gems: " + gemAmount;
    }

    

    public IEnumerator IFramesFlash()
    {

        for (int i = 0; i < 5; i++)
        {
            mySR.color = new Color(1f, 1f, 1f, 0.18f);
            yield return new WaitForSeconds(0.1f);
            mySR.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(0.1f);
        }
    }


    public void AddGems(int amtOfGems)
    {
        //gemAmount += amtOfGems;
        //gemText.text = "gems: " + gemAmount;

        //whenever we get want our hidden counter to increase too
        //gemExtraLifeCounter += gemAmount;
        //gemText.text = "gems: " + gemAmount;
        //gemSound.Play();
    }

    //want to know how much damage to
    //inflict on the player

    public void DamagePlayer(float damageAmount)
    {

        if (!invincibilityFrames)
        {
            currentHealth -= damageAmount;
            //healthBar.SetCurrentHealth(currentHealth / maxHealth);
            player.Knockback();
            //player.hurtSound.Play();
            StartCoroutine("IFramesFlash");
        }


    }

    //public int getGemAmount()
    //{
        //return gemAmount;
    //}

    public void AddLife(int amt)
    {
        currentLivesCount += amt;
        //livesText.text = "x: " + currentLivesCount;
        //gemSound.Play();
    }

    //we want a method to give player health
    public void GiveHealth(int healthToGive)
    {
        currentHealth += healthToGive;
        //want to make sure we do not give ourselves too much health
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        //we want to update the display on screen to show that we have new health
        //healthBar.SetCurrentHealth(currentHealth / maxHealth);
    }
}


