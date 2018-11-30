using UnityEngine;

[ExecuteInEditMode]
public class Editor_cleanup_act : MonoBehaviour
{
    [Header("Storage gameobjects")]
    public GameObject roadsStorage;
    public GameObject decorationsStorage;
    public GameObject buildingsStorage;
    public GameObject movableBuildingsStorage;
    public GameObject scoreStorage;
 
    private void Start()
    {
        roadsStorage = GameObject.Find("Roads");
        decorationsStorage = GameObject.Find("Decorations");
        buildingsStorage = GameObject.Find("Buildings");
        movableBuildingsStorage = GameObject.Find("Movable buildings");
        scoreStorage = GameObject.Find("ScoreStorage");
    }

    public void CleanEditor()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        for (int i = 0; i < allObjects.Length; i++)
        {
            if (allObjects[i].name.Contains("Road") || allObjects[i].name.Contains("Railroad") || allObjects[i].name.Contains("Square"))
                allObjects[i].transform.parent = roadsStorage.transform;
            else if (allObjects[i].name.Contains("Lamppost") || allObjects[i].name.Contains("Bench") || allObjects[i].name.Contains("Cables"))
                allObjects[i].transform.parent = decorationsStorage.transform;
            else if (allObjects[i].name.Contains("Building") || allObjects[i].name.Contains("Basement"))
                allObjects[i].transform.parent = buildingsStorage.transform;
            else if (allObjects[i].name.Contains("Human"))
                allObjects[i].transform.parent = scoreStorage.transform;
            else if (allObjects[i].name.Contains("Movable"))
                allObjects[i].transform.parent = movableBuildingsStorage.transform;
        }
    }

}