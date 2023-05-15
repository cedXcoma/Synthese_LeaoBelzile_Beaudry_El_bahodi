using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _enemyPrefab1 = default;
    [SerializeField] private GameObject _enemyPrefab2 = default;
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

    // Méthodes publiques ========================================================

    // Arrête le spawn à la mort du joueur (fin de partie)
    public void mortJoueur()
    {
        _stopSpawn = true;
    }
}
