using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PommeVerte : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    private GameObject[] aliveEnemies;
    [SerializeField] ParticleSystem verte = default;
    [SerializeField] private AudioClip _pomSound = default;
    [SerializeField] private AudioClip _PUSound = default;
    [SerializeField] private GameObject _explosionPrefab = default;
    //[SerializeField] private AudioClip _powerUpSound = default;

   
    // Déplace le powerUp vers le bas et le détruit s'il n'est pas saisi
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // Gère la collision et le déclenchement de améliorations quand le powerUp touche le joueur
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {    
            Destroy(this.gameObject);
            Instantiate(verte, transform.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(_PUSound, Camera.main.transform.position, 0.3f);
            DestroyAllEnemies();
        }
    }


    void DestroyAllEnemies()                                                   
    {;
        aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < aliveEnemies.Length; i++)
        {
            
            Destroy(aliveEnemies[i]);
            AudioSource.PlayClipAtPoint(_pomSound, Camera.main.transform.position, 0.3f);
            Instantiate(_explosionPrefab, aliveEnemies[i].transform.position,Quaternion.identity);          
        }
    }
}
