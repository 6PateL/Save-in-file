using System;
using UnityEngine;

public class Example : MonoBehaviour
{
  public GameObject cube;
  private Storage _storage;
  private GameData _gameData;

  private void Start() {
    _storage = new Storage();
    Load();
  }

  private void Update() {
    if (Input.GetKeyDown(KeyCode.Alpha1)) {
      Save();
    }
    if (Input.GetKeyDown(KeyCode.Alpha2)) {
     Load();
    }
  }

  private void Save() {
    _storage.Save(_gameData);
    Debug.Log("Data saved");
  }
  private void Load() {
    _gameData = (GameData)SerializeLoad.Load(new GameData());

    cube.transform.position = _gameData.position;
    cube.transform.rotation = _gameData.rotation;
    Debug.Log($"Loaded speed = {_gameData.speed}");
  }
}