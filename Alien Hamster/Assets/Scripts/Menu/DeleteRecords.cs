using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteRecords : MonoBehaviour {

 

    private void OnMouseDown()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(0);
    }
}
