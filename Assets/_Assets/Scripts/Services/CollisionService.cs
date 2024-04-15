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

        private CollisionService(SuikasFactory suikasFactory, ResetService resetService)
        {
            _suikasFactory = suikasFactory;
            _resetService = resetService;
        }

        public void OnCollision(Suika suika, Suika other)
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
                Object.Destroy(suika.gameObject);
                Object.Destroy(other.gameObject);
                _suikasFactory.Create(suikaIndex, middle);
            }
        }

        public void OnCollisionDestroy(Suika destroyer, Suika other)
        {
            _resetService.RemoveSuika(destroyer);
            _resetService.RemoveSuika(other);
            Object.Destroy(destroyer.gameObject);
            Object.Destroy(other.gameObject);
        }
    }
}