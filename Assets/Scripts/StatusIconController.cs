﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ステータスアイコンを操作
/// </summary>
public class StatusIconController : MonoBehaviour
{
    //[SerializeField] bool m_noMoveEffect = true;
    [SerializeField] protected Slider m_HPBar = default;
    [SerializeField] protected Slider m_guardBar = default;
    [SerializeField] protected Slider m_SPBar = default;
    [SerializeField] protected GameObject m_coolTimeBarPrefab = default;
    protected Slider m_coolTimeBar = default;
    protected TextMeshProUGUI m_coolTimeValue = default;
    //protected Transform m_coolTimeBarPanel = default;
    int m_maxCoolTimeBar = 200;

    protected float m_effectTime = 0.2f;

    public virtual void Awake()
    {
        //m_coolTimeBarPanel = GameObject.FindWithTag("EnemysCoolTimeBar").transform;
    }

    /// <summary>
    /// ステータスやクールタイムアイコンのセットアップ
    /// </summary>
    /// <param name="status"></param>
    /// <param name="coolTimePanel"></param>
    public virtual void SetupStatus(BattleStatusControllerBase status, GameObject coolTimePanel)
    {
        UpdateHPBar(status.MaxHP, status.CurrentHP);
        UpdateGuardBar(status.MaxGuard, status.CurrentGuard);
        UpdateSPBar(status.MaxSP, status.CurrentSP);

        //m_coolTimeBarPanel = coolTimePanel.transform.Find("Enemys");
        GameObject go = Instantiate(m_coolTimeBarPrefab, coolTimePanel.transform.Find("Enemys"));
        m_coolTimeBar = go.GetComponent<Slider>();
        m_coolTimeValue = go.GetComponentInChildren<TextMeshProUGUI>();//valueを参照
        Image[] children = go.GetComponentsInChildren<Image>();
        foreach (var child in children)
        {
            if (child.name == "Frame") child.color = Color.magenta;//枠の色を変える
            else if (child.name == "UnitImage") child.sprite = status.CoolTimeIcon;//そのユニットのイメージに
        }
        UpdateCoolTimeBar(status.CoolTime);
    }

    /// <summary>
    /// クールタイムバーを非表示に
    /// </summary>
    public void HideCoolTimeBar()
    {
        m_coolTimeBar.gameObject.SetActive(false);
    }

    /// <summary>
    /// HPBarを更新
    /// </summary>
    /// <param name="maxHP"></param>
    /// <param name="currentHP"></param>
    public virtual void UpdateHPBar(int maxHP, int currentHP)
    {
        //バーを現在値の比率までなめらかに変化させる
        m_HPBar.DOValue((float)currentHP / (float)maxHP, m_effectTime);
    }

    /// <summary>
    /// GuardBarを更新
    /// </summary>
    /// <param name="maxGuard"></param>
    /// <param name="currentGuard"></param>
    public virtual void UpdateGuardBar(int maxGuard, int currentGuard)
    {
        //バーを現在値の比率までなめらかに変化させる
        m_guardBar.DOValue((float)currentGuard / (float)maxGuard, m_effectTime);
    }

    /// <summary>
    /// SPBarを更新
    /// </summary>
    /// <param name="maxSP"></param>
    /// <param name="currentSP"></param>
    public virtual void UpdateSPBar(int maxSP, int currentSP)
    {
        //バーを現在値の比率までなめらかに変化させる
        m_SPBar.DOValue((float)currentSP / (float)maxSP, m_effectTime);
    }

    public void UpdateCoolTimeBar(float coolTime)
    {
        //バーを現在値の比率までなめらかに変化させる//クールタイムバーの最大値はm_maxCoolTimeBarにする
        m_coolTimeBar.DOValue(coolTime / m_maxCoolTimeBar, m_effectTime);

        //小さい順に

        //値を現在値までなめらかに変化させる
        DOTween.To(() => int.Parse(m_coolTimeValue.text), x => m_coolTimeValue.text = x.ToString(), (int)coolTime, m_effectTime);
    }


    //if (m_noMoveEffect)
    //{
    //m_HPBar.DOValue((float)currentHP / (float)maxHP, m_effectTime);
    //}
    //else
    //{
    //}

    //if (m_HPValue)
    //{
    //    if (m_noMoveEffect)
    //    {
    //        m_HPValue.text = currentHP.ToString();
    //    }
    //    else
    //    {

    //    }
    //}
}
