using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graine : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    [SerializeField] private string _nom = default;
    [SerializeField] private AudioClip _GraineSound = default;
    [SerializeField] private GameObject _miniExplosionPrefab = default;

    private GestionUI _uiManager;
    private float _vitesseProjectileEnnemi;

    private void Awake()
    {
        _uiManager = FindObjectOfType<GestionUI>();
    }
    void Update()
    {
        // Déplace le laser vers le haut
        DeplacementGraine();
    }

    private void DeplacementGraine()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -6f)
        {
            // Si le laser sort de l'écran il se détruit
            if (this.transform.parent == null)
            {
                Destroy(this.gameObject);
            }
            // Si le laser fait partie d'un conteneur il détruit le conteneur
            else
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }

   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && _nom != "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Degats();
            AudioSource.PlayClipAtPoint(_GraineSound, Camera.main.transform.position, 0.3f);
            Instantiate(_miniExplosionPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
