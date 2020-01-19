using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour, IEventHandler
{
	// Constante

	private readonly int BONUS_MULTIPLICATEUR_SCORE = 3;
	private readonly int WARNING_TIME = 10; // Temps avant de lancer l'alerte, peu de temps.
	private readonly float DELAI_CHANGE_COLOR_WARNING_TIME = 0.5f; // Délai entre 2 changement de couleur lors d'un warning time activé.


	// Attributs

	[SerializeField] AudioClip SongOfThisLevel;
    [SerializeField] GameObject SpawnPlayer1;
	[SerializeField] Material SkyBoxAttached;
	private GameObject currentPlayer1; // Joueur 1

	private bool LevelIsSkipped;

    [SerializeField] private int m_TimeLeft;
    private bool IsGameOver;

    private void Start()
    {
		if (RenderSettings.skybox != SkyBoxAttached && SkyBoxAttached != null)
		{
			RenderSettings.skybox = SkyBoxAttached;
		}

        StartCoroutine("TimerCountdown");
		EventManager.Instance.Raise(new NewLevelIsGeneratedEvent());

		MusicLoopsManager.Instance.PlayMusic(SongOfThisLevel);
    }

    // Coroutine for timer
    #region
    IEnumerator TimerCountdown()
    {
		bool CoroutineIsStart = false;

        while (m_TimeLeft > 0)
        {
            if (GameManager.Instance.IsPlaying)
            {
				if (!CoroutineIsStart  && m_TimeLeft <= WARNING_TIME)
				{
					CoroutineIsStart = true;
					StartCoroutine("NotALotTime");
				}

				LevelManager.Instance.m_CountdownTimer.text = m_TimeLeft.ToString();
				yield return new WaitForSeconds(1f);
				--m_TimeLeft;
			}
        }

		LevelManager.Instance.m_CountdownTimer.text = m_TimeLeft.ToString();
		EventManager.Instance.Raise(new GameOverEvent());
    }

	IEnumerator NotALotTime()
	{
		while (m_TimeLeft <= WARNING_TIME && m_TimeLeft > 0)
		{
			if (LevelManager.Instance.m_CountdownTimer.color == Color.white)
			{
				LevelManager.Instance.m_CountdownTimer.color = Color.red;
			} else
			{
				LevelManager.Instance.m_CountdownTimer.color = Color.white;
			}

			yield return new WaitForSeconds(DELAI_CHANGE_COLOR_WARNING_TIME);
		}

		LevelManager.Instance.m_CountdownTimer.color = Color.white;
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

	public void CreatePlayer1AtSpawnPosition(GameObject PlayerPrefab)
	{
		currentPlayer1 = Instantiate(PlayerPrefab, SpawnPlayer1.transform.position, Quaternion.identity, this.gameObject.transform);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire2") && !LevelIsSkipped && !IsGameOver && GameManager.Instance.IsPlaying)
		{
			LevelIsSkipped = true;
			EventManager.Instance.Raise(new LevelIsSkippedEvent());
		}
    }


	// Outils
	private void BallHasBeenDestroyed(BallHasBeenDestroyedEvent e)
	{
		if (Ball.GetAllBall().Count == 0)
		{
			EventManager.Instance.Raise(new ScoreItemEvent { eScore = m_TimeLeft * BONUS_MULTIPLICATEUR_SCORE });
			EventManager.Instance.Raise(new GoToNextLevelEvent());
		}
	}

    private void GameOver(GameOverEvent e)
    {
        StopCoroutine("TimerCountdown");
        IsGameOver = true;
    }
}
