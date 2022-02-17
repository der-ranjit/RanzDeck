using System;
using UnityEngine;
using UnboundLib;

namespace RanzDeck.MonoBehaviours
{
    class Rampage : RanzBehavior
    {
        private float healthToDamageRatio = 0.2f;
        private float massToBounceForceRatio = 1.4f;
        private float forceMultiplier = 200f;

        public void Start()
        {
            PlayerCollision playerCollision = base.GetComponentInParent<PlayerCollision>();
            playerCollision.collideWithPlayerAction = (Action<Vector2, Vector2, Player>)Delegate
                .Combine(playerCollision.collideWithPlayerAction, new Action<Vector2, Vector2, Player>(this.OnPlayerCollision));

            // TODO disable gun and blocking by disabling respective input actions
            // TODO take away gun and block indicators
            Player player = base.GetComponentInParent<Player>();
            player.data.block.enabled = false;
            player.data.weaponHandler.gun.enabled = false;
        }

        private void OnPlayerCollision(Vector2 collision, Vector2 normal, Player target)
        {
            Player player = base.GetComponentInParent<Player>();
            float healthDamage = player.data.health * this.healthToDamageRatio;
            Vector2 damage = normal.normalized * healthDamage;
            float force = (float)player.data.playerVel.GetFieldValue("mass") * this.massToBounceForceRatio * this.forceMultiplier;
            Vector2 bounceForce = normal.normalized * force;
            player.data.healthHandler.CallTakeForce(bounceForce);
            target.data.healthHandler.CallTakeDamage(damage, collision, null, player);
        }

        private void OnDestroy()
        {
            PlayerCollision playerCollision = base.GetComponentInParent<PlayerCollision>();
            playerCollision.collideWithPlayerAction = (Action<Vector2, Vector2, Player>)Delegate
                .Remove(playerCollision.collideWithPlayerAction, new Action<Vector2, Vector2, Player>(this.OnPlayerCollision));
            // TODO disable gun and blocking by disabling respective input actions
            Player player = base.GetComponentInParent<Player>();
            player.data.block.enabled = true;
            player.data.weaponHandler.gun.enabled = true;
        }
    }
}