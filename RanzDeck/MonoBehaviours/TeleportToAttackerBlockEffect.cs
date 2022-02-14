using System;
using UnityEngine;

namespace RanzDeck.MonoBehaviours {
    class TeleportToPlayerBlockEffect : MonoBehaviour {
        public void Start() {
            Block block = base.GetComponentInParent<Block>();
            block.BlockProjectileAction = (Action<GameObject, Vector3, Vector3>)Delegate.Combine(block.BlockProjectileAction, new Action<GameObject, Vector3, Vector3>(this.DoBlockedProjectile));
        }
        
        public void DoBlockedProjectile(GameObject projectile, Vector3 forward, Vector3 hitPos) {
            ProjectileHit component = projectile.GetComponent<ProjectileHit>();
            UnityEngine.Debug.Log($"[{RanzDeck.ModInitials}][BlockEffect] Hi {component.ownPlayer.name}.");
        }
    }
}