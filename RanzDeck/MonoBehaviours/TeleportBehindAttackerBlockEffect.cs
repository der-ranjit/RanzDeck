using System;
using System.Collections;
using RanzDeck.Utils;
using UnityEngine;

namespace RanzDeck.MonoBehaviours {
    class TeleportBehindAttackerBlockEffect : MonoBehaviour {
        public void Start()
        {
            Block block = base.GetComponentInParent<Block>();
            block.BlockProjectileAction = (Action<GameObject, Vector3, Vector3>)Delegate
                .Combine(block.BlockProjectileAction, new Action<GameObject, Vector3, Vector3>(this.DoBlockedProjectile));
        }
        
        private void DoBlockedProjectile(GameObject projectile, Vector3 forward, Vector3 hitPos)
        {
            Player target = projectile.GetComponent<ProjectileHit>().ownPlayer;
            this.Go(target);
        }

        /// <summary>
        /// Teleports a source player behind target player's aim direction.
        /// </summary>
        /// <param name="target"></param>
        private void Go(Player target)
        {
            base.StartCoroutine(this.DelayedTeleport(target));
        }

        private IEnumerator DelayedTeleport(Player target)
        {
            Vector3 targetPosition = target.transform.position;
            Vector3 aimDirection = target.GetComponent<CharacterData>().aimDirection;
            
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
            target.GetComponent<CircleCollider2D>().enabled = false;
            this.gameObject.GetComponent<PlayerCollision>().IgnoreWallForFrames(5);
            yield return base.StartCoroutine(WaitFor.Frames(1));

            // TODO maybe care for player scale
            float offsetDistance = 3.5f;
            base.transform.root.transform.position = targetPosition + (aimDirection.normalized * -1 * offsetDistance);

            yield return base.StartCoroutine(WaitFor.Frames(4));
            this.gameObject.GetComponent<CircleCollider2D>().enabled = true;
            target.GetComponent<CircleCollider2D>().enabled = true;
            yield break;
        }

        private void OnDestroy()
        {
            Block block = base.GetComponentInParent<Block>();
            block.BlockProjectileAction = (Action<GameObject, Vector3, Vector3>)Delegate
                .Remove(block.BlockProjectileAction, new Action<GameObject, Vector3, Vector3>(this.DoBlockedProjectile));
        }
    }
}