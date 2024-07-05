using _Assets.Scripts.Gameplay;
using _Assets.Scripts.Services.Factories;
using _Assets.Scripts.Services.StateMachine;
using UnityEngine;

namespace _Assets.Scripts.Services
{
    public class CollisionService
    {
        private readonly SuikasFactory _suikasFactory;
        private readonly ResetService _resetService;
        private readonly ContinueService _continueService;

        private CollisionService(SuikasFactory suikasFactory, ResetService resetService, ContinueService continueService)
        {
            _suikasFactory = suikasFactory;
            _resetService = resetService;
            _continueService = continueService;
        }

        public async void OnCollision(Suika suika, Suika other)
        {
            if (suika.Index == other.Index)
            {
                var middle = (suika.transform.position + other.transform.position) / 2f;
                //Or move it to the another suika position
                //var suikaPosition = suika.transform.position;
                //newSuikaInstance.transform.position = suikaPosition;
                var suikaIndex = suika.Index;
                _resetService.RemoveSuika(suika);
                _resetService.RemoveSuika(other);
                _continueService.RemoveSuika(suika);
                _continueService.RemoveSuika(other);
                Object.Destroy(suika.gameObject);
                Object.Destroy(other.gameObject);
                var newSuika = await _suikasFactory.Create(suikaIndex, middle);
                newSuika.Drop();
                newSuika.Land();
            }
        }

        public void OnCollisionDestroy(Suika destroyer, Suika other)
        {
            _resetService.RemoveSuika(destroyer);
            _resetService.RemoveSuika(other);
            _continueService.RemoveSuika(destroyer);
            _continueService.RemoveSuika(other);
            Object.Destroy(destroyer.gameObject);
            Object.Destroy(other.gameObject);
        }
    }
}