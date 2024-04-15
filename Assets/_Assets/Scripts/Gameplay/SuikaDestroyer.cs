using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class SuikaDestroyer : Suika
    {
        protected override void OnCollision(Suika other)
        {
            CollisionService.OnCollisionDestroy(this, other);
        }

        private void Update()
        {
            Rigidbody2D.AddForce(Vector2.down * gravityScale, ForceMode2D.Force);
        }
    }
}