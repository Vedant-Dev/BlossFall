using UnityEngine;
public class Block : MonoBehaviour
{
    public GameManager gam;
    public GameObject deathParticle;
    private Rigidbody rb;
    public float speed;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        var gravityVector = new Vector3(0f, -9.81f, 0);
        rb.AddForce(gravityVector * Time.deltaTime, ForceMode.Acceleration);
        gam = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
    }

    private void Update()
    {
        transform.position += Vector3.down * speed * Time.deltaTime;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Ground"))
        {
            GameObject deathParticleGO = Instantiate(deathParticle, new Vector3(transform.position.x, transform.position.y+2, transform.position.z), Quaternion.identity);
            Destroy(deathParticleGO, 2f);
            Destroy(gameObject);
            gam.AddScore();
        }
        if (collision.gameObject.tag.Equals("Player"))
        {
            GameObject deathParticleGO = Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(deathParticleGO, 2f);
            Destroy(gameObject);
            gam.SetDeath();
        }
    }
}
