using System;
using UnityEngine;

namespace RanzDeck.MonoBehaviours {
    class TeleportToAttackerBlockEffect : MonoBehaviour {
        public void Start()
        {
            Block block = base.GetComponentInParent<Block>();
            block.BlockProjectileAction = (Action<GameObject, Vector3, Vector3>)Delegate
                .Combine(block.BlockProjectileAction, new Action<GameObject, Vector3, Vector3>(this.DoBlockedProjectile));
        }
        
        private void DoBlockedProjectile(GameObject projectile, Vector3 forward, Vector3 hitPos)
        {
            ProjectileHit component = projectile.GetComponent<ProjectileHit>();
            Player target = component.ownPlayer;
            this.Go(target);
        }

        /// <summary>
        /// Teleports a source player to a target player.
        /// </summary>
        /// <param name="target"></param>
        private void Go(Player target)
        {
            Vector3 sourcePosition = base.transform.position;
            Vector3 targetPosition = target.transform.position;
            Vector3 aimDirection = target.GetComponent<GeneralInput>().aimDirection;
            
            // this needs to be a coroutine with a delay
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            target.GetComponent<CircleCollider2D>().enabled = false;

            base.transform.root.transform.position = targetPosition + (aimDirection.normalized * -3.5f);
            
            this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            target.GetComponent<CircleCollider2D>().enabled = true;
        }

        private void OnDestroy()
        {
            Block block = base.GetComponentInParent<Block>();
            block.BlockProjectileAction = (Action<GameObject, Vector3, Vector3>)Delegate
                .Remove(block.BlockProjectileAction, new Action<GameObject, Vector3, Vector3>(this.DoBlockedProjectile));
        }
    }
}