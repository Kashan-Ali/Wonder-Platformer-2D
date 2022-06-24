using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;
    [SerializeField] TreasureChase _treasureChase;
    public int score;

    [SerializeField] Text _scoreTxt;
    [SerializeField] Image HUD;
    [SerializeField] Image PauseUI;
    [SerializeField] Image gameOverUI;
    [SerializeField] Image levelCompleteUI;

    private void OnEnable()
    {
        Time.timeScale = 1;
        _playerController = FindObjectOfType<PlayerController>();
        _treasureChase = FindObjectOfType<TreasureChase>();

        levelCompleteUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        PauseUI.gameObject.SetActive(false);
        HUD.gameObject.SetActive(true);
    }

    private void Update()
    {
        SetScore();

        if (_playerController.lifeState == PlayerController.Life.Death)
        {
            HUD.gameObject.SetActive(false);
            gameOverUI.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else if (_treasureChase.isLevelComplete)
        {
            Invoke("LevelComplete", 0.22f);
        }
    }

    void LevelComplete()
    {
        HUD.gameObject.SetActive(false);
        levelCompleteUI.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void SetScore()
    {
        _scoreTxt.text = score.ToString();
    }

    public void GamePause()
    {
        PauseUI.gameObject.SetActive(true);
        HUD.gameObject.SetActive(false);
        Time.timeScale = 0;
    }

    public void GameResume()
    {
        PauseUI.gameObject.SetActive(false);
        HUD.gameObject.SetActive(true);
        Time.timeScale = 1;
    }
}
