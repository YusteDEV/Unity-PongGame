using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Transform player1T, player2T, ballT;
    public Rigidbody2D p1RB, p2RB, ballRB;
    public float speed = 10f, boundL = 4f;
    public Text p1ScoreT, p2ScoreT;
    private int P1Score, P2Score;
    // Start is called before the first frame update
    void Start()
    {
        P1Score = 0;
        P2Score = 0;
        /*p1ScoreT.text = P1Score.ToString();
        p2ScoreT.text = P2Score.ToString();*/
        Invoke("BallStart", 2);
    }

    // Update is called once per frame
    void Update()
    {
        //Player 1 Controller
        var vel1 = p1RB.velocity;
        if (Input.GetKey(KeyCode.W))
        {
            vel1.y = speed;
        }else if (Input.GetKey(KeyCode.S))
        {
            vel1.y = -speed;
        }
        else
        {
            vel1.y = 0;
        }
        p1RB.velocity = vel1;

        var pos1 = player1T.position;
        if (pos1.y > boundL)
        {
            pos1.y = boundL;
        }else if(pos1.y < -boundL)
        {
            pos1.y = -boundL;
        }
        player1T.position = pos1;

        //Player 2 Controller
        var vel2 = p2RB.velocity;
        if (Input.GetKey(KeyCode.O))
        {
            vel2.y = speed;
        }
        else if (Input.GetKey(KeyCode.L))
        {
            vel2.y = -speed;
        }
        else
        {
            vel2.y = 0;
        }
        p2RB.velocity = vel2;

        var pos2 = player2T.position;
        if (pos2.y > boundL)
        {
            pos2.y = boundL;
        }
        else if (pos2.y < -boundL)
        {
            pos2.y = -boundL;
        }
        player2T.position = pos2;

        //Score script
        if (ballT.position.x > 4)
        {
            P1Score++;
            GameRestart();
        }
        if (ballT.position.x < -4)
        {
            P2Score++;
            GameRestart();
        }
        p1ScoreT.text = P1Score.ToString();
        p2ScoreT.text = P2Score.ToString();
    }
    void BallStart()
    {
        float rSpawn = Random.Range(0, 2);
        if (rSpawn < 1)
        {
            ballRB.AddForce(new Vector2(20, -15));
        }else
        {
            ballRB.AddForce(new Vector2(-20, -15));
        }
    }
    void BallReset()
    {
        ballRB.velocity = Vector2.zero;
        ballT.position = Vector2.zero;
    }
    void GameRestart()
    {
        BallReset();
        Invoke("BallStart", 1);
    }
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Player"))
        {
            Vector2  velBall;
            velBall.x = ballRB.velocity.x;
            velBall.y = (ballRB.velocity.y / 2) + (coll.collider.attachedRigidbody.velocity.y / 3);
            ballRB.velocity = velBall;
        }
    }
}
