using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MobileDetection : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern bool IsMobile();

    private void Start()
    {
        if (PlatformCheck())
        {
            SceneManager.LoadScene("ARScene");
        }
        else
        {
            SceneManager.LoadScene("BrowserScene");
        }
    }

    public bool PlatformCheck()
    {
#if !UNITY_EDITOR
        return IsMobile();
#else
        return false;
#endif
    }
}
