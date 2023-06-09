using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 25f;
    [SerializeField] private string _nom = default;
    [SerializeField] private GameObject _miniExplosionPrefab = default;

    private GestionUI _uiManager;
    private float _vitesseProjectileEnnemi;

    private void Awake()
    {
        _uiManager = FindObjectOfType<GestionUI>();
    }
    void Update()
    {
            // D�place le laser vers le haut
            DeplacementLaserJoueur();
    }

    private void DeplacementLaserJoueur()
    {
        transform.Translate(Vector3.up * Time.deltaTime * _speed);
        if (transform.position.y > 8f)
        {
            // Si le laser sort de l'�cran il se d�truit
            if (this.transform.parent == null)
            {
                Destroy(this.gameObject);
            }
            // Si le laser fait partie d'un conteneur il d�truit le conteneur
            else
            {
                Destroy(this.transform.parent.gameObject);
            }
        }
    }

   
}
