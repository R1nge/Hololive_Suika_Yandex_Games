using System.Collections;
using _Assets.Scripts.Misc;
using _Assets.Scripts.Services.Factories;
using Cysharp.Threading.Tasks;
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

        public void SpawnContinue() =>
            _suikaRigidbody = _suikasFactory.CreatePlayerContinue(_transform.position, _transform);

        private async void Spawn()
        {
            _suikaRigidbody = await _suikasFactory.CreateKinematic(_transform.position, _transform);
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
            yield return new WaitForSeconds(.25f);

            if (_transform != null)
            {
                Spawn();
            }

            _canDrop = true;
        }
    }
}