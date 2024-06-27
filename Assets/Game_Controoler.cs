using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Game_Controoler : MonoBehaviour
{



    [Serializable]
    public class ColorItem
    {
        public string Color_Label;
        public Color ColorValue;
    }


    public AudioSource Dorost;
    public AudioSource Eshtebah;


    public float initialTime = 1f;
    public float Start_delay = 3f;


    public List<ColorItem> Colors_List;
    public TextMeshProUGUI ColorName;

    public Image[] ColorButtons;

    private int CorrectColorIndex;
    private int Score;
    private float RemainingTime;
    private bool isGameActive;
    private int chosenColorIndex;

    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimerText;
    public GameObject WinScreen;
    public GameObject LoseScreen;

    
    void Start()
    {
        InitializeGame();
    }

    private void InitializeGame()
    {
        Score = 0;
        StartCoroutine(BeginGameAfterDelay(Start_delay));
    }

    private IEnumerator BeginGameAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SetupColorButtons();
        isGameActive = true;
        RemainingTime = initialTime;
    }

    private void SetupColorButtons()
    {
        List<int> chosenIndices = new List<int>();

        for (int i = 0; i < ColorButtons.Length; i++)
        {
            int randomIndex;
            do
            {
                randomIndex = UnityEngine.Random.Range(0, Colors_List.Count);
            } while (chosenIndices.Contains(randomIndex));

            chosenIndices.Add(randomIndex);

            ColorButtons[i].color = Colors_List[randomIndex].ColorValue;
            ColorButtons[i].GetComponent<Color_Butten>().ColorName = Colors_List[randomIndex].Color_Label;
        }

        CorrectColorIndex = UnityEngine.Random.Range(0, chosenIndices.Count);
        ColorName.text = Colors_List[chosenIndices[CorrectColorIndex]].Color_Label;
        chosenColorIndex = chosenIndices[CorrectColorIndex];
    }

    void Update()
    {
        if (isGameActive)
        {

            RemainingTime -= Time.deltaTime;
            TimerText.text = "Time : " + RemainingTime.ToString("N0");

            if (RemainingTime <= 0)
            {
                Eshtebah.Play();
                UpdateScore(-1);
                RestartRound();
            }
        }
    }

    public void CheckColorChoice(string selectedColor)
    {
        if (selectedColor == Colors_List[chosenColorIndex].Color_Label)
        {
            Dorost.Play();
            UpdateScore(1);
            RestartRound();
        }
        else
        {
            Eshtebah.Play();
            UpdateScore(-1);
            RestartRound();
        }
    }


  


    private void UpdateScore(int amount)
    {
        Score += amount;
        ScoreText.text = "Score : "+Score.ToString();

        if (Score >= 20)
        {
            WinScreen.SetActive(true);
            isGameActive = false;
            StopAllCoroutines();
        }
        else if (Score <= 0)
        {
            Score = 0;
            LoseScreen.SetActive(true);
            isGameActive = false;
            StopAllCoroutines();
        }
    }

    private void RestartRound()
    {
        if (isGameActive)
        {
            RemainingTime = initialTime;
            SetupColorButtons();
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } 



}
