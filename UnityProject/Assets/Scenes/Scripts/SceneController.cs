using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void Awake()
    {
        SceneManager.LoadScene(_sceneName);
        for (int i = 0; i < transform.childCount; i++)
        {
            DontDestroyOnLoad(transform.GetChild(i).gameObject);
        }
    }
}