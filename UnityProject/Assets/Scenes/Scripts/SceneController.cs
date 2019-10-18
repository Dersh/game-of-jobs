using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
public class SceneController : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void Awake()
    {
        SceneManager.LoadScene(_sceneName);
        GameObject[] roots = FindObjectsOfType<GameObject>().Select(go => go.transform.root.gameObject).Distinct().ToArray();
        for (int i = 0; i < roots.Length; i++)
        {
            DontDestroyOnLoad(roots[i]);
        }
    }
}