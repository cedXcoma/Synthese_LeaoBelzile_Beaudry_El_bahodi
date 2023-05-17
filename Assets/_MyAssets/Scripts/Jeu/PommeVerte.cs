using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PommeVerte : MonoBehaviour
{
    [SerializeField] private float _speed = 4.0f;
    private GameObject[] aliveEnemies;
    [SerializeField] private GameObject _explosionPrefab = default;
    //[SerializeField] private AudioClip _powerUpSound = default;

   
    // D�place le powerUp vers le bas et le d�truit s'il n'est pas saisi
    void Update()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y <= -5.0f)
        {
            Destroy(this.gameObject);
        }
    }

    // G�re la collision et le d�clenchement de am�liorations quand le powerUp touche le joueur
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {    
            Destroy(this.gameObject);
            DestroyAllEnemies();
        }
    }


    void DestroyAllEnemies()                                                   
    {;
        aliveEnemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (var i = 0; i < aliveEnemies.Length; i++)
        {
            
            Destroy(aliveEnemies[i]);
            Instantiate(_explosionPrefab, aliveEnemies[i].transform.position,Quaternion.identity);          
        }
    }
}
