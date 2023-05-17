using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pomme_bleue : MonoBehaviour
{
    [SerializeField] public int _points = 50;
    [SerializeField] private GameObject _enemyLaserPrefab = default;
    [SerializeField] private GameObject _explosionPrefab = default;

    private GestionUI _uiManager;
    private float _fireRate;
    private float _canFire;

    private void Start()
    {
        _uiManager = FindObjectOfType<GestionUI>().GetComponent<GestionUI>();
        _canFire = Random.Range(0.5f, 1f);
    }

    void Update()
    {
        //D�place l'ennemi vers le bas et s'il sort de l'�cran le replace en
        //haut de la sc�ne � une position al�atoire en X
        DeplacementEnnemi();

        //M�thode qui g�re le tir de laser par les ennemis
        TirEnnemi();

    }

    private void TirEnnemi()
    {
        if (_uiManager.getScore() > 500)
        {
            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(1f, 3f);
                _canFire = Time.time + _fireRate;
                Instantiate(_enemyLaserPrefab, transform.position + new Vector3(0f, -0.9f, 0f), Quaternion.identity);
            }
        }
    }

    private void DeplacementEnnemi()
    {
        transform.Translate(Vector3.down * Time.deltaTime * (_uiManager.getVitessePommeBleue()/2.0f));
        
        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-8.17f, 8.17f);
            transform.position = new Vector3(randomX, 8f, 0f);
        }
    }

    // G�re les collisions entre les ennemis et les lasers/joueur
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Si la collision survient avec le joueur
        if (other.tag == "Player")
        {
            //R�cup�rer la classe Player afin d'acc�der aux m�thodes publiques
            Player player = other.transform.GetComponent<Player>();
            player.Degats();  // Appeler la m�thode d�gats du joueur
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject); // D�truire l'objet ennemi
        }
        // Si la collision se produit avec un laser
        else if (other.tag == "Laser")
        {
            // D�truit l'ennemi et le laser
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            // Appelle la m�thode dans la classe UIManger pour augmenter le pointage
            _uiManager.AjouterScore(_points);
        }
    }

}
