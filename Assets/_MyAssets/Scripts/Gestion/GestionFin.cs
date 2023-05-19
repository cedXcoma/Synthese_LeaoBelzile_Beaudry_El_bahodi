using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GestionFin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtGameOver = default;
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private GameObject _txtSaisie = default;

    private int _score;
    

    // Start is called before the first frame update
    void Start()
    {
        _score = PlayerPrefs.GetInt("Score");
        _txtScore.text = "Votre Score : " + _score.ToString() + " pts";
        
        GameOverSequence();
    }

    // Update is called once per frame
    void Update()
    {
        // Permet de redémarrer la partie ou se déplacer au menu de départ à la fin de la partie
        if (Input.GetKeyDown(KeyCode.R) && !_txtSaisie.activeSelf)
        {
            SceneManager.LoadScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && !_txtSaisie.activeSelf)
        {
            SceneManager.LoadScene(0);
        }
    }

    // Méthode qui affiche la fin de la partie et lance la coroutine d'animation
    private void GameOverSequence()
    {
        _txtGameOver.gameObject.SetActive(true);
        StartCoroutine(GameOverBlinkRoutine());
    }

    IEnumerator GameOverBlinkRoutine()
    {
        while (true)
        {
            _txtGameOver.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.7f);
            _txtGameOver.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.7f);
        }
    }
}
