﻿using UnityEngine;
using System.Collections;
using com.tksr.statemachine.defines;

namespace com.tksr.statemachine
{
    public class StopStatusEffect : StatusEffect
    {
        Stats myStats;

        void OnEnable()
        {
            myStats = GetComponentInParent<Stats>();
            if (myStats)
                this.AddObserver(OnCounterWillChange, Stats.WillChangeNotification(EnumStatTypes.CTR), myStats);
            this.AddObserver(OnAutomaticHitCheck, HitRate.AutomaticHitCheckNotification);
        }

        void OnDisable()
        {
            this.RemoveObserver(OnCounterWillChange, Stats.WillChangeNotification(EnumStatTypes.CTR), myStats);
            this.RemoveObserver(OnAutomaticHitCheck, HitRate.AutomaticHitCheckNotification);
        }

        void OnCounterWillChange(object sender, object args)
        {
            ValueChangeException exc = args as ValueChangeException;
            exc.FlipToggle();
        }

        void OnAutomaticHitCheck(object sender, object args)
        {
            Unit owner = GetComponentInParent<Unit>();
            MatchException exc = args as MatchException;
            if (owner == exc.target)
                exc.FlipToggle();
        }
    }
}