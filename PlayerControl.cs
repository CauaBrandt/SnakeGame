using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public float score = 0;
    public float Speed;
    public bool IsFacingWest = false;
    public bool IsFacingEast = false;
    public bool IsFacingSouth = false;
    public bool IsFacingNorth = false;

    public GameObject Apple;
    public GameObject Player;
    public GameObject Snake;
    public Transform GameOver;
    private Transform newTail;
    public Transform SnakeBody;
    private Vector2 dir;
    public Animator GameOverAni;
    public Animator WinAni;
    public TextMeshProUGUI ScoreT;

    public List<Transform> Tail = new List<Transform>();

    void Start()
    {
        int x = Random.Range(-10, 10);
        int y = Random.Range(-6, 6);
        Instantiate(Apple, new Vector2(x, y), Quaternion.identity);

        InvokeRepeating("PlayerMove", 0f, Speed);
    }
    void Update()
    {
        if (dir == Vector2.right)
        {
            if (Input.GetKeyDown("a"))
            {
                dir = dir;
            }
            else if (Input.GetKeyDown("w"))
            {
                dir = Vector2.up;
                IsFacingNorth = true;
                IsFacingWest = false;
                IsFacingEast = false;
                IsFacingSouth = false;
            }
            else if (Input.GetKeyDown("s"))
            {
                dir = Vector2.down;
                IsFacingSouth = true;
                IsFacingWest = false;
                IsFacingEast = false;
                IsFacingNorth = false;
            }
        }
        else if (dir == Vector2.left)
        {
            if (Input.GetKeyDown("d"))
            {
                dir = dir;
            }
            else if (Input.GetKeyDown("w"))
            {
                dir = Vector2.up;
                IsFacingNorth = true;
                IsFacingWest = false;
                IsFacingEast = false;
                IsFacingSouth = false;
            }
            else if (Input.GetKeyDown("s"))
            {
                dir = Vector2.down;
                IsFacingSouth = true;
                IsFacingWest = false;
                IsFacingEast = false;
                IsFacingNorth = false;
            }
        }
        else if (dir == Vector2.up)
        {
            if (Input.GetKeyDown("s"))
            {
                dir = dir;
            }
            else if (Input.GetKeyDown("d"))
            {
                dir = Vector2.right;
                IsFacingEast = true;
                IsFacingWest = false;
                IsFacingSouth = false;
                IsFacingNorth = false;
            }
            else if (Input.GetKeyDown("a"))
            {
                dir = Vector2.left;
                IsFacingWest = true;
                IsFacingEast = false;
                IsFacingSouth = false;
                IsFacingNorth = false;
            }
        }
        else if (dir == Vector2.down)
        {
            if (Input.GetKeyDown("w"))
            {
                dir = dir;
            }
            else if (Input.GetKeyDown("d"))
            {
                dir = Vector2.right;
                IsFacingEast = true;
                IsFacingWest = false;
                IsFacingSouth = false;
                IsFacingNorth = false;
            }
            else if (Input.GetKeyDown("a"))
            {
                dir = Vector2.left;
                IsFacingWest = true;
                IsFacingEast = false;
                IsFacingSouth = false;
                IsFacingNorth = false;
            }
        }
        else
        {
            if (Input.GetKeyDown("d"))
            {
                dir = Vector2.right;
                IsFacingEast = true;
                IsFacingWest = false;
                IsFacingSouth = false;
                IsFacingNorth = false;
            }
            else if (Input.GetKeyDown("a"))
            {
                dir = Vector2.left;
                IsFacingWest = true;
                IsFacingEast = false;
                IsFacingSouth = false;
                IsFacingNorth = false;
            }
            else if (Input.GetKeyDown("w"))
            {
                dir = Vector2.up;
                IsFacingNorth = true;
                IsFacingWest = false;
                IsFacingEast = false;
                IsFacingSouth = false;
            }
            else if (Input.GetKeyDown("s"))
            {
                dir = Vector2.down;
                IsFacingSouth = true;
                IsFacingWest = false;
                IsFacingEast = false;
                IsFacingNorth = false;
            }
        }
        ScoreT.text = score.ToString();

        if(score >= 240)
        {
            WinAni.SetBool("Win", true);
            CancelInvoke("PlayerMove");
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    void PlayerMove()
    {
        var cur = transform.position;

        transform.Translate(dir);

        if(Tail.Count > 0)
        {
            Tail.Last().position = cur;

            Tail.Insert(0, Tail.Last());
            Tail.RemoveAt(Tail.Count - 1);
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "dead")
        {
            CancelInvoke("PlayerMove");
            GameOverAni.SetBool("IsDead", true);
            if (IsFacingEast == true)
            {
                Player.transform.position = new Vector2(Player.transform.position.x -1, Player.transform.position.y);
            }
            else if (IsFacingWest == true)
            {
                Player.transform.position = new Vector2(Player.transform.position.x + 1, Player.transform.position.y);
            }
            else if (IsFacingNorth == true)
            {
                Player.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y - 1);
            }
            else if (IsFacingSouth == true)
            {
                Player.transform.position = new Vector2(Player.transform.position.x, Player.transform.position.y + 1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int x = Random.Range(-10, 10);
        int y = Random.Range(-6, 6);

        if (collision.gameObject.tag == "apple")
        {
            if (score < 240)
            {
                Instantiate(Apple, new Vector2(x, y), Quaternion.identity);
            }
            if(Tail.Count > 0)
            {
                newTail = Instantiate(SnakeBody, Tail.Last().transform.position, Quaternion.identity);
            }
            else
            {
                newTail = Instantiate(SnakeBody, new Vector2(300,300), Quaternion.identity);
            }
            newTail.transform.parent = Snake.transform;
            Tail.Add(newTail);
        }
    }
}
