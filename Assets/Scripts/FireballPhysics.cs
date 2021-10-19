using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FireballPhysics : MonoBehaviour
{
    public float speed = 30f;
    private int score1 = 0;
    private int score2 = 0;
    public int winValue = 1;
    public Text counter1;
    public Text counter2;
    private Color color;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rgdBody2D;
    //private float startSpeed;
    private float time;
    public float acceleration = 1.2f;
    [SerializeField] private AudioClip scoreUp;
    private AudioSource audioSource;
    public WinScreenScpript BlueWin;
    public WinScreenScpript RedWin;

    void Start()
    {
        time = 0f;
        audioSource = GetComponent<AudioSource>();
        rgdBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        color = spriteRenderer.color;
        Respawn(2f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        time += Time.deltaTime;

        if (time > 1f)
        {
            rgdBody2D.velocity *= acceleration;
            time = 0f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float respawnTime = 1.0f;
        if (collision.gameObject.name == "RaketLeft")
        {
            float y = HitFactor(this.transform.position, collision.transform.position, collision.collider.bounds.size.y);
            Vector2 dir = new Vector2(1, y).normalized;
            rgdBody2D.velocity = dir * speed;
        }
        if (collision.gameObject.name == "RaketRight")
        {
            float y = HitFactor(this.transform.position, collision.transform.position, collision.collider.bounds.size.y);
            Vector2 dir = new Vector2(-1, y).normalized;
            rgdBody2D.velocity = dir * speed;
        }
        if (collision.gameObject.name == "LeftWall")
        {
            audioSource.PlayOneShot(scoreUp);
            score2++;
            counter2.text = score2.ToString();
            if (score2 == winValue)
            {
                SwitchAlphaChannel(0);
                rgdBody2D.velocity = new Vector2(0, 0);
                //gameObject.SetActive(false);
                //Destroy(gameObject);
                RedWin.WinScreenAppear();
                StartCoroutine(ReloadScene());
                return;
            }
            
            Respawn(respawnTime);
        }
        if (collision.gameObject.name == "RightWall")
        {
            audioSource.PlayOneShot(scoreUp);
            score1++;
            counter1.text = score1.ToString();
            if (score1 == winValue)
            {
                rgdBody2D.velocity = new Vector2(0, 0);
                SwitchAlphaChannel(0);
                //gameObject.SetActive(false);
                //Destroy(gameObject);
                BlueWin.WinScreenAppear();
                StartCoroutine(ReloadScene());
                return;
            }
            
            Respawn(respawnTime);
        }
    }

    private float HitFactor(Vector2 ballPostion, Vector2 racketPosition, float racketHeight)
    {
        return (ballPostion.y - racketPosition.y) / racketHeight;
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void Respawn(float respawnTime)
    {
        this.gameObject.transform.position = new Vector3(0, 0, 0);
        SwitchAlphaChannel(0);
        rgdBody2D.velocity = new Vector2(0, 0);
        StartCoroutine(The_Ball_Is_Alive(respawnTime));
    }

    private IEnumerator The_Ball_Is_Alive(float respawnTime)
    {
        float y = Random.Range(-1, 2);
        float x = Random.Range(-1, 2);
        x = x == 0 ? -1 : x;
        y = y == 0 ? -1 : y;
        yield return new WaitForSeconds(respawnTime);//время на респаун
        SwitchAlphaChannel(1);
        yield return new WaitForSeconds(0.5f);//время увидеть шарик
        rgdBody2D.velocity = new Vector2(x, y) * speed;
    }

    private void SwitchAlphaChannel(float alphaValue)
    {
        color.a = alphaValue;
        spriteRenderer.color = color;
    }

}
