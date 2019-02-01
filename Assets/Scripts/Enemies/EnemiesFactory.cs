
using UnityEngine;

public class EnemiesFactory : MonoBehaviour {

    private void Awake(){
        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        collider.size = new Vector2(0.5f, 9.5f);
    }

    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag == "Player") {
            Destroy(GetComponent<Collider2D>());
            WorldManager.level++;

            for (int i = 0; i < WorldManager.Difficulty; i++){
                int randomInstance = Random.Range(0, 6);
                float randomX = transform.position.x + (6 * (Random.Range(0,2) * 2 - 1));
                float randomY = Random.Range(4, 8);

                switch (randomInstance) {
                    case 0:
                        Enemies<Gigantor> giantGeorge = new Enemies<Gigantor>("GiantGeorge");
                        giantGeorge.scriptComponent.Initialize(speed: 1, position: new Vector3(randomX, randomY, 1));
                        break;
                    case 1:
                        Enemies<Tweaker> TweakyTim = new Enemies<Tweaker>("TweakyTim");
                        TweakyTim.scriptComponent.Initialize(speed: 4, position: new Vector3(randomX, randomY, 1));
                        break;
                    case 2:
                        Enemies<Lush> lushyLinda = new Enemies<Lush>("LushyLinda");
                        lushyLinda.scriptComponent.Initialize(speed: Random.Range(6, 18), position: new Vector3(randomX, randomY, 1));
                        break;
                    case 3:
                        Enemies<Bouncer> bouncyBill = new Enemies<Bouncer>("BouncyBill");
                        bouncyBill.scriptComponent.Initialize(speed: 4, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(randomX, randomY, 1));
                        break;
                    case 4:
                        Enemies<Torque> TorqyTom = new Enemies<Torque>("TorquyTom");
                        TorqyTom.scriptComponent.Initialize(speed: 3, direction: Random.Range(0, 2) * 2 - 1, position: new Vector3(randomX, randomY, 1));
                        break;
                    case 5:
                        Enemies<Ghost> ghostlyGayle = new Enemies<Ghost>("GhostlyGayle");
                        ghostlyGayle.scriptComponent.Initialize(speed: 2, position: new Vector3(randomX, randomY, 1));
                        break;

                }
            }
        }
    }
}
