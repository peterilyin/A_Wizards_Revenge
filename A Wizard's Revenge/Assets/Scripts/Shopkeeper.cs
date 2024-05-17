using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using TMPro;
using UnityEngine.UI;

public class Shopkeeper : MonoBehaviour
{
    public GameObject shopCanvas;
    public GameObject distanceUpgradeButton;
    public GameObject amountUpgradeButton;
    public TextMeshProUGUI distancePriceText;
    public TextMeshProUGUI amountPriceText;
    public GameObject distanceBar1;
    public GameObject distanceBar2;
    public GameObject distanceBar3;
    public GameObject amountBar1;
    public GameObject amountBar2;
    public GameObject amountBar3;

    public bool dU;
    public bool aU;

    public bool resetUpgrades;

    void Start()
    {
        //shopCanvas.SetActive(false);
    }

    
    void Update()
    {
        if (PlayerPrefs.GetInt("DistanceUpgrade") == 1)
        {
            distancePriceText.text = "x100";
            distanceBar1.SetActive(true);
            distanceBar2.SetActive(false);
            distanceBar3.SetActive(false);
        } else if (PlayerPrefs.GetInt("DistanceUpgrade") == 2)
        {
            distancePriceText.text = "x150";
            distanceBar1.SetActive(true);
            distanceBar2.SetActive(true);
            distanceBar3.SetActive(false);
        } else if (PlayerPrefs.GetInt("DistanceUpgrade") == 3)
        {
            distancePriceText.text = "x200";
            distanceBar1.SetActive(true);
            distanceBar2.SetActive(true);
            distanceBar3.SetActive(true);
        } else
        {
            distancePriceText.text = "x50";
            distanceBar1.SetActive(false);
            distanceBar2.SetActive(false);
            distanceBar3.SetActive(false);
        }

        if (dU)
        {
            dU = false;
        }



        if (PlayerPrefs.GetInt("AmountUpgrade") == 1)
        {
            amountPriceText.text = "x100";
            amountBar1.SetActive(true);
            amountBar2.SetActive(false);
            amountBar3.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("AmountUpgrade") == 2)
        {
            amountPriceText.text = "x150";
            amountBar1.SetActive(true);
            amountBar2.SetActive(true);
            amountBar3.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("AmountUpgrade") == 3)
        {
            amountPriceText.text = "x200";
            amountBar1.SetActive(true);
            amountBar2.SetActive(true);
            amountBar3.SetActive(true);
        }
        else
        {
            amountPriceText.text = "x50";
            amountBar1.SetActive(false);
            amountBar2.SetActive(false);
            amountBar3.SetActive(false);
        }

        if (aU)
        {
            aU = false;
        }
        
        if (resetUpgrades)
        {
            PlayerPrefs.SetInt("DistanceUpgrade", 0);
            PlayerPrefs.SetInt("AmountUpgrade", 0);
        }
    }


    public void UpgradeDistance()
    {
        if (PlayerPrefs.GetInt("DistanceUpgrade") < 3)
        {
            dU = true;

            Debug.Log((50 + (PlayerPrefs.GetInt("DistanceUpgrade") * 50)));
            Debug.Log(PlayerPrefs.GetInt("Coins"));

            if (PlayerPrefs.GetInt("Coins") >= (50 + (PlayerPrefs.GetInt("DistanceUpgrade") * 50)))
            {
                int newCoinAmount = PlayerPrefs.GetInt("Coins");
                newCoinAmount -= (50 + (PlayerPrefs.GetInt("DistanceUpgrade") * 50));
                PlayerPrefs.SetInt("Coins", newCoinAmount);

                int currDU = PlayerPrefs.GetInt("DistanceUpgrade");
                currDU += 1;
                PlayerPrefs.SetInt("DistanceUpgrade", currDU);
            }
            else
            {
                Debug.Log("you are broke 1");
            }
            


            
            
        }
    }

    public void UpgradeAmount()
    {
        if (PlayerPrefs.GetInt("AmountUpgrade") < 3)
        {
            aU = true;
            Debug.Log((50 + (PlayerPrefs.GetInt("AmountUpgrade") * 50)));


            if (PlayerPrefs.GetInt("Coins") >= (50 + (PlayerPrefs.GetInt("AmountUpgrade") * 50)))
            {
                int newCoinAmount = PlayerPrefs.GetInt("Coins");
                newCoinAmount -= (50 + (PlayerPrefs.GetInt("AmountUpgrade") * 50));
                PlayerPrefs.SetInt("Coins", newCoinAmount);

                int currAU = PlayerPrefs.GetInt("AmountUpgrade");
                currAU += 1;
                PlayerPrefs.SetInt("AmountUpgrade", currAU);
            }
            else
            {
                Debug.Log("you are broke 2");
            }
            

            
            
        }
    }

}
