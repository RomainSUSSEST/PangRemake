

namespace STUDENT_NAME
{
	using System.Collections;
	using System.Collections.Generic;
	using UnityEngine;
	using System.Linq;
	using SDD.Events;

	public class LevelManager : Manager<LevelManager>
	{
		[Header("LevelsManager")]
		#region levels & current level management

		[SerializeField] GameObject[] m_LevelsPrefabs;
		private int CurrentLevelIndex;
		private GameObject CurrentLevel;

		[SerializeField] private GameObject PlayerPrefab;
		private GameObject currentPlayer1;
		

		#endregion


		#region Manager implementation
		protected override IEnumerator InitCoroutine()
		{
			yield break;
		}
		#endregion

		public override void SubscribeEvents()
		{
			base.SubscribeEvents();
			EventManager.Instance.AddListener<GoToNextLevelEvent>(GoToNextLevel);
			EventManager.Instance.AddListener<BallHasBeenDestroyedEvent>(BallHasBeenDestroyed);
		}

		public override void UnsubscribeEvents()
		{
			base.UnsubscribeEvents();
			EventManager.Instance.RemoveListener<GoToNextLevelEvent>(GoToNextLevel);
			EventManager.Instance.AddListener<BallHasBeenDestroyedEvent>(BallHasBeenDestroyed);
		}

		#region Callbacks to GameManager events
		protected override void GamePlay(GamePlayEvent e)
		{
			currentPlayer1 = Instantiate(PlayerPrefab);
			InstantiateLevel(0);
		}

		protected override void GameMenu(GameMenuEvent e)
		{
			Destroy(CurrentLevel);
			Destroy(currentPlayer1);
		}

		protected void GoToNextLevel(GoToNextLevelEvent e)
		{
			++CurrentLevelIndex;
			StartCoroutine(GoToNextLevelCoroutine());
		}

		protected void BallHasBeenDestroyed(BallHasBeenDestroyedEvent ball)
		{
			
			if (Ball.GetAllBall().Count == 0)
			{
				EventManager.Instance.Raise(new GoToNextLevelEvent());
			}
		}
		#endregion

		// Outils

		private IEnumerator GoToNextLevelCoroutine()
		{
			Destroy(CurrentLevel);
			while (CurrentLevel != null)
			{
				yield return null;
			}

			InstantiateLevel(CurrentLevelIndex);
		}

		private void InstantiateLevel(int levelIndex)
		{
			levelIndex = Mathf.Max(levelIndex, 0) % m_LevelsPrefabs.Length;
			CurrentLevel = Instantiate(m_LevelsPrefabs[levelIndex]);
		}
	}
}