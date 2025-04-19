using System;
using UnityEngine;


public class KeyboardKey : MonoBehaviour
{
    public Action OnOn;
    public Action OnOff;
    
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private ParticleSystem _singParticle;

    private Color _startColor;
    private void Start()
    {
        _startColor = _sprite.color;
    }

    public void On(float velocity)
    {
        _sprite.color = Color.yellow;
        _animator.SetBool("Sings", true);
        _singParticle.Play();
        OnOn?.Invoke();
    }

    public void Off()
    {
        _sprite.color = _startColor;
        _animator.SetBool("Sings", false);
        _singParticle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        OnOff?.Invoke();
    }
}