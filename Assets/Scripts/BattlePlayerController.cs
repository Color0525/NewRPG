﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 味方戦闘時の行動
/// </summary>
public class BattlePlayerController : BattleStatusControllerBase
{
    [SerializeField] GameObject m_statusIconPrefab = default;
    //[SerializeField] ParticleSystem m_fireSwordParticle;

    void Awake()
    {
        //statusIconをセット
        GameObject statusIcon = Instantiate(m_statusIconPrefab, GameObject.FindWithTag("StatusPanel").transform);
        SetStatusIcon(statusIcon.GetComponent<StatusIconController>());
    }

    /// <summary>
    /// 行動開始(味方)
    /// </summary>
    public override void BeginAction()
    {
        base.BeginAction();
        BattleManager.Instance.StartCommandSelect(m_havesSkills, this);
    }

    /// <summary>
    /// 行動コマンド(Player)
    /// </summary>
    public void PlayerActionCommand(SkillDatabase skill)
    {
        BattleManager.Instance.EndCommandSelect();
        //N
        //m_CurrentSkill = skill;
        skill.Effect(this, FindObjectsOfType<BattleEnemyController>());
        UseSkill(skill);
        //UseSP(m_CurrentSkill.m_CostSP);
        //if (m_CurrentSkill.m_FireEffect)
        //{
        //    m_fireSwordParticle.gameObject.SetActive(true);
        //}
        //PlayStateAnimator(m_CurrentSkill);
    }

    //N
    // アニメイベント    
    //public override void Hit(BattleStatusControllerBase target = null)
    //{
    //    BattleStatusControllerBase thisTarget = FindObjectOfType<BattleEnemyController>();
    //    base.Hit(thisTarget);
    //    if (m_CurrentSkill.m_FireEffect)
    //    {
    //        m_fireSwordParticle.gameObject.SetActive(false);
    //    }
    //}

    //public override void End()
    //{
    //    if (m_CurrentSkill.m_FireEffect)
    //    {
    //        m_fireSwordParticle.gameObject.SetActive(false);
    //    }
    //    base.End();
    //}
}
