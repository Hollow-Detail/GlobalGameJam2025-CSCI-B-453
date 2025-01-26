using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSFX : MonoBehaviour
{
    private ParticleSystem _parentParticleSystem;

    private int _currentNumberOfParticles = 0;

    [SerializeField] private bool playBornSound, playDieSound;
    public SoundManager.Sounds BornSound;
    public SoundManager.Sounds DieSound;

    // Start is called before the first frame update
    void Start()
    {
        _parentParticleSystem = this.GetComponent<ParticleSystem>();
        if (_parentParticleSystem == null)
            Debug.LogError("Missing ParticleSystem!", this);
    }

    // Update is called once per frame
    void Update()
    {
        var amount = Mathf.Abs(_currentNumberOfParticles - _parentParticleSystem.particleCount);

        if (playBornSound && _parentParticleSystem.particleCount < _currentNumberOfParticles)
        {
            for (int i = 0; i < amount; i++)
            {
                SoundManager.Instance?.PlaySound(DieSound);
            }
        }

        if (playDieSound && _parentParticleSystem.particleCount > _currentNumberOfParticles)
        {
            for (int i = 0; i < amount; i++)
            {
                SoundManager.Instance?.PlaySound(BornSound);
            }
        }

        _currentNumberOfParticles = _parentParticleSystem.particleCount;
    }
}
