using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;

public class HudManager : Manager<HudManager>
{

    //[Header("HudManager")]
    #region Labels & Values
    [SerializeField] Text currentScoreGUI;
    [SerializeField] public Text currentHighscoreGUI;
    [SerializeField] GameObject HUDPanel;
    [SerializeField] public RawImage m_WeaponIcon;
    [SerializeField] public Text currentPlayerName;
    public float m_CurrentHighscore;
    [SerializeField] private RawImage WeaponIcon;
    #endregion

    #region Manager implementation
    protected override IEnumerator InitCoroutine()
	{
		yield break;
	}
	#endregion

	#region Callbacks to GameManager events
	protected override void GameStatisticsChanged(GameStatisticsChangedEvent e)
	{
        if (m_CurrentHighscore < GameManager.Instance.Score)
            m_CurrentHighscore = GameManager.Instance.Score;
        currentScoreGUI.text = GameManager.Instance.Score.ToString(); // Changing current score
    }
    #endregion

    #region Callbacks to GameManager events
    protected override void GamePlay(GamePlayEvent e)
    {
        ShowHUD();
    }

    protected override void GameResume(GameResumeEvent e)
    {
        ShowHUD();
    }

    protected override void GameMenu(GameMenuEvent e)
    {
        HideHUD();
    }

    protected override void GamePause(GamePauseEvent e)
    {
        HideHUD();
    }
    #endregion

    #region Show & Hide HUD
    void ShowHUD()
    {
        HUDPanel.SetActive(true);
    }

    void HideHUD()
    {
        HUDPanel.SetActive(false);
    }
    #endregion

    #region Highscore & Cie
    [SerializeField] public List<Text> HighscoresGUI = new List<Text>();
    int count;

    public class Highscores
    {
        public List<HighscoreEntry> highscoreEntries;
    }

    [System.Serializable]
    public class HighscoreEntry
    {
        public float m_PlayerScore;
        public string m_PlayerName;
    }

    public void AddHighscoreEntry(float playerScore, string playerName)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { m_PlayerScore = playerScore, m_PlayerName = playerName };
        string jsonString = PlayerPrefs.GetString("HIGHSCORES");
        Highscores m_Highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (m_Highscores == null)
        {
            m_Highscores = new Highscores()
            {
                highscoreEntries = new List<HighscoreEntry>()
            };
        }
        m_Highscores.highscoreEntries.Add(highscoreEntry);

        string json = JsonUtility.ToJson(m_Highscores);
        PlayerPrefs.SetString("HIGHSCORES", json);
        PlayerPrefs.Save();
    }

    public void UpdateLeaderboard()
    {
        string jsonString = PlayerPrefs.GetString("HIGHSCORES");
        Highscores m_Highscores = JsonUtility.FromJson<Highscores>(jsonString);
        count = 0;

        if (m_Highscores == null)
        {
            // Default values of leaderboard
            AddHighscoreEntry(1000, "AAAAA");
            AddHighscoreEntry(900, "BBBBB");
            AddHighscoreEntry(800, "CCCCC");
            AddHighscoreEntry(700, "DDDDD");
            AddHighscoreEntry(600, "EEEEE");
            AddHighscoreEntry(500, "FFFFF");
            AddHighscoreEntry(400, "GGGGG");
            AddHighscoreEntry(300, "HHHHH");
            AddHighscoreEntry(200, "IIIII");
            AddHighscoreEntry(100, "JJJJJ");
            // Reload leaderboard
            jsonString = PlayerPrefs.GetString("HIGHSCORES");
            m_Highscores = JsonUtility.FromJson<Highscores>(jsonString);
        }

        for (int i = 0; i < m_Highscores.highscoreEntries.Count; i++)
        {
            for (int j = i + 1; j < m_Highscores.highscoreEntries.Count; j++)
            {
                if (m_Highscores.highscoreEntries[j].m_PlayerScore > m_Highscores.highscoreEntries[i].m_PlayerScore)
                {
                    HighscoreEntry m_HSEntry = m_Highscores.highscoreEntries[i];
                    m_Highscores.highscoreEntries[i] = m_Highscores.highscoreEntries[j];
                    m_Highscores.highscoreEntries[j] = m_HSEntry;
                }
            }
        }

        foreach (HighscoreEntry highscoreEntry in m_Highscores.highscoreEntries)
        {
            Debug.Log(count);
            if (count < HighscoresGUI.Count)
            {
                HighscoresGUI[count].text = "#" + (count + 1) + " " + highscoreEntry.m_PlayerName + " - " + highscoreEntry.m_PlayerScore;
            }
            count++;
        }

        string json = JsonUtility.ToJson(m_Highscores);
        PlayerPrefs.SetString("HIGHSCORES", json);
        PlayerPrefs.Save();
    }
    #endregion

    #region WeaponIcon
    public void SetWeaponIcon(Texture icon)
    {
        WeaponIcon.texture = icon;
    }
    #endregion
}