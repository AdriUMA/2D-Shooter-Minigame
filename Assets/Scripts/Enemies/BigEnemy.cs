using System.Collections;
using UnityEngine;

public class BigEnemy : Enemy
{
    [Header("Big Enemy")]
    [SerializeField] private AudioClip _roarSound;

    protected override IEnumerator Start()
    {
        AudioManager.Instance.PlayFX(_roarSound);
        yield return base.Start();
    }
}