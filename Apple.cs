using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    [SerializeField] PlayerControl pc;

    private void OnCollisionEnter2D(Collision2D col)
    {
        int x = Random.Range(-10, 10);
        int y = Random.Range(-6, 6);

        if (col.gameObject.tag == "dead")
        {
            Destroy(gameObject);
            if (pc.score < 240)
            {
                Instantiate(gameObject, new Vector2(x, y), Quaternion.identity);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "player")
        {
            pc.score++;
            Destroy(gameObject);
        }
    }
}
