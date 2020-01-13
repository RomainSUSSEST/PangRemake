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
    [SerializeField] public RawImage m_WeaponIcon;
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
        float m_Score = GameManager.Instance.Score;
        currentScore.text = m_Score.ToString();
    }
    #endregion

    #region Callbacks to GameManager events
    protected override void GamePlay(GamePlayEvent e)
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

}