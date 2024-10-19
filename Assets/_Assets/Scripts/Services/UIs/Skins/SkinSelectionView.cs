using System.Collections.Generic;
using _Assets.Scripts.Services.Skins;
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
        [Inject] private UIStateMachine _uiStateMachine;
        private int _firstSkinIndex = -1, _secondSkinIndex = -1;
        private readonly List<RaycastResult> _results = new(10);

        private bool _fromSkinList;
        private Transform _firstSkinTransform;
        private Vector3 _firstSkinPosition;

        private void Start()
        {
            backButton.onClick.AddListener(Back);
            Init();
        }

        private void Back()
        {
            _uiStateMachine.SwitchToPreviousState().Forget();
        }

        public async void Init()
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

            for (int i = 0; i < _skinService.SelectedSkinLength; i++)
            {
                skins[i].Init(await _skinService.GetSprite(i), i);
            }
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
                Vector3 localPosition = _firstSkinTransform.root.InverseTransformPoint(worldPosition);
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

                    if (skinView != null && skinView.SkinIndex != _firstSkinIndex)
                    {
                        found = true;
                        if (_firstSkinIndex == -1)
                        {
                            _firstSkinTransform = skinView.transform;
                            _firstSkinIndex = skinView.SkinIndex;
                            _firstSkinPosition = skinView.transform.position;
                            _firstSkinTransform.SetAsLastSibling();
                            Debug.Log($"First: {_firstSkinIndex}");
                            break;
                        }

                        if (_secondSkinIndex == -1)
                        {
                            _secondSkinIndex = skinView.SkinIndex;
                            Debug.Log($"Second: {_secondSkinIndex}");

                            //TODO: 

                            if (_fromSkinList)
                            {
                                _skinService.Set(_firstSkinIndex, _secondSkinIndex);
                            }
                            else
                            {
                                _skinService.Swap(_firstSkinIndex, _secondSkinIndex);
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
                    if (skinView != null && skinView.SkinIndex != _firstSkinIndex)
                    {
                        found = true;
                        if (_firstSkinIndex == -1)
                        {
                            _firstSkinTransform = skinView.transform;
                            _firstSkinIndex = skinView.SkinIndex;
                            _firstSkinPosition = skinView.transform.position;
                            _firstSkinTransform.SetAsLastSibling();
                            _fromSkinList = true;
                            Debug.Log($"First: {_firstSkinIndex}");
                            break;
                        }
                    }
                }
            }

            if (!found)
            {
                if (_firstSkinTransform != null)
                {
                    _firstSkinTransform.position = _firstSkinPosition;
                    _firstSkinTransform = null;
                }
            }
        }

        private void ResetSelection()
        {
            _firstSkinIndex = -1;
            _secondSkinIndex = -1;

            if (_firstSkinTransform != null)
            {
                _firstSkinTransform.position = _firstSkinPosition;
                _firstSkinTransform = null;
            }

            _fromSkinList = false;
        }
    }
}