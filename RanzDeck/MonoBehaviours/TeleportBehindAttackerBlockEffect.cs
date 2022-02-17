using System;
using UnityEngine;

namespace RanzDeck.MonoBehaviours
{
    class TeleportBehindAttackerBlockEffect : RanzBehavior
    {
        public void Start()
        {
            Block block = base.GetComponentInParent<Block>();
            block.BlockProjectileAction = (Action<GameObject, Vector3, Vector3>)Delegate
                .Combine(block.BlockProjectileAction, new Action<GameObject, Vector3, Vector3>(this.HandleBlockedProjectile));
        }

        private void HandleBlockedProjectile(GameObject projectile, Vector3 forward, Vector3 hitPos)
        {
            Player target = projectile.GetComponent<ProjectileHit>().ownPlayer;
            this.TeleportTo(target);
        }

        /// <summary>
        /// Teleports a source player behind target player's aim direction.
        /// </summary>
        /// <param name="target"></param>
        private void TeleportTo(Player target)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 aimDirection = target.GetComponent<CharacterData>().aimDirection;
            this.gameObject.GetComponent<PlayerCollision>().IgnoreWallForFrames(2);

            // TODO maybe care for player scale
            float offsetDistance = 3.5f;
            base.transform.root.transform.position = targetPosition + (aimDirection.normalized * -1 * offsetDistance);
        }

        private void OnDestroy()
        {
            Block block = base.GetComponentInParent<Block>();
            block.BlockProjectileAction = (Action<GameObject, Vector3, Vector3>)Delegate
                .Remove(block.BlockProjectileAction, new Action<GameObject, Vector3, Vector3>(this.HandleBlockedProjectile));
        }
    }
}