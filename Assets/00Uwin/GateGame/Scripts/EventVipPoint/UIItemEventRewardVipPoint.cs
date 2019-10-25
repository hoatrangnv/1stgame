using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemEventRewardVipPoint : MonoBehaviour {

    public Image imgIcon;
    public Text txtPoint;
    public Text txtReward;
    public ButtonGray buttonReceive;
    public GameObject boxUnlockReceive;
    MVipPointDatabase vipPointData;
    private int level;
    public void Init(int level)
    {
        this.level = level;
        vipPointData = DatabaseServer.ListVipPointDatabasee[level];
        txtPoint.text = VKCommon.ConvertStringMoney(vipPointData.VipPoint);
        txtReward.text = VKCommon.ConvertStringMoney(vipPointData.RewardLevel);

        if (vipPointData.RewardLevel <= 0)
        {
            buttonReceive.gameObject.SetActive(false);
            boxUnlockReceive.gameObject.SetActive(true);
        }
        else
        {
            buttonReceive.gameObject.SetActive(true);
            boxUnlockReceive.gameObject.SetActive(false);
            InitBtnReceiveState();
        }
    }

    private void InitBtnReceiveState()
    {
        MAccountVipPoint accountVip = Database.Instance.AccountVipPoint();
        if (accountVip.LevelMax > 0 && level <= accountVip.LevelMax - 1)
        {
            string[] levelRewardList = accountVip.LevelReward.Split(',');
            for (int i = 1; i < levelRewardList.Length; i++)
            {
                if (level == int.Parse(levelRewardList[i]))
                {
                    buttonReceive.gameObject.SetActive(false);
                    boxUnlockReceive.gameObject.SetActive(true);
                    return;
                }
            }
            buttonReceive.IsActive = true;
        }
        else
        {
            buttonReceive.IsActive = false;
        }
       
    }

    public void OnBtnReceive()
    {
        MAccountVipPoint mAccountVipPoint = Database.Instance.AccountVipPoint();
        SendRequest.SendReceiveLevelVipPoint(level);
    }
}
