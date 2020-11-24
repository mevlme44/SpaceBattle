using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteManager
{
    private readonly static GameManager manager = Camera.main.gameObject.GetComponent<GameManager>();
    public static GameManager GetManager()
    {
        return manager;
    }

}
