using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderCountDown : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    [SerializeField] private float _time = 5f;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(_time);

        SceneManager.LoadScene(_sceneName);
    }
}
