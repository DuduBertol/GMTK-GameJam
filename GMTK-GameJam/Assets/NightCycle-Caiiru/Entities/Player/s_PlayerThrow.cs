 
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class s_PlayerThrow : MonoBehaviour
{
    [Header("Throw Settings")] [SerializeField]
    private bool _canThrow = false; 
    private Vector3 _forceVelocity;
    [SerializeField] private Vector3 _destinationPosition = Vector3.zero;
    [SerializeField] private float _height = 8f; // altura maxima do arco
    [SerializeField] private Transform _releasePosition;
    
    [Space(1)] 
    [SerializeField] private float _throwCooldown;
    private float _currentCooldown;
    
    [Header("Pool Settings")]
    public GameObject rockPrefab;

    [SerializeField] private int _rockCount;
    private GameObject[] _rocks;

    [Space(5)] [Header("Projection Settings")] [SerializeField]
    private GameObject _lineRendererGO;
    private LineRenderer _lineRenderer;

    [Range(10, 100)][SerializeField] private int _linePoints = 25;
    [Range(0.01f, 0.25f)][SerializeField]private float _timeBetweenPoints = 0.1f;

    [SerializeField]private LayerMask _rockCollisionLayerMask;
    
    [Header("Debug")] public bool playerIsHoldingRock = false;
    
 
    #region Components
    private Rigidbody _rockRigidbody;
    private s_PlayerAim _playerAim;
    

    #endregion
    void Start()
    {
        var _line = Instantiate(_lineRendererGO);
        _lineRenderer = _line.transform.GetComponent<LineRenderer>();
        
        _playerAim = GetComponent<s_PlayerAim>();
        _rockRigidbody = rockPrefab.GetComponent<Rigidbody>();

        int rockLayer = rockPrefab.gameObject.layer;

        _rocks = new GameObject[_rockCount];

        for (int i = 0; i < _rockCount; i++)
        {
            var _instance = Instantiate(rockPrefab);
            _instance.SetActive(false);
            _instance.transform.SetParent(transform.GetChild(0));
            _rocks[i] = _instance;
            
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        if (!playerIsHoldingRock) // Se o player n possuir nenhuma pedra, nada acontece
        {
            if(_lineRenderer.enabled) _lineRenderer.enabled = false;
            return;
        }
        
        CalculateThrow();

        if (Time.time > _currentCooldown && _canThrow == false)
        {
            _canThrow = true;
        }
    }
 
    private void CalculateThrow()
    {
        var position = _releasePosition.position;
        _destinationPosition = _playerAim.GetMouseWorldPosition();

        Vector3 direction = _destinationPosition - position;

        float h = direction.y;
        direction.y = 0;

        float distance = direction.magnitude;
        direction.y = distance;

        float velocityY = Mathf.Sqrt(2 * Physics.gravity.magnitude * _height);
        float timeToApex = velocityY / Physics.gravity.magnitude;
        float timeTotal = timeToApex + Mathf.Sqrt(2 * _height - h) / Physics.gravity.magnitude;

        float velocityXZ = distance / timeTotal;

        Vector3 velocityVector = direction.normalized * velocityXZ;
        velocityVector.y = velocityY;
        
        _forceVelocity = velocityVector * _rockRigidbody.mass;
        
        ShowProjection(_forceVelocity);
       
        //ThrowObject(force); 
    }

    private GameObject GetRock()
    {
        foreach (var _instance in _rocks)
        {
            if (!_instance.gameObject.activeInHierarchy)
            {
                return _instance;
            }
        }

        return null;
    }

    private void ThrowObject(Vector3 _force)
    {
        _canThrow = false;
        _currentCooldown = Time.time + _throwCooldown;
        var _rock = GetRock();
        _rock.transform.position = _releasePosition.position; 
        _rock.SetActive(true);
        _rock.GetComponent<s_Rock>().Create(_force, 5);

    }

    private void ShowProjection(Vector3 _velocityVector)
    {
        _lineRenderer.enabled = true;
        _lineRenderer.positionCount = Mathf.CeilToInt(_linePoints / _timeBetweenPoints) + 1;
        Vector3 startPosition = _releasePosition.position;
        Vector3 startVelocity = _velocityVector / _rockRigidbody.mass;

        int i = 0;
        _lineRenderer.SetPosition(i, startPosition);
        for (float time = 0; time < _linePoints; time += _timeBetweenPoints)
        {
            i++;
            Vector3 point = startPosition + time * startVelocity;
            point.y = startPosition.y + startVelocity.y * time + (Physics.gravity.y / 2f * time * time);
            _lineRenderer.SetPosition(i,point);

            Vector3 lastPosition = _lineRenderer.GetPosition(i - 1);
            if (Physics.Raycast(lastPosition, (point - lastPosition).normalized, out RaycastHit hit,
                    (point - lastPosition).magnitude, _rockCollisionLayerMask))
            {
                _lineRenderer.SetPosition(i, hit.point);
                _lineRenderer.positionCount = i + 1;
                return;
            }
        }
    }

    public void OnFireAction(InputAction.CallbackContext context)
    { 
        if(_canThrow && playerIsHoldingRock)
            ThrowObject(_forceVelocity);
    }
}
