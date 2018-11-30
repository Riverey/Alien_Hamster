using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenButton : MonoBehaviour {
    private GameObject screen;
    public GameObject target;

    public enum ButtonType { navigation, level }
    public MenuUI.Direction direction;
    private MenuUI menuUI;

    public ButtonType buttonType = ButtonType.navigation;
    public string scene;

    private void Start()
    {
        menuUI = FindObjectOfType<MenuUI>();
        screen = gameObject.transform.parent.gameObject;
    }

    public void OnMouseDown()
    {
        switch (buttonType)
        {
            case ButtonType.navigation: menuUI.MoveRequest(screen, target, direction); break;
            case ButtonType.level: SceneManager.LoadScene(scene); break;
        }
    }
}
