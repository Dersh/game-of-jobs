using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void Awake()
    {
        SceneManager.LoadScene(_sceneName);
        GameObject[] all = FindObjectsOfType<GameObject>();
        for (int i = 0; i < all.Length; i++)
        {
            DontDestroyOnLoad(all[i]);
        }
    }
}