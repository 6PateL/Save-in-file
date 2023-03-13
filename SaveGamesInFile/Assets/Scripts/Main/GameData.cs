using System;
using UnityEngine;


[Serializable] public class GameData
{
  public int speed;
  public Vector3 position;
  public Quaternion rotation;

  public GameData() {
    speed = 10;
    position = Vector3.up;
    rotation = Quaternion.identity; 
  }
}
