using System.Collections;
using UnityEngine;

public class MenuUI : MonoBehaviour {
    public Transform activeScreen;
    public Transform rightScreen;
    public Transform leftScreen;

    public bool isMoving = false;

    public float moveSpeed = 0.0f;

    private Vector3 targetPosition = Vector3.zero;

    public enum Direction { Left, Right };

    public void MoveRequest(GameObject movable, GameObject movable2, Direction dir)
    {
        if (!isMoving) StartCoroutine(MoveUI(movable, movable2, dir));
    }

    private IEnumerator MoveUI(GameObject movable, GameObject movable2, Direction dir)
    {
        isMoving = true;
        float rate = 1.0f / moveSpeed;
        movable2.SetActive(true);
        switch (dir)
        {
            case Direction.Left: targetPosition = rightScreen.position; Debug.Log("Started Coroutine, direction left"); break;
            case Direction.Right: targetPosition = leftScreen.position; Debug.Log("Started Coroutine, direction right"); break;
        }
        float i = 0.0f;
        while (i < 1.0f)
        {
            i += Time.unscaledDeltaTime * rate;
            movable.transform.position = Vector3.Lerp(movable.transform.position, targetPosition, i);
            movable2.transform.position = Vector3.Lerp(movable2.transform.position, activeScreen.position, i);
            yield return null;
        }
        movable.SetActive(false);
        isMoving = false;
    }
}
