using System;
using System.Collections;
using Agava.YandexGames;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ScoreStore : MonoBehaviour
{
    [SerializeField] private Score _score;

    private List<int> _bestKills = new List<int>();
    private List<int> _scores = new List<int>();
    private List<int> _breads = new List<int>();
    private List<bool> _isOpenLevels = new List<bool>();
    private bool _isSoundOn = true;

    private int _bestScore;//
    private int _currentSaveSlot;
    private int _finalScore;

    private string _nameSaveFile = "/PlayerStats.dat";
    private const string LeaderboardName = "Leaderboard";

    public int BestScore => _bestScore;
    public int FinalScore => _finalScore;

    private void Awake()
    {
        GetSaveData();
    }

    private void OnEnable()
    {
        if (_score != null)
        {
            _score.SavedData += ChangeSaveData;
        }
    }

    private IEnumerator Start()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        yield break;
#endif
        // Always wait for it if invoking something immediately in the first scene.
        yield return YandexGamesSdk.Initialize();
    }

    private void OnDisable()
    {
        if (_score != null)
        {
            _score.SavedData -= ChangeSaveData;
        }
    }

    private void CalculateFinalScore()
    {
        int finalScore = 0;

        for (int i = 0; i < _scores.Count; i++)
        {
            finalScore += _scores[i];
        }

        if (finalScore < _finalScore)
        {
            _finalScore = finalScore;

            if (Application.isEditor == false)
            {
                Leaderboard.SetScore(LeaderboardName, _finalScore);
            }
        }
    }

    public int GetFinalScore()
    {
        return _finalScore;
    }

    public int GetBestKillsScore(int saveSlot)
    {
        _currentSaveSlot = saveSlot;
        return _bestKills[saveSlot];
    }

    public bool GetSoundStatus()
    {
        return _isSoundOn;
    }

    public int GetScore(int saveSlot)
    {
        _currentSaveSlot = saveSlot;
        return _scores[saveSlot];
    }

    public int GetBreadCount(int saveSlot)
    {
        _currentSaveSlot = saveSlot;
        return _breads[saveSlot];
    }

    public bool GetLevelStatus(int saveSlot)
    {
        _currentSaveSlot = saveSlot;
        return _isOpenLevels[saveSlot];
    }

    private void GetSaveData()
    {
        if (File.Exists(Application.persistentDataPath + _nameSaveFile))
        {
            LoadGame();
        }
        else
        {
            ResetData();
        }
    }

    private void ResetData()
    {
        if (File.Exists(Application.persistentDataPath + _nameSaveFile))
        {
            File.Delete(Application.persistentDataPath + _nameSaveFile);
        }

        int maxLevel = 100;

        for (int i = 0; i < maxLevel; i++)
        {
            _bestKills.Add(0);
            _scores.Add(0);
            _breads.Add(0);

            if (i == 0)
            {
                _isOpenLevels.Add(true);
            }

            _isOpenLevels.Add(false);
        }

        SaveGame();
    }

    private void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + _nameSaveFile))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + _nameSaveFile, FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            _bestKills = new List<int>(data.GetBestKills());
            _scores = new List<int>(data.GetScores());
            _breads = new List<int>(data.GetBreads());
            _isOpenLevels = new List<bool>(data.GetLevelState());
            _finalScore = data.GetFinalScore();
            _isSoundOn = data.GetSoundStatus();
            _bestScore = data.BestScoreInStore;
        }
    }

    private void ChangeSaveData(int bestKills, int scores, int breads, bool isOpen)
    {
        if (bestKills > _bestKills[_currentSaveSlot])
        {
            _bestKills[_currentSaveSlot] = bestKills;
        }
        if (scores > _scores[_currentSaveSlot])
        {
            _scores[_currentSaveSlot] = scores;
        }
        if (breads > _breads[_currentSaveSlot])
        {
            _breads[_currentSaveSlot] = breads;
        }
        if (isOpen == true)
        {
            if (_currentSaveSlot < _isOpenLevels.Count)
            {
                _isOpenLevels[_currentSaveSlot + 1] = true;
            }
        }

        SaveGame();
    }

    public void ChangeSound(bool isSound)
    {
        _isSoundOn = isSound;
        SaveGame();
    }

    private void ChangeScore(int bestScore)
    {
        _bestScore = bestScore;
        SaveGame();
    }

    private void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + _nameSaveFile);
        SaveData data = new SaveData();
        CalculateFinalScore();

        data.AddData(_bestKills.AsReadOnly(), _scores.AsReadOnly(), _breads.AsReadOnly(), _isOpenLevels.AsReadOnly(), _finalScore, _isSoundOn);

        data.AddData(_bestScore);

        bf.Serialize(file, data);
        file.Close();
    }
}

[Serializable]
public class SaveData
{
    private List<int> _bestKillsInStore;
    private List<int> _scoresInStore;
    private List<int> _breadsInStore;
    private List<bool> _isOpenInStore;
    private bool _isSoundOnInStore;

    private int _finalScoreInStore;
    private int _bestScoreInStore = 0;

    public int BestScoreInStore => _bestScoreInStore;

    public ReadOnlyCollection<int> GetBestKills()
    {
        return _bestKillsInStore.AsReadOnly();
    }

    public int GetFinalScore()
    {
        return _finalScoreInStore;
    }

    public bool GetSoundStatus()
    {
        return _isSoundOnInStore;
    }

    public ReadOnlyCollection<int> GetScores()
    {
        return _scoresInStore.AsReadOnly();
    }

    public ReadOnlyCollection<int> GetBreads()
    {
        return _breadsInStore.AsReadOnly();
    }

    public ReadOnlyCollection<bool> GetLevelState()
    {
        return _isOpenInStore.AsReadOnly();
    }

    public void AddData(ReadOnlyCollection<int> bestKills, ReadOnlyCollection<int> _scores, ReadOnlyCollection<int> _breads, ReadOnlyCollection<bool> isOpen, int finalScore, bool isSoundOn)
    {
        _bestKillsInStore = new List<int>(bestKills);
        _scoresInStore = new List<int>(_scores);
        _breadsInStore = new List<int>(_breads);
        _isOpenInStore = new List<bool>(isOpen);
        _isSoundOnInStore = isSoundOn;

        if (CalculateFinalScoreInStore() <= finalScore)
        {
            _finalScoreInStore = finalScore;
        }
    }

    public int CalculateFinalScoreInStore()
    {
        _finalScoreInStore = 0;

        for (int i = 0; i < _scoresInStore.Count; i++)
        {
            _finalScoreInStore += _scoresInStore[i];
        }

        return _finalScoreInStore;
    }

    public void AddData(int bestScore)
    {
        _bestScoreInStore = bestScore;
    }
}
