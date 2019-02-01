
using UnityEngine;

public class StompController : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Enemy"){
            GameObject.Destroy(collision.gameObject);

            Rigidbody2D cheeseHead = GameObject.Find("CheeseHead").GetComponent<Rigidbody2D>();
            cheeseHead.velocity = new Vector2(cheeseHead.velocity.x, 0);
            cheeseHead.AddForce(new Vector2(0, 5), ForceMode2D.Impulse);

            GetComponent<AudioSource>().Play();

            WorldManager.score += 300;
        }
    }
}
