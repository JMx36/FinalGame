using UnityEngine;
//Josh Castillo

public class Bullet : MonoBehaviour
{
    public float speed = 20f;
    public Rigidbody2D bullet;
    public Transform player;

    [SerializeField] private float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        //velocity is given to the bullet as it spawns
        bullet.velocity = transform.right * speed;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.position);
        if (distance > maxDistance)
        {
            DestroyBullet();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        DestroyBullet();
    }
    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
