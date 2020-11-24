using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class PlayerView : MonoBehaviour
{
    public delegate void Damage();
    public event Damage OnDamaged;

    private Vector2 position;
    private Quaternion rotation;
    private Rigidbody2D velocity;
    public GameManager manager;

    public Text hp;
    public Text scoreText;
    public GameObject ResultPanel;
    public Text Result;
    public Image lineFirstToSecond;
    public Image lineSecondToThird;
    public Text textSecondLevel;
    public Text textThirdLevel;
    public Button firstLevel;
    public Button secondLevel;
    public Button thirdLevel;
    public GameObject menu;

    public void Start()
    {
        position = transform.position;
        rotation = transform.rotation;
        velocity = gameObject.GetComponent<Rigidbody2D>();
        manager.Subscribe(firstLevel,secondLevel,thirdLevel);

    }
    public void Update()
    {
        position = transform.position;
        rotation = transform.rotation;
    }
    public void UpdateVelocity(Vector2 Velocity)
    {
        velocity.velocity = Velocity;
    }
    public void UpdatePosition(Vector2 positionNew)
    {
        transform.position = new Vector2(positionNew.x, positionNew.y);   
    }
    public void UpdateRotation(Quaternion rotationNew)
    {
        transform.rotation = new Quaternion(rotationNew.x, rotationNew.y, rotationNew.z, rotationNew.w);
    }
    public Vector2 GetPosition()
    {
        return position;
    }
    public Quaternion GetRotation()
    {
        return rotation;
    }
    public void InstanceShot(GameObject shot,Transform shotPoint)
    {
        Instantiate(shot, shotPoint.position, shotPoint.rotation);
    }
    public void Lose()
    {
        manager.WaitForClick(ResultPanel);
        ResultPanel.SetActive(true);
        Result.text = "Поражение";
    }
    public void Win()
    {
        manager.WaitForClick(ResultPanel);
        ResultPanel.SetActive(true);
        Result.text = "Победа";
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            OnDamaged();
        }
    }
    public void ChangeHealthPoint(int healthPoint)
    {
       hp.text = healthPoint.ToString();
    }
    public void ChangeScore(int score)
    {
        scoreText.text = score.ToString();
    }

    public void Colored(int currentLevel)
    {
        if (currentLevel >= 1)
        {
            lineFirstToSecond.color = Color.white;
            var colorButtonSecond = secondLevel.gameObject.GetComponent<Image>();
            colorButtonSecond.color = Color.white;
            textSecondLevel.color = Color.white;
            if (currentLevel >= 2)
            {
                lineSecondToThird.color = Color.white;
                var colorButtonThird = thirdLevel.gameObject.GetComponent<Image>();
                colorButtonThird.color = Color.white;
                textSecondLevel.color = Color.white;
            }
        }
    }
    public void OnKeyDown()
    {
        menu.SetActive(true);
        ResultPanel.SetActive(false);
    }

}
