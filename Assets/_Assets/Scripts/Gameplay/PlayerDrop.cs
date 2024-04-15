using System.Collections;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Factories;
using UnityEngine;

namespace _Assets.Scripts.Gameplay
{
    public class PlayerDrop
    {
        private readonly CoroutineRunner _coroutineRunner;
        private SuikasFactory _suikasFactory;
        private readonly Transform _transform;
        private Rigidbody2D _suikaRigidbody;
        private bool _canDrop = true;
        private readonly YieldInstruction _wait = new WaitForSeconds(0.25f);

        public PlayerDrop(CoroutineRunner coroutineRunner, SuikasFactory suikasFactory, Transform transform)
        {
            _coroutineRunner = coroutineRunner;
            _suikasFactory = suikasFactory;
            _transform = transform;
        }

        public bool TryDrop()
        {
            if (_canDrop)
            {
                Drop();
                _coroutineRunner.StartCoroutine(Cooldown());
                return true;
            }

            return false;
        }

        public void SpawnSuika() => Spawn();

        public void SpawnContinue() => _suikaRigidbody = _suikasFactory.CreatePlayerContinue(_transform.position, _transform);

        private void Spawn()
        {
            _suikaRigidbody = _suikasFactory.CreateKinematic(_transform.position, _transform);
        }

        private void Drop()
        {
            _suikaRigidbody.transform.parent = null;
            _suikaRigidbody.isKinematic = false;
            if (_suikaRigidbody.TryGetComponent(out Suika suika))
            {
                suika.Drop();
                _suikaRigidbody = null;
            }
        }

        private IEnumerator Cooldown()
        {
            _canDrop = false;
            yield return _wait;
            Spawn();
            _canDrop = true;
        }
    }
}