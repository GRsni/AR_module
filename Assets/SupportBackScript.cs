using UnityEngine;

public class SupportBackScript : MonoBehaviour
{
    // Start is called before the first frame update
    private void FixedUpdate()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }
        }
    }
}
