using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GestionUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txtScore = default;
    [SerializeField] private GameObject _pausePanel = default;

    [SerializeField] private float _vitesseEnnemi = 9.0f;
    [SerializeField] private float _augVitesseParNiveau = 2.0f;
    [SerializeField] private int _pointageAugmentation = 500;

    private int _score = 0;
    private bool _estChanger = false;
    private bool _pauseOn = false;

    private void Start()
    {
        _score = 0;
        _pauseOn = false;
        Time.timeScale = 1;
        UpdateScore();
    }

    private void Update()
    {

        // Permet la gestion du panneau de pause (marche/arrêt)
        if ((Input.GetKeyDown(KeyCode.Escape) && !_pauseOn))
        {
            _pausePanel.SetActive(true);
            Time.timeScale = 0;
            _pauseOn = true;
        }
        else if ((Input.GetKeyDown(KeyCode.Escape) && _pauseOn))
        {
            _pausePanel.SetActive(false);
            Time.timeScale = 1;
            _pauseOn = false;
        }

        if (_score % _pointageAugmentation == 0 && _score != 0 && _estChanger == false)
        {
            AugmentVitesseEnnemi();
            _estChanger = true;
        }
        else if (_score % _pointageAugmentation != 0)
        {
            _estChanger = false;
        }
    }

    // Méthode qui change le pointage sur le UI
    private void UpdateScore()
    {
        _txtScore.text = "Score : " + _score.ToString();
    }

    private void AugmentVitesseEnnemi()
    {
        _vitesseEnnemi += _augVitesseParNiveau;
    }

    // Méthodes publiques ==================================================

    public int getScore()
    {
        return _score;
    }
    // Méthode qui permet l'augmentation du score
    public void AjouterScore(int points)
    {
        _score += points;
        UpdateScore();
    }


    IEnumerator FinPartie()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(2);
    }

    // Méthode qui relance la partie après une pause
    public void ResumeGame()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1;
        _pauseOn = false;
    }

    public void ChargerDepart()
    {
        SceneManager.LoadScene(0);
    }

    public float getVitesseEnnemi()
    {
        return _vitesseEnnemi;
    }
}
