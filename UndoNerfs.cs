using Il2CppAssets.Scripts.Models;
using Il2CppAssets.Scripts.Models.Towers.Mods;
using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.Enums;
using BTD_Mod_Helper.Api.ModOptions;
using BTD_Mod_Helper.Extensions;
using Il2CppSystem.Collections.Generic;
using MelonLoader;
using UndoNerfs;
using Il2CppAssets.Scripts.Models.Towers.Projectiles.Behaviors;

[assembly: MelonInfo(typeof(UndoNerfs.UndoNerfs), ModHelperData.Name, ModHelperData.Version, ModHelperData.RepoOwner)]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace UndoNerfs;

public class UndoNerfs : BloonsTD6Mod
{
    private static readonly ModSettingBool TackShooterNerf = new(true)
    {
        displayName = "Undo Tack Shooter Nerf",
        icon = VanillaSprites.TackShooter005
    };

    private static readonly ModSettingBool DragonsBreathNerf = new(true)
    {
        displayName = "Undo Dragon's Breath Nerf",
        icon = VanillaSprites.Wizard030
    };

    private static readonly ModSettingBool CaltropsNerf = new(true)
    {
        displayName = "Undo Ninja's Caltrops Nerf",
        icon = VanillaSprites.NinjaMonkey002
    };

    private static readonly ModSettingBool TopSpikeFactoryNerf = new(true)
    {
        displayName = "Undo Top Path Spike Factory Nerf",
        icon = VanillaSprites.SpikeFactory500
    };

    private static readonly ModSettingBool PermaspikeNerf = new(true)
    {
        displayName = "Undo Permaspike Nerf",
        icon = VanillaSprites.SpikeFactory005
    };

    public override void OnNewGameModel(GameModel gameModel, List<ModModel> mods)
    {
        foreach (var towerModel in gameModel.towers)
        {
            if (TackShooterNerf == true)
            {
                if (towerModel.appliedUpgrades.Contains(UpgradeType.EvenFasterShooting))
                {
                    towerModel.GetWeapon().rate *= 0.8f;
                }
                if (towerModel.appliedUpgrades.Contains(UpgradeType.HotShots))
                {
                    towerModel.GetWeapon().rate *= 1.25f;
                }
            }

            if (DragonsBreathNerf == true && towerModel.appliedUpgrades.Contains(UpgradeType.DragonsBreath))
            {
                towerModel.GetWeapons()[3].rate *= 0.8f;
                towerModel.GetWeapons()[3].projectile.AddBehavior(new DamageModifierForTagModel("DamageModifierForTagModel_Projectile", "Ceramic", 1, 1, false, false));
            }

            if (CaltropsNerf == true && towerModel.appliedUpgrades.Contains(UpgradeType.Caltrops))
            {
                towerModel.GetWeapons()[1].projectile.RemoveBehavior<AgeModel>();
                towerModel.GetWeapons()[1].projectile.AddBehavior(new AgeModel("AgeModel_Projectile", 70, 0, true, null));
            }

            if (TopSpikeFactoryNerf == true && towerModel.appliedUpgrades.Contains(UpgradeType.SpikedBalls))
            {
                towerModel.GetWeapon().projectile.pierce = 14;
            }

            if (PermaspikeNerf == true && towerModel.appliedUpgrades.Contains(UpgradeType.PermaSpike))
            {
                towerModel.GetWeapon().projectile.pierce *= 2;
                towerModel.GetWeapon().projectile.GetDamageModel().damage = 10;
            }
        }
    }
}