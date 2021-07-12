using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] confetti;

    private void Start()
    {
        GameController.Instance.OnLevelDone += SpawnComfetti;
    }

    private void SpawnComfetti()
    {
        foreach (ParticleSystem ps in confetti)
        {
            ps.Play();
        }
    }

    private void OnDisable()
    {
        GameController.Instance.OnLevelDone -= SpawnComfetti;
    }
}
