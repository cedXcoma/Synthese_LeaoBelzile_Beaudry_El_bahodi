using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab1 = default;
    [SerializeField] private GameObject _enemyPrefab2 = default;
    [SerializeField] private GameObject _pommeOr = default;
    [SerializeField] private GameObject _pommeVerte = default;
    [SerializeField] private GameObject _container = default;

    private bool _stopSpawn = false;

    void Start()
    {
        StartSpawning();  //Déclenche les coroutine pour le spawn des ennemis et des améliorations
    }

    private void StartSpawning()
    {
        StartCoroutine(Spawn1Coroutine());
        StartCoroutine(Spawn2Coroutine());
        StartCoroutine(SpawnOrCoroutine());
        StartCoroutine(SpawnVertCoroutine());
    }

    // Coroutine pour l'apparition des PowerUps
    IEnumerator Spawn1Coroutine()
    {
        yield return new WaitForSeconds(3f);
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefab1, positionSpawn, Quaternion.identity);
            newEnemy.transform.parent = _container.transform;
            yield return new WaitForSeconds(3f);
  
        }
    }

    //Coroutine pour l'apparition des ennemis
    IEnumerator Spawn2Coroutine()
    {
        yield return new WaitForSeconds(2f); // Délai initial
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            GameObject newEnemy = Instantiate(_enemyPrefab2, positionSpawn, Quaternion.identity);
            newEnemy.transform.parent = _container.transform;
            yield return new WaitForSeconds(6f);
        }

    }


    // Coroutine pour l'apparition des PowerUps
    IEnumerator SpawnOrCoroutine()
    {
        yield return new WaitForSeconds(5f);
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            //Choisi au hasard un powerUp faisant partie du tableau et l'instancie           
            Instantiate(_pommeOr, positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
    }

    IEnumerator SpawnVertCoroutine()
    {
        yield return new WaitForSeconds(5f);
        while (!_stopSpawn)
        {
            Vector3 positionSpawn = new Vector3(Random.Range(-8f, 8f), 7f, 0f);
            //Choisi au hasard un powerUp faisant partie du tableau et l'instancie           
            Instantiate(_pommeVerte, positionSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(15f, 30f));
        }
    }
    // Méthodes publiques ========================================================

    // Arrête le spawn à la mort du joueur (fin de partie)
    public void mortJoueur()
    {
        _stopSpawn = true;
    }
}
