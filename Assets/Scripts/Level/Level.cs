﻿using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour, IEventHandler
{
    // Attributs
    [SerializeField] GameObject SpawnPlayer1;
	private bool LevelIsSkipped;

    [SerializeField] int m_TimeLeft;
    private bool IsGameOver;

    void Start()
    {
        StartCoroutine("TimerCountdown");
    }

    // Coroutine for timer
    #region
    IEnumerator TimerCountdown()
    {
        yield return new WaitForSeconds(0.5f);

        while (m_TimeLeft > 0)
        {
            if (GameManager.Instance.IsPlaying)
            {
                yield return new WaitForSeconds(1f);
                m_TimeLeft--;
            }
        }
    }
    #endregion

    // Méthode

    private void OnDestroy()
	{
		UnsubscribeEvents();
	}

	private void Awake()
	{
		SubscribeEvents();
	}

	public void SubscribeEvents()
	{
		EventManager.Instance.AddListener<BallHasBeenDestroyedEvent>(BallHasBeenDestroyed);
        EventManager.Instance.AddListener<GameOverEvent>(GameOver);
	}

	public void UnsubscribeEvents()
	{
		EventManager.Instance.RemoveListener<BallHasBeenDestroyedEvent>(BallHasBeenDestroyed);
        EventManager.Instance.RemoveListener<GameOverEvent>(GameOver);
	}

	public void SetPlayer1Position(GameObject Player1)
	{
		Player1.transform.position = SpawnPlayer1.transform.position;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire2") && !LevelIsSkipped && !IsGameOver && GameManager.Instance.IsPlaying)
		{
			LevelIsSkipped = true;
			EventManager.Instance.Raise(new GoToNextLevelEvent());
		}

        LevelManager.Instance.m_CountdownTimer.text = m_TimeLeft.ToString();

        if (m_TimeLeft <= 0)
        {
            EventManager.Instance.Raise(new GameOverEvent());
        }
            
    }


	// Outils
	private void BallHasBeenDestroyed(BallHasBeenDestroyedEvent e)
	{
		if (Ball.GetAllBall().Count == 0 && !LevelIsSkipped)
		{
			EventManager.Instance.Raise(new GoToNextLevelEvent());
		}
	}

    private void GameOver(GameOverEvent e)
    {
        StopCoroutine("TimerCountdown");
        IsGameOver = true;
    }
}
