﻿using Gameplay.GameControllers.Entities;
using UnityEngine;

namespace BlasClient.PvP
{
    public abstract class PlayerAttack
    {
        public abstract DamageArea.DamageType DamageType { get; }
        public abstract DamageArea.DamageElement DamageElement { get; }
        public abstract int BaseDamage { get; }
        public abstract int Force { get; }
        public abstract string SoundId { get; }
    }

    public class NormalAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 10;

        public override int Force => 1;

        public override string SoundId => "PenitentSimpleEnemyHit";
    }

    public class ComboNormalAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Heavy;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 20;

        public override int Force => 0;

        public override string SoundId => "PenitentHeavyEnemyHit";
    }

    public class ComboUpAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Heavy;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 40;

        public override int Force => 1;

        public override string SoundId => "PenitentHeavyEnemyHit";
    }

    public class ComboDownAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Heavy;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 40;

        public override int Force => 0;

        public override string SoundId => "PenitentHeavyEnemyHit";
    }

    public class ChargedAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Heavy;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 40;

        public override int Force => 2;

        public override string SoundId => "PenitentHeavyEnemyHit";
    }

    public class LungeAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Heavy;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 30;

        public override int Force => 1;

        public override string SoundId => "PenitentLungeAttackHit";
    }

    public class VerticalAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Heavy;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 20;

        public override int Force => 1;

        public override string SoundId => null;
    }

    public class RangedAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 20;

        public override int Force => 1;

        public override string SoundId => null;
    }

    // Prayers

    public class DeblaAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Magic;

        public override int BaseDamage => 20;

        public override int Force => 1;

        public override string SoundId => "PenitentMagicDamage";
    }

    public class VerdialesAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Magic;

        public override int BaseDamage => 60;

        public override int Force => 1;

        public override string SoundId => null;
    }

    public class TarantoAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Lightning;

        public override int BaseDamage => 60;

        public override int Force => 1;

        public override string SoundId => null;
    }

    public class TiranaAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Lightning;

        public override int BaseDamage => 60;

        public override int Force => 1;

        public override string SoundId => null;
    }

    public class PoisonMistAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Toxic;

        public override int BaseDamage => 60;

        public override int Force => 1;

        public override string SoundId => null;
    }

    public class ShieldAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 60;

        public override int Force => 1;

        public override string SoundId => null;
    }

    public class MiriamAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Normal;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 60;

        public override int Force => 1;

        public override string SoundId => null;
    }

    public class AubadeAttack : PlayerAttack
    {
        public override DamageArea.DamageType DamageType => DamageArea.DamageType.Heavy;

        public override DamageArea.DamageElement DamageElement => DamageArea.DamageElement.Normal;

        public override int BaseDamage => 60;

        public override int Force => 2;

        public override string SoundId => null;
    }
}
