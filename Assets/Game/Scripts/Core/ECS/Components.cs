using System;
using UnityEngine;

namespace Game
{
    public struct PlayerTag { }

    public struct Position
    {
        public Vector2 Value;
    }

    public struct Rotation
    {
        public Quaternion Value;
    }

    public struct RotationSpeed
    {
        public float Value;
    }
    
    public struct MoveInput
    {
        public Vector2 Value;
    }

    public struct MoveSpeed
    {
        public float Value;
    }

    public struct MovementBounds
    {
        public Vector2 Size;
        public Vector2 PlayerScale;
    }
    
    public struct PlayerViewRef
    {
        public Transform Transform;
    }

    public struct Health
    {
        public int Current;
        public int Max;
    }

    public struct Invulnerability
    {
        public float TimeLeft;
        public float Duration;
    }
    
    public struct DamageRequest
    {
        public Entity Target;
        public float Amount;
    }
    
    public struct CoinPickupRequest
    {
        public Coin Coin;
    }

    public struct CrystalPickupRequest
    {
        public Crystal Crystal;
    }

    public struct DeadTag { }
    
    public struct DeathHandledTag { }
    
    public struct PlayingTag { }
    
    public struct EnemyTag { }

    public struct EnemyViewRef
    {
        public Transform Transform;
    }
    
    public struct EnemyHealth
    {
        public float Current;
        public float Max;
    }
    
    public struct EnemyDamageRequest
    {
        public Entity Target;
        public float Amount;
    }
    
    public struct EnemyDeadTag { }

    public struct EnemyDeathHandledTag { }
    
    public struct EnemyMeleeAttack
    {
        public int Damage;
        public float Cooldown;
        public float Timer;
    }

    public struct EnemyPlayerInRangeTag { }
    
    public struct EnemySuicideAttackTag { }

    public struct EnemyAttackRequestTag { }
    
    public struct EnemyAxeAttack
    {
        public float Cooldown;
        public float Timer;
    }

    public struct EnemyAxeSpawnRef
    {
        public Action Spawn;
    }
    
    public struct AxeTag { }

    public struct AxeViewRef
    {
        public Axe Axe;
    }

    public struct Direction
    {
        public Vector2 Value;
    }

    public struct Lifetime
    {
        public float TimeLeft;
    }
    
    public struct Damage
    {
        public float Value;
    }
    
    public struct AxeHitPlayerTag { }
    
    public struct EnemySpawnTimer
    {
        public float TimeLeft;
    }
    
    public struct EnemySpawnStageNextRequest { }
    
    public struct EnemySpawnRequest { }
    
    public struct EnemyDropRequest
    {
        public Vector2 Position;
    }
    
    public struct EnemyDespawnRequest
    {
        public EnemyBase Enemy;
    }
    
    public struct EnemyMonoRef
    {
        public EnemyBase Enemy;
    }
    
    public struct EnemyKilledRequest { }
    
    public struct AxeDespawnRequest
    {
        public Axe Axe;
    }
    
    public struct AxeDespawnRequestedTag { }
    
    public struct WeaponAddRequest
    {
        public Weapons Name;
        public Weapon Weapon;
    }

    public struct WeaponStatsSetRequest
    {
        public Weapons Name;
        public WeaponStats Stats;
    }
    
    public struct WeaponResetRequest { }
    
    public struct HudRefreshRequest { }
    
    public struct ProjectileTag { }

    public struct ProjectileViewRef
    {
        public Transform Transform;
        public ProjectileEcsLink Link;
        public Action Despawn;
    }

    public struct ProjectileHitEnemyTag { }

    public struct ProjectileHitEnemyTarget
    {
        public Entity Enemy;
    }

    public struct ProjectileDespawnRequest
    {
        public Entity Projectile;
    }

    public struct ProjectileDespawnRequestedTag { }
    
    public struct WeaponHitboxTag { }

    public struct WeaponHitboxViewRef
    {
        public Transform Transform;
    }

    public struct WeaponHitboxHitEnemyRequest
    {
        public Entity WeaponHitbox;
        public Entity Enemy;
    }
    
    public struct WeaponHitboxDamageSetRequest
    {
        public Entity WeaponHitbox;
        public float Damage;
    }
    
    public struct ProjectileWeaponTag { }

    public struct ProjectileWeaponAttack
    {
        public float Cooldown;
        public float Timer;
    }

    public struct ProjectileWeaponSpawnRef
    {
        public Action Spawn;
    }
    
    public struct ProjectileWeaponCooldownSetRequest
    {
        public Entity Weapon;
        public float Cooldown;
    }
    
    public struct OrbitWeaponTag { }

    public struct OrbitWeaponViewRef
    {
        public Transform Transform;
    }

    public struct OrbitWeaponRotation
    {
        public float Angle;
        public float DegreesPerSecond;
    }

    public struct OrbitWeaponRotationSetRequest
    {
        public Entity Weapon;
        public float DegreesPerSecond;
    }
    
    public struct PlayerShield
    {
        public bool IsActive;
        public int Current;
        public int Cooldown;
    }
    
    public struct PlayerShieldSetRequest
    {
        public bool IsActive;
        public int Cooldown;
    }
    
    public struct PlayerWeaponRuntimeTag { }
    
    public struct GameRuntimeTag { }

    public struct GameRuntimeCleanupRequest { }
    public struct GameRuntimeCleanupValidationRequest { }
}