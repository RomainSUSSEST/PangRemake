using SDD.Events;
using STUDENT_NAME;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour, IEventHandler
{
	// Attributs

	[SerializeField] private GameObject SpawnPlayer1;


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
	}

	public void UnsubscribeEvents()
	{
		EventManager.Instance.RemoveListener<BallHasBeenDestroyedEvent>(BallHasBeenDestroyed);
	}

	public void SetPlayer1Position(GameObject Player1)
	{
		Player1.transform.position = SpawnPlayer1.transform.position;
	}


	// Outils

	private void BallHasBeenDestroyed(BallHasBeenDestroyedEvent e)
	{
		if (Ball.GetAllBall().Count == 0)
		{
			EventManager.Instance.Raise(new GoToNextLevelEvent());
		}
	}
}
