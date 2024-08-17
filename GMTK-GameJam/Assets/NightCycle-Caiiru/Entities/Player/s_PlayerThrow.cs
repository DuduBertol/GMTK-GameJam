 
using UnityEngine;
using UnityEngine.InputSystem;

public class s_PlayerThrow : MonoBehaviour
{
    [Header("Throw Settings")]
    [SerializeField] private float _objectTimeFly = 5f;
    [SerializeField] private Vector3 _destinationPosition = Vector3.zero;
    [SerializeField] private bool _isFiring;
    
    [SerializeField] private float _height = 8f; // altura maxima do arco
    [SerializeField] private Transform _releasePosition;
    public GameObject rockPrefab;

    [Space(5)] [Header("Projection Settings")]
    [SerializeField]private LineRenderer _lineRenderer;

    [Range(10, 100)][SerializeField] private int _linePoints = 25;
    [Range(0.01f, 0.25f)][SerializeField]private float _timeBetweenPoints = 0.1f;

    [SerializeField]private LayerMask _rockCollisionLayerMask;
    
    [Header("Debug")] public bool playerIsHoldingRock = false;
    private Rigidbody _rockRigidbody;

    public bool fire;
    #region Components

    private s_PlayerAim _playerAim;
    

    #endregion
    void Start()
    {
        _playerAim = GetComponent<s_PlayerAim>();
        _rockRigidbody = rockPrefab.GetComponent<Rigidbody>();

        int rockLayer = rockPrefab.gameObject.layer;
        /*
        for (int i = 0; i < 31; i++)
        {
            if (!Physics.GetIgnoreLayerCollision(_rockCollisionLayerMask, i))
            {
                _rockCollisionLayerMask |= 1 << i;
            }
        }
        */
    }

    private void Fire()
    {
        fire = false;
        
        var _rock = Instantiate(rockPrefab); 
        var position = _releasePosition.position;
        _rock.transform.position = position;


            
        _rock.GetComponent<Rigidbody>().AddForce(new Vector3(10,9,0), ForceMode.Impulse);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (fire)
            Fire();
        
        if (!playerIsHoldingRock)
        {
            if(_lineRenderer.enabled) _lineRenderer.enabled = false;
            return;
        }

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

        Vector3 force = velocityVector;
        
        
        
       
        ShowProjection(force); 
        
        if (_isFiring)
        {
            
            _isFiring = false;
            var _rock = Instantiate(rockPrefab); 
            _rock.transform.position = position;


            
            _rock.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);


        }
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
        _isFiring = context.performed; 
    }
}
