using System;
using UnityEngine;

public class Hittable : MonoBehaviour
{
    public Action OnWin;
    
    [SerializeField] private float _speed;
    [SerializeField] private ParticleSystem _onDeathParticles;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void Update()
    {
        transform.position -= new Vector3(0, _speed * Time.deltaTime, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Hitzone>() == false) return;
        
        OnWin?.Invoke();
        _onDeathParticles.transform.SetParent(transform.parent);
        _onDeathParticles.Play();
        Destroy(gameObject);
    }
}