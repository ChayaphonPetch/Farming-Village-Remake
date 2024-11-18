using UnityEngine;

public class PupilFollow : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        eyeFollow();
    }

    void eyeFollow()
    {
        Vector3 mousePos = Input.mousePosition;

        Vector2 direction = new Vector2(mousePos.x - transform.position.x,mousePos.y - transform.position.y);
        transform.up = direction;
    }
}
