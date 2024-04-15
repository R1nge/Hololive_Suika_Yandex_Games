using System.Collections;
using _Assets.Scripts.Services;
using UnityEngine;
using VContainer;

namespace _Assets.Scripts.Gameplay
{
    public class Suika : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private PolygonColliderOptimizer polygonColliderOptimizer;
        [SerializeField] private float originalScale;
        [SerializeField] protected float gravityScale = 100;
        [SerializeField] protected Rigidbody2D Rigidbody2D;
        public PolygonColliderOptimizer PolygonColliderOptimizer => polygonColliderOptimizer;
        public bool HasLanded => _landed;
        public bool HasDropped => _dropped;

        public bool HasCollided
        {
            get => _collided;
            set => _collided = value;
        }

        public SpriteRenderer SpriteRenderer => spriteRenderer;
        protected internal int Index;
        private bool _landed;
        private bool _dropped;
        private bool _collided;
        [Inject] protected ContinueGameService ContinueGameService;
        [Inject] protected CollisionService CollisionService;

        public void SetIndex(int index) => Index = index;

        public void Drop()
        {
            _dropped = true;
            ContinueGameService.AddSuika(this);
        }

        private void Update()
        {
           if (!_dropped) return;
           if (_landed) return;
           Rigidbody2D.AddForce(Vector2.down * gravityScale, ForceMode2D.Force);
        }

        public void Land() => _landed = true;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!_dropped) return;

            if (_collided) return;

            _landed = true;

            if (!other.gameObject.TryGetComponent(out Suika suika)) return;

            if (!suika.HasDropped) return;
            
            if (suika.HasCollided) return;

            OnCollision(suika);
        }

        protected virtual void OnCollision(Suika other)
        {
            if (other.Index != Index) return;
            other.HasCollided = true;
            _collided = true;
            CollisionService.OnCollision(this, other);
        }

        public void SetSprite(Sprite sprite)
        {
            spriteRenderer.sprite = sprite;
            spriteRenderer.size = new Vector2(256, 256);
        }

        private IEnumerator ScaleCoroutine(float duration)
        {
            var time = 0f;
            var startLocalScale = new Vector3(0, 0, 0);
            var endLocalScale = new Vector3(originalScale, originalScale, originalScale);

            while (time < duration)
            {
                var t = time / duration;
                transform.localScale = Vector3.Lerp(startLocalScale, endLocalScale, t);
                time += Time.deltaTime;
                yield return null;
            }

            transform.localScale = endLocalScale;
            polygonColliderOptimizer.OptimizePolygonCollider(.01f);
        }

        public void Scale(float duration)
        {
            StartCoroutine(ScaleCoroutine(duration));
        }
    }
}