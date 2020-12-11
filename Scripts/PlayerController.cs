using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 0.01f;
    public GameManager gam;
    public GameObject deathParticle;
    public bool isLookingRight = true;
    public Animator anim;
    public float input;

    private void Update()
    {

        if (Input.touchCount > 0) 
        {
            if(Input.GetTouch(0).position.x > Screen.width / 2) {
                //move right
                input = 1.0f;
            }
            if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                //move left
                input = -1.0f;
            }
        }
        else
        {
            input = Input.GetAxis("Horizontal");
        }

        if (transform.position.y < -10)
        {
            gam.SetDeath();
        }
        if (transform.position.x < 31 && transform.position.x > -31 && !gam.GetDeath())
        {
            transform.position += Vector3.right * speed * Time.deltaTime * input;
            if (input != 0)
            {
                anim.SetBool("Walk Forward", true);
            }
            else
            {
                anim.SetBool("Walk Forward", false);
            }

            if (input != 0)
            {
                if (isLookingRight && input < 0)
                {
                    transform.Rotate(new Vector3(0f, -180f, 0f));
                    isLookingRight = false;
                }
                if (!isLookingRight && input > 0)
                {
                    transform.Rotate(new Vector3(0f, 180f, 0f));
                    isLookingRight = true;
                }
            }
        }

        if (gam.GetDeath())
        {
            anim.SetTrigger("Die");
            GameObject deathParticleGO = Instantiate(deathParticle, transform.position, Quaternion.identity);
            Destroy(deathParticleGO, 2f);
            Destroy(gameObject);
        }
    }
}