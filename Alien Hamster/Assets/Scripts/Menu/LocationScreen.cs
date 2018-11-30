using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationScreen : MonoBehaviour {
    [System.Serializable]
    public class testClass
    {
        public UnityEngine.SceneManagement.Scene levelScene;
        public bool isUnlocked;
        public GameObject levelButton;
        public List<GameObject> stars;
    }
    [Header("Location Screen")]
    [SerializeField]
    public List<testClass> buttonsList;
    private int itemNumber = 0;
    public GameObject locationPicture;

    public bool fakeScore = false;
    public bool wipeResults = false;
    public int baseIndex = 0;

    void Start () {
    foreach (testClass item in buttonsList)
        {
            itemNumber++;

            if (fakeScore) createSaveRecord(item.isUnlocked);

            PlayerPrefs.SetInt("Lvl_1isUnlckd", 1);
            PlayerPrefs.SetInt("Lvl_1raiting", 0);


            if (PlayerPrefs.GetInt("Lvl_" + (baseIndex + itemNumber) + "isUnlckd") == 1)
            {
                item.isUnlocked = true;
                item.levelButton.GetComponent<MeshRenderer>().material.SetInt("Vector1_43B687E", 1);
                int starCount = PlayerPrefs.GetInt("Lvl_" + (baseIndex + itemNumber) + "raiting");
                if (starCount > 0)
                {
                    Debug.Log("StarCount is " + starCount);
                    item.levelButton.GetComponent<MeshRenderer>().material.SetInt("Vector1_38C638C4", 1);
                    for (int i = 0; i < starCount; i++) //ask why so
                    {
                        item.stars[i].SetActive(true);
                    }
                }
                else
                    item.levelButton.GetComponent<MeshRenderer>().material.SetInt("Vector1_38C638C4", 0);
            }
            else
            {
                item.isUnlocked = false;
                item.levelButton.GetComponent<MeshRenderer>().material.SetInt("Vector1_43B687E", 0);
            }
            if (itemNumber == 1)
            {
                if (!item.isUnlocked)
                {
                    locationPicture.GetComponent<MeshRenderer>().material.SetInt("Vector1_43B687E", 0);
                    locationPicture.GetComponent<MeshRenderer>().material.SetInt("Vector1_38C638C4", 1);
                }
                else
                {
                    locationPicture.GetComponent<MeshRenderer>().material.SetInt("Vector1_43B687E", 1);
                    locationPicture.GetComponent<MeshRenderer>().material.SetInt("Vector1_38C638C4", 0);
                }
            }
        }
	}

    public void createSaveRecord(bool isUnlocked)
    {
        int lckd = 0;
        if (isUnlocked) lckd = 1;
        PlayerPrefs.SetInt("Lvl_" + (baseIndex + itemNumber) + "isUnlckd", lckd);

        if (wipeResults)
            PlayerPrefs.SetInt("Lvl_" + (baseIndex + itemNumber) + "raiting", 0);
        else
        {
            int randomScore = Random.Range(1, 3);
            PlayerPrefs.SetInt("Lvl_" + (baseIndex + itemNumber) + "raiting", randomScore);
        }
        //Debug.Log("Created a record for level " + itemNumber + "and set it to " + lckd + "and score set to " + randomScore);
    }
}
