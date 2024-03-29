﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using SDD.Events;
using System.Linq;

public enum GameState { gameMenu, gamePlay, gameNextLevel, gamePause, gameOver, gameVictory }

public class GameManager : Manager<GameManager>
{
	#region Game State
	private GameState m_GameState;
	private bool LevelIsSkiped;
	public bool IsPlaying { get { return m_GameState == GameState.gamePlay; } }
	#endregion

	//LIVES
	#region Lives
	[Header("GameManager")]
	[SerializeField]
	private int m_NStartLives;

	private int m_NLives;
	public int NLives { get { return m_NLives; } }
	void DecrementNLives(int decrement)
	{
		SetNLives(m_NLives - decrement);
	}

	void SetNLives(int nLives)
	{
		m_NLives = nLives;
		EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eBestScore = BestScore, eScore = Score, eNLives = m_NLives});
	}
    #endregion


    #region Score
    private float m_Score;
	public float Score
	{
		get { return m_Score; }
		set
		{
			m_Score = value;
			BestScore = Mathf.Max(BestScore, value);
		}
	}

	public float BestScore
	{
		get { return PlayerPrefs.GetFloat("BEST_SCORE", 0); }
		set { PlayerPrefs.SetFloat("BEST_SCORE", value); }
	}

	void IncrementScore(float increment)
	{
		SetScore(m_Score + increment);
	}

	void SetScore(float score, bool raiseEvent = true)
	{
		Score = score;
		
		if (raiseEvent)
		{
			EventManager.Instance.Raise(new GameStatisticsChangedEvent() { eBestScore = BestScore, eScore = Score, eNLives = m_NLives });
		}	
	}
	#endregion

	#region Time
	void SetTimeScale(float newTimeScale)
	{
		Time.timeScale = newTimeScale;
	}
	#endregion


	#region Events' subscription
	public override void SubscribeEvents()
	{
		base.SubscribeEvents();
			
		//MainMenuManager
		EventManager.Instance.AddListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
		EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
		EventManager.Instance.AddListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
		EventManager.Instance.AddListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
		EventManager.Instance.AddListener<QuitButtonClickedEvent>(QuitButtonClicked);
        EventManager.Instance.AddListener<HighscoresButtonClickedEvent>(HighscoresButtonClicked);

		//Score Item
		EventManager.Instance.AddListener<ScoreItemEvent>(ScoreHasBeenGained);

		// Level
		EventManager.Instance.AddListener<LevelIsSkippedEvent>(LevelSkipped);
		EventManager.Instance.AddListener<NewLevelIsGeneratedEvent>(LevelUnSkipped);
	}

	public override void UnsubscribeEvents()
	{
		base.UnsubscribeEvents();

		//MainMenuManager
		EventManager.Instance.RemoveListener<MainMenuButtonClickedEvent>(MainMenuButtonClicked);
		EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
		EventManager.Instance.RemoveListener<ResumeButtonClickedEvent>(ResumeButtonClicked);
		EventManager.Instance.RemoveListener<EscapeButtonClickedEvent>(EscapeButtonClicked);
		EventManager.Instance.RemoveListener<QuitButtonClickedEvent>(QuitButtonClicked);
        EventManager.Instance.RemoveListener<HighscoresButtonClickedEvent>(HighscoresButtonClicked);

		//Score Item
		EventManager.Instance.RemoveListener<ScoreItemEvent>(ScoreHasBeenGained);

		// Level
		EventManager.Instance.RemoveListener<LevelIsSkippedEvent>(LevelSkipped);
		EventManager.Instance.RemoveListener<NewLevelIsGeneratedEvent>(LevelUnSkipped);
	}
	#endregion

	#region Manager implementation
	protected override IEnumerator InitCoroutine()
	{
		Menu();
		InitNewGame(); // Essentiellement pour que les statistiques du jeu soient mise à jour en HUD
		yield break;
	}
	#endregion

	#region Game flow & Gameplay
	//Game initialization
	void InitNewGame(bool raiseStatsEvent = true)
	{
		SetScore(0);
	}
	#endregion

	#region Callbacks to events issued by Score items
	private void ScoreHasBeenGained(ScoreItemEvent e)
	{
		if (IsPlaying && !LevelIsSkiped)
			IncrementScore(e.eScore);
	}
	#endregion

	#region Callbacks to Events issued by MenuManager
	private void MainMenuButtonClicked(MainMenuButtonClickedEvent e)
	{
		Menu();
	}

	private void PlayButtonClicked(PlayButtonClickedEvent e)
	{
		Play();
	}

	private void ResumeButtonClicked(ResumeButtonClickedEvent e)
	{
		Resume();
	}

	private void EscapeButtonClicked(EscapeButtonClickedEvent e)
	{
		if (IsPlaying) Pause();
	}

    private void HighscoresButtonClicked(HighscoresButtonClickedEvent e)
    {
        Highscores();
    }

	private void QuitButtonClicked(QuitButtonClickedEvent e)
	{
		Application.Quit();
	}
    #endregion

    #region Callbacks to Events issued by Level
	private void LevelSkipped(LevelIsSkippedEvent e)
	{
		LevelIsSkiped = true;
	}

	private void LevelUnSkipped(NewLevelIsGeneratedEvent e)
	{
		LevelIsSkiped = false;
	}
    #endregion

    #region GameState methods
    private void Menu()
	{
		SetTimeScale(1);
		m_GameState = GameState.gameMenu;
		if(MusicLoopsManager.Instance)MusicLoopsManager.Instance.PlayMenuMusic();
		EventManager.Instance.Raise(new GameMenuEvent());
	}

	private void Play()
	{
		InitNewGame();
		SetTimeScale(1);
		m_GameState = GameState.gamePlay;

		EventManager.Instance.Raise(new GamePlayEvent());
	}

	private void Pause()
	{
		if (!IsPlaying) return;

		SetTimeScale(0);
		m_GameState = GameState.gamePause;
		EventManager.Instance.Raise(new GamePauseEvent());
	}

	private void Resume()
	{
		if (IsPlaying) return;

		SetTimeScale(1);
		m_GameState = GameState.gamePlay;
		EventManager.Instance.Raise(new GameResumeEvent());
	}

	private void Over()
	{
		SetTimeScale(0);
		m_GameState = GameState.gameOver;
		EventManager.Instance.Raise(new GameOverEvent());
		if(SfxManager.Instance) SfxManager.Instance.PlaySfx2D(Constants.GAMEOVER_SFX);
	}

    private void Highscores()
    {
        SetTimeScale(0);
        m_GameState = GameState.gameMenu;
		HudManager.Instance.UpdateLeaderboard();
		EventManager.Instance.Raise(new GameHighscoresEvent());
    }
	#endregion
}

