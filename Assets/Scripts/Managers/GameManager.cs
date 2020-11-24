using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public delegate void destroyAsteroidByPlayer();
    public event destroyAsteroidByPlayer OnDestroyAsteroidByPlayer;


    private SaveData data;

    public PlayerView view;
    public PlayerController controller;

    public GameObject[] asteroids;
    public GameObject asteroidBoss;
    public Sprite[] backgrounds;
    public GameObject[] bounds;
    public SpriteRenderer currentBackground;


    private GameObject currentAsteroid;
    private List<GameObject> listAsteroids;
    private System.IDisposable clickEvent;


    private bool play;
    private int currentLevel;
    public void Start()
    {
        SetBounds();
        data = SaveManager.Load();
        controller.GetModel().onDeath += Lose;
        OnDestroyAsteroidByPlayer += controller.KillAsteroid;
        view.OnDamaged += controller.Damage;
        play = false;
        view.Colored(data.CurrentLevel);
    }
    public void Update()
    {
        CheckWin();
    }
    private void StartGame(int amountAsteroids, int background, int typesAsteroids)
    {

        listAsteroids = new List<GameObject>();

        currentAsteroid = typesAsteroids != 4? asteroids[typesAsteroids] : asteroidBoss;
        currentBackground.sprite = backgrounds[background];
        play = true;
        view.menu.SetActive(false);
        data.SetData(amountAsteroids, background, typesAsteroids,currentLevel-1);
        Spawn(amountAsteroids);
    }
    private void Spawn(int currentAmount)
    {
        for (int i = 0; i < currentAmount; i++)
        {
            int randomSection = Random.Range(0, 4);
            Vector2 positionSpawn = new Vector2();
            switch (randomSection)
            {
                case 0:
                    positionSpawn = new Vector2(Random.Range(bounds[0].transform.position.x + 1, -2.0f), Random.Range(bounds[2].transform.position.y + 1, bounds[3].transform.position.y - 1));
                    break;
                case 1:
                    positionSpawn = new Vector2(Random.Range(bounds[0].transform.position.x + 1, bounds[1].transform.position.x - 1), Random.Range(2.0f, bounds[3].transform.position.y - 1));
                    break;
                case 2:
                    positionSpawn = new Vector2(Random.Range(2.0f, bounds[1].transform.position.x - 1), Random.Range(bounds[2].transform.position.y + 1, bounds[3].transform.position.y - 1));
                    break;
                case 3:
                    positionSpawn = new Vector2(Random.Range(bounds[0].transform.position.x + 1, bounds[1].transform.position.x - 1), Random.Range(bounds[2].transform.position.y + 1, -2.0f));
                    break;
            }
            GameObject asteroidNew = Instantiate(currentAsteroid);
            asteroidNew.transform.position = positionSpawn;
            listAsteroids.Add(asteroidNew);
        }
    }
    public void DestroyAsteroidByBullet(GameObject asteroid)
    {
        listAsteroids.Remove(asteroid);
        Destroy(asteroid);
        OnDestroyAsteroidByPlayer();
    }
    public void DestroyAsteroidbyPlayer(GameObject asteroid)
    {
        listAsteroids.Remove(asteroid);
        Destroy(asteroid);
    }
    public void DestroyBullet(GameObject bullet)
    {
        Destroy(bullet);
    }
    private void DestroyAll()
    {
        foreach (var asteroid in listAsteroids)
            Destroy(asteroid);
    }
    private void Lose()
    {
        play = false;
        SaveManager.Save(data);
        DestroyAll();
    }

    private void SetWin()
    {
        data.CurrentLevel = currentLevel;
        view.Colored(data.CurrentLevel);
        SaveManager.Save(data);
        controller.Win();
        view.menu.SetActive(true);
        play = false;
    }
    private void CheckWin()
    {
        if (play)
        {
            if (listAsteroids.Count == 0)
            {
                SetWin();
            }
        }
    }
    private void SetBounds()
    {
        bounds[0].transform.position = new Vector2(-CameraOptimize.ComputeResolution().Item1/2, 0);
        bounds[1].transform.position = new Vector2(CameraOptimize.ComputeResolution().Item1/2, 0);
        bounds[2].transform.position = new Vector2(0, -CameraOptimize.ComputeResolution().Item2/2);
        bounds[3].transform.position = new Vector2(0, CameraOptimize.ComputeResolution().Item2)/2;
    }
    public void Subscribe(Button firstLevel,Button secondLevel, Button thirdLevel)
    {
        var lvl1 = Observable.Merge(
                firstLevel.OnClickAsObservable().Select(_ => data));
        lvl1.Where(x => x.CurrentLevel >= 0)
                 .Subscribe(x =>
                 {
                     currentLevel = 1;
                     if (x._amount[0] != 0)
                         StartGame(x._amount[0], x._background[0], x._asteroidType[0]);
                     else
                         StartGame(Random.Range(5, 11), Random.Range(0, backgrounds.Length), Random.Range(0, asteroids.Length));

                 });
        var lvl2 = Observable.Merge(
                secondLevel.OnClickAsObservable().Select(_ => data));
        lvl2.Where(x => x.CurrentLevel >= 1)
                 .Subscribe(x =>
                 {
                     currentLevel = 2;
                     if (x._amount[1] != 0)
                         StartGame(x._amount[1], x._background[1], x._asteroidType[1]);
                     else
                         StartGame(Random.Range(15, 21), Random.Range(0, backgrounds.Length), Random.Range(0, asteroids.Length));

                 });
        var lvl3 = Observable.Merge(
                thirdLevel.OnClickAsObservable().Select(_ => data));
        lvl3.Where(x => x.CurrentLevel >= 2)
                 .Subscribe(x =>
                 {
                     currentLevel = 3;
                     if (x._amount[2] != 0)
                         StartGame(x._amount[2], x._background[2], x._asteroidType[2]);
                     else
                         StartGame(Random.Range(1, 2), Random.Range(0, backgrounds.Length), 4);

                 });
    }
    public void WaitForClick(GameObject panel)
    {
        clickEvent = Observable.EveryUpdate()
           .Where(_ => Input.anyKeyDown)
           .Subscribe(_ =>
           {
               Click();
           }).AddTo(panel);
    }
    public void Click()
    {
        view.OnKeyDown();
       clickEvent.Dispose();
    }
}
