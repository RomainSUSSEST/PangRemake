using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SDD.Events;

public class MenuManager : Manager<MenuManager>
{

	[Header("MenuManager")]

	#region Panels
	[Header("Panels")]
	[SerializeField] GameObject m_PanelMainMenu;
	[SerializeField] UnityEngine.UI.RawImage BlackScreen;
	[SerializeField] GameObject m_PanelInGameMenu;
	[SerializeField] GameObject m_PanelGameOver;
    [SerializeField] GameObject m_PanelHighscores;

	List<GameObject> m_AllPanels;
	#endregion

	#region Events' subscription
	public override void SubscribeEvents()
	{
		base.SubscribeEvents();
	}

	public override void UnsubscribeEvents()
	{
		base.UnsubscribeEvents();
	}
	#endregion

	#region Manager implementation
	protected override IEnumerator InitCoroutine()
	{
		yield break;
	}
	#endregion

	#region Monobehaviour lifecycle
	protected override void Awake()
	{
		base.Awake();
		RegisterPanels();
	}

	private void Update()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			EscapeButtonHasBeenClicked();
		}
	}
	#endregion

	#region Panel Methods
	void RegisterPanels()
	{
		m_AllPanels = new List<GameObject>();
		m_AllPanels.Add(m_PanelMainMenu);
		m_AllPanels.Add(m_PanelInGameMenu);
		m_AllPanels.Add(m_PanelGameOver);
        m_AllPanels.Add(m_PanelHighscores);
	}

	void OpenPanel(GameObject panel)
	{
		foreach (var item in m_AllPanels)
			if (item) item.SetActive(item == panel);
	}
	#endregion

	#region UI OnClick Events
	public void EscapeButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new EscapeButtonClickedEvent());
	}

	public void PlayButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new PlayButtonClickedEvent());
	}

	public void ResumeButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new ResumeButtonClickedEvent());
	}

	public void MainMenuButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new MainMenuButtonClickedEvent());
	}

	public void QuitButtonHasBeenClicked()
	{
		EventManager.Instance.Raise(new QuitButtonClickedEvent());
	}

    public void HighscoresButtonHasBeenClicked()
    {
        EventManager.Instance.Raise(new HighscoresButtonClickedEvent());
    }
	#endregion

	#region Callbacks to GameManager events
	protected override void GameMenu(GameMenuEvent e)
	{
		StartCoroutine("LaunchGameMenu");
	}

	private IEnumerator LaunchGameMenu()
	{
		BlackScreen.gameObject.SetActive(true);
		yield return new WaitForSeconds(1);
		OpenPanel(m_PanelMainMenu);
		float decreaseValueAlpha = 0.05f;

		while (BlackScreen.color.a > 0)
		{
			Debug.Log(BlackScreen.color.a);
			BlackScreen.color = new Color(BlackScreen.color.r, BlackScreen.color.g, BlackScreen.color.b, BlackScreen.color.a - decreaseValueAlpha);
			yield return new WaitForSeconds(0.05f);
		}

		BlackScreen.gameObject.SetActive(false);
	}

	protected override void GamePlay(GamePlayEvent e)
	{
		OpenPanel(null);
	}

	protected override void GamePause(GamePauseEvent e)
	{
		OpenPanel(m_PanelInGameMenu);
	}

	protected override void GameResume(GameResumeEvent e)
	{
		OpenPanel(null);
	}

	protected override void GameOver(GameOverEvent e)
	{
		OpenPanel(m_PanelGameOver);
	}

    protected override void GameHighscores(GameHighscoresEvent e)
    {
        OpenPanel(m_PanelHighscores);
    }
	#endregion
}
