//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using System;

//public class HighscoresScoring : MonoBehaviour
//{
//    [SerializeField] public List<Text> HighscoresGUI = new List<Text>();
//    int count;

//    public class Highscores
//    {
//        public List<HighscoreEntry> highscoreEntries;
//    }

//    [System.Serializable]
//    public class HighscoreEntry
//    {
//        public float m_PlayerScore;
//        public string m_PlayerName;
//    }

//    public void AddHighscoreEntry(float playerScore, string playerName)
//    {
//        HighscoreEntry highscoreEntry = new HighscoreEntry { m_PlayerScore = playerScore, m_PlayerName = playerName };

//        string jsonString = PlayerPrefs.GetString("HIGHSCORES");
//        Highscores m_Highscores = JsonUtility.FromJson<Highscores>(jsonString);

//        if (m_Highscores == null)
//        {
//            m_Highscores = new Highscores()
//            {
//                highscoreEntries = new List<HighscoreEntry>()
//            };
//        }

//        m_Highscores.highscoreEntries.Add(highscoreEntry);

//        string json = JsonUtility.ToJson(m_Highscores);
//        PlayerPrefs.SetString("HIGHSCORES", json);
//        PlayerPrefs.Save();
//    }

//    public void UpdateLeaderboard()
//    {
//        string jsonString = PlayerPrefs.GetString("HIGHSCORES");
//        Highscores m_Highscores = JsonUtility.FromJson<Highscores>(jsonString);

//        if (m_Highscores == null)
//        {
//            // Default values of leaderboard
//            AddHighscoreEntry(10000, "AAAAA");
//            AddHighscoreEntry(9000, "BBBBB");
//            AddHighscoreEntry(8000, "CCCCC");
//            AddHighscoreEntry(7000, "DDDDD");
//            AddHighscoreEntry(6000, "EEEEE");
//            AddHighscoreEntry(5000, "FFFFF");
//            AddHighscoreEntry(4000, "GGGGG");
//            AddHighscoreEntry(3000, "HHHHH");
//            AddHighscoreEntry(2000, "IIIII");
//            AddHighscoreEntry(1000, "JJJJJ");
//            // Reload leaderboard
//            jsonString = PlayerPrefs.GetString("HIGHSCORES");
//            m_Highscores = JsonUtility.FromJson<Highscores>(jsonString);
//        }

//        for (int i = 0; i < m_Highscores.highscoreEntries.Count; i++)
//        {
//            for (int j = i + 1; j < m_Highscores.highscoreEntries.Count; j++)
//            {
//                if (m_Highscores.highscoreEntries[j].m_PlayerScore > m_Highscores.highscoreEntries[i].m_PlayerScore)
//                {
//                    HighscoreEntry m_HSEntry = m_Highscores.highscoreEntries[i];
//                    m_Highscores.highscoreEntries[i] = m_Highscores.highscoreEntries[j];
//                    m_Highscores.highscoreEntries[j] = m_HSEntry;
//                }
//            }
//        }

//        foreach (HighscoreEntry highscoreEntry in m_Highscores.highscoreEntries)
//        {
//            if (count < HighscoresGUI.Count)
//            {
//                HighscoresGUI[count].text = "#" + (count + 1) + " " + highscoreEntry.m_PlayerName + " - " + highscoreEntry.m_PlayerScore;
//            }
//            count++;
//        }
//    }
//}