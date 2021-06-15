using System.Collections;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

public class Player : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float padding = 1f;
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    private Coroutine firingCoroutine;
    
    private float xMin;
    private float xMax;
    private float yMin;
    private float yMax;

    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }
    
    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;


        var position = transform.position;
        var newXPos = Mathf.Clamp(position.x + deltaX, xMin, xMax);
        var newYPos = Mathf.Clamp(position.y + deltaY, yMin, yMax);
        
        position = new Vector2(newXPos, newYPos);
        transform.position = position;
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) firingCoroutine = StartCoroutine(FireContinuously());
        if (Input.GetButtonUp("Fire1")) StopCoroutine(firingCoroutine);
    }

    private IEnumerator FireContinuously()
    {
        while (true)
        {
            var laser = Instantiate(laserPrefab, transform.position, Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }
    
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + padding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - padding;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + padding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - padding;
    }
}
