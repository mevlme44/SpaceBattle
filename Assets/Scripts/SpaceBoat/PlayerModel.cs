using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerModel 
{
    private struct Boundary
    {
        public float xMin, xMax, yMin, yMax;
    }

    public delegate void Death();
    public event Death onDeath;


    public int healthPoint = 3;
    public int score = 0;
    private readonly float tilt = 4.0f;
    public float Speed { get; set; }

    public Vector2 Position { 
        get
        {
            return _Position; 
        }
        set
        {
            _Position.x = value.x;
            _Position.y = value.y;
        }
    }
    private Vector2 _Position;
    public Quaternion Rotation
    {
        get
        {
            return _Rotation;
        }
        set
        {
            _Rotation.x = value.x;
            _Rotation.y = value.y;
            _Rotation.z = value.z;
            _Rotation.w = value.w;
        }
    }
    private Quaternion _Rotation;
    private PlayerView view;

    private Boundary bound = new Boundary();



    public void SetBound(float xMin,float xMax, float yMin, float yMax)
    {
        bound.xMax = xMax;
        bound.xMin = xMin;
        bound.yMax = yMax;
        bound.yMin = yMin;


    }
    public void SetView(PlayerView view)
    {
        this.view = view;
    }
    public void CalculatePosition(float x, float y)
    {
        Vector2 movement = new Vector3(x, y);
        Vector2 velocity = movement * Speed;
        view.UpdateVelocity(velocity);
        Position = new Vector2
        (
            Mathf.Clamp (Position.x, bound.xMin, bound.xMax), 
            Mathf.Clamp (Position.y, bound.yMin, bound.yMax)
        );
        view.UpdatePosition(Position);
        Rotation = Quaternion.Euler(0.0f, velocity.x * -tilt, 0.0f) ;
        view.UpdateRotation(Rotation);
    }
    public void Fire(GameObject shot,Transform shotPoint)
    {
        view.InstanceShot(shot, shotPoint);
    }
    public void GetDamage()
    {
        healthPoint--;
        view.ChangeHealthPoint(healthPoint);
        if(healthPoint <= 0)
        {
            Lose();
        }
    }
    public void KilledAsteroid()
    {
        score++;
        view.ChangeScore(score);
    }
    public void Win()
    {
        view.Win();
        Position = new Vector2(0.0f, 0.0f);
        view.UpdatePosition(Position);
        view.UpdateVelocity(new Vector2(0.0f, 0.0f));
        healthPoint = 3;
        view.ChangeHealthPoint(healthPoint);
        score = 0;
        view.ChangeScore(score);
    }
    private void Lose()
    {
        onDeath();
        view.Lose();
        Position = new Vector2(0.0f, 0.0f);
        view.UpdatePosition(Position);
        view.UpdateVelocity(new Vector2(0.0f, 0.0f));
        healthPoint = 3;
        view.ChangeHealthPoint(healthPoint);
        score = 0;
        view.ChangeScore(score);
    }

}