using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private PlayerView view;
    private readonly PlayerModel model = new PlayerModel();
    private Vector2 position;
    private Quaternion rotation;
    private float nextFire;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private Transform shotSpawn;
    [SerializeField]
    private GameObject shot;
    public void FixedUpdate()
    {
        GetInput();
    }
    public void Start()
    {
        float x, y;
        x = CameraOptimize.ComputeResolution().Item1;
        y = CameraOptimize.ComputeResolution().Item2;
        model.SetBound(-x/2 + 0.8f, x/2 - 0.8f, -y/2 + 0.8f, y/2 - 0.8f);
        model.Speed = 10f;
        model.SetView(view);
    }
    public void Damage()
    {
        model.GetDamage();
    }
    public void Win()
    {
        model.Win();
    }
    public void KillAsteroid()
    {
        model.KilledAsteroid();
    }
    public PlayerModel GetModel()
    {
        return model;
    }
    private void GetInput()
    {
        position = view.GetPosition();
        rotation = view.GetRotation();
        model.Position = position;
        model.Rotation = rotation;
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        model.CalculatePosition(x, y);
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            model.Fire(shot, shotSpawn);
        }
    }

}
