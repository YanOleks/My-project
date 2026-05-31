using UnityEngine;

public class DestroyOffScreen : MonoBehaviour
{
    public float offsetBelowCamera = 6f;

    void Update()
    {
        if (transform.position.y < Camera.main.transform.position.y - offsetBelowCamera)
        {
            Destroy(gameObject);
        }
    }
}