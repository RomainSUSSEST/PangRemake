using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SDD.Events;

public class HudManager : Manager<HudManager>
{

    //[Header("HudManager")]
    #region Labels & Values
    [SerializeField] Text currentScore;
    [SerializeField] GameObject HUDPanel;
    #endregion

    private void Update()
    {
        ShowHideHUD();
    }

    #region Manager implementation
    protected override IEnumerator InitCoroutine()
	{
		yield break;
	}
	#endregion

	#region Callbacks to GameManager events
	protected override void GameStatisticsChanged(GameStatisticsChangedEvent e)
	{
        float m_Score = GameManager.Instance.Score;
        currentScore.text = m_Score.ToString();
    }
    #endregion

    #region Show & Hide HUD
    void ShowHideHUD()
    {
        if (GameManager.Instance.IsPlaying)
        {
            HUDPanel.SetActive(true);
        } else
        {
            HUDPanel.SetActive(false);
        }
    }
    #endregion

}