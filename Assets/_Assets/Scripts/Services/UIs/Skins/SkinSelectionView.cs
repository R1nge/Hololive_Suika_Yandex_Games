using System.Collections.Generic;
using _Assets.Scripts.Services.Skins;
using _Assets.Scripts.Services.StateMachine;
using _Assets.Scripts.Services.UIs.StateMachine;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using VContainer;

namespace _Assets.Scripts.Services.UIs.Skins
{
    public class SkinSelectionView : MonoBehaviour
    {
        [SerializeField] private float radius = 50f;
        [SerializeField] private SkinView[] skins;
        [SerializeField] private Button backButton;
        [Inject] private SkinService _skinService;
        [Inject] private GameStateMachine _gameStateMachine;
        private readonly List<RaycastResult> _results = new(10);

        private bool _fromSkinList;
        private Transform _firstSkinTransform;
        private Vector3 _firstSkinPosition;

        private void Start() => backButton.onClick.AddListener(Back);

        private void Back() => _gameStateMachine.SwitchState(GameStateType.MainMenu).Forget();

        public async UniTask Init()
        {
            float scaleFactor = 0.75f;
            float totalScale = 0f;
            float[] scales = new float[skins.Length];

            // First, calculate the total scale sum to determine the normalized angle step
            for (int i = 0; i < skins.Length; i++)
            {
                scales[i] = scaleFactor * (1f + i * 0.5f);
                totalScale += scales[i];
            }

            float angleStep = 360f / totalScale;
            float currentAngle = 0f;

            for (int i = 0; i < skins.Length; i++)
            {
                skins[i].transform.localScale = new Vector3(scales[i], scales[i], 1f);

                float angle = currentAngle * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;

                skins[i].transform.localPosition = new Vector3(x, y, skins[i].transform.localPosition.z);

                currentAngle +=
                    angleStep * scales[i]; // Increment current angle based on the normalized angle step and skin scale
            }

            var initTasks = new List<UniTask>();
            for (int i = 0; i < _skinService.SelectedSkinLength; i++)
            {
                var index = i;
                initTasks.Add(InitializeSkin(index));
            }
            
            await UniTask.WhenAll(initTasks);
        }

        private async UniTask InitializeSkin(int index)
        {
            var sprite = await _skinService.GetSprite(index);
            skins[index].Init(sprite, index);
        }

        private async void UpdateSprite()
        {
            for (int i = 0; i < _skinService.SelectedSkinLength; i++)
            {
                skins[i].UpdateSprite(await _skinService.GetSprite(i));
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectSkinView();
            }

            if (_firstSkinTransform != null)
            {
                Vector3 mousePosition = Camera.main.WorldToScreenPoint(Input.mousePosition);
                Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 localPosition = _firstSkinTransform.parent.InverseTransformPoint(worldPosition);
                _firstSkinTransform.localPosition = localPosition;
            }
        }

        private void SelectSkinView()
        {
            PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
            {
                // Set the PointerEventData position to that of the mouse position
                position = Input.mousePosition
            };

            EventSystem.current.RaycastAll(pointerEventData, _results);

            bool found = false;
            for (var i = 0; i < _results.Count; i++)
            {
                var result = _results[i];
                if (result.gameObject.layer == LayerMask.NameToLayer("SkinSelection"))
                {
                    Debug.Log("Pointer is over " + gameObject.name);
                    var skinView = result.gameObject.GetComponent<SkinView>();

                    if (skinView != null && skinView.SkinIndex != _skinService.FirstSkinIndex)
                    {
                        found = true;
                        if (_skinService.FirstSkinIndex == -1)
                        {
                            _firstSkinTransform = skinView.transform;
                            _skinService.SetFirstSkin(skinView.SkinIndex);
                            _firstSkinPosition = skinView.transform.position;
                            _firstSkinTransform.SetAsLastSibling();
                            Debug.Log($"First: {_skinService.FirstSkinIndex}");
                            break;
                        }

                        if (_skinService.SecondSkinIndex == -1)
                        {
                            _skinService.SetSecondSkin(skinView.SkinIndex);
                            Debug.Log($"Second: {_skinService.SecondSkinIndex}");

                            if (_fromSkinList)
                            {
                                _skinService.Set(_skinService.FirstSkinIndex, _skinService.SecondSkinIndex);
                            }
                            else
                            {
                                _skinService.Swap(_skinService.FirstSkinIndex, _skinService.SecondSkinIndex);
                            }

                            ResetSelection();
                            UpdateSprite();
                            break;
                        }
                    }
                }
                else if (result.gameObject.layer == LayerMask.NameToLayer("SkinsList"))
                {
                    Debug.Log("Pointer is over " + gameObject.name);
                    var skinView = result.gameObject.GetComponent<SkinView>();
                    if (skinView != null && skinView.SkinIndex != _skinService.FirstSkinIndex)
                    {
                        found = true;
                        if (_skinService.FirstSkinIndex == -1)
                        {
                            _firstSkinTransform = skinView.transform;
                            _skinService.SetFirstSkin(skinView.SkinIndex);
                            _firstSkinPosition = skinView.transform.position;
                            _firstSkinTransform.SetAsLastSibling();
                            _fromSkinList = true;
                            Debug.Log($"First: {_skinService.FirstSkinIndex}");
                            break;
                        }
                    }
                }
            }

            if (!found)
            {
                ResetSelection();
            }
        }

        private void ResetSelection()
        {
            if (_firstSkinTransform != null)
            {
                _firstSkinTransform.position = _firstSkinPosition;
                _firstSkinTransform = null;

                _skinService.SetFirstSkin(-1);
                _skinService.SetSecondSkin(-1);
                _fromSkinList = false;
            }
        }
    }
}