using System;
using UnityEngine;
using UnboundLib;

namespace RanzDeck.MonoBehaviours {
    class Rampage : MonoBehaviour {
        private float healthToDamageRatio = 0.4f;
        private float massToBounceForceRatio = 1.25f;

        public void Start()
        {
            PlayerCollision playerCollision = base.GetComponentInParent<PlayerCollision>();
            playerCollision.collideWithPlayerAction = (Action<Vector2, Vector2, Player>)Delegate
                .Combine(playerCollision.collideWithPlayerAction, new Action<Vector2, Vector2, Player>(this.OnPlayerCollision));
            // TODO take away gun and block indicator
            // TODO this surely won't work in network
            // TODO disable gun and blocking by disabling respective input actions
            // gun.enabled = false;
            // block.enabled = false;
        }
        
        private void OnPlayerCollision(Vector2 collision, Vector2 normal, Player target)
        {
            Player player = base.GetComponentInParent<Player>();
            float healthDamage = player.data.health * this.healthToDamageRatio;
            Vector2 damage = normal.normalized * healthDamage;
            Vector2 bounceForce = normal.normalized * (float)player.data.playerVel.GetFieldValue("mass") * this.massToBounceForceRatio;
            player.data.healthHandler.CallTakeForce(bounceForce);
            target.data.healthHandler.CallTakeDamage(damage, collision, null, player);
        }

        private void OnDestroy()
        {
            PlayerCollision playerCollision = base.GetComponentInParent<PlayerCollision>();
            playerCollision.collideWithPlayerAction = (Action<Vector2, Vector2, Player>)Delegate
                .Remove(playerCollision.collideWithPlayerAction, new Action<Vector2, Vector2, Player>(this.OnPlayerCollision));
            // TODO disable gun and blocking by disabling respective input actions
            // gun.enabled = this.wasGunEnabled;
            // block.enabled = this.wasBlockEnabled;
        }
    }
}