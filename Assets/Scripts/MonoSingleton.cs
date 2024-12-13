using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
        }
        else if (Instance != this)
        {
            Debug.LogWarning($"Multiple instances of {typeof(T)} found in the scene. Destroying this instance.");
            Destroy(gameObject);
        }
    }
}