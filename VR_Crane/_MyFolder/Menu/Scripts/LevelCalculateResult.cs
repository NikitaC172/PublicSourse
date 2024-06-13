using DaVanciInk.AdvancedPlayerPrefs;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CraneGame
{
    public class LevelCalculateResult : MonoBehaviour
    {
        [SerializeField] private List<Cargo> _cargos;
        [SerializeField] private TMP_Text _textResult;
        [SerializeField] private float _scoreForOneStar;
        [SerializeField] private float _scoreForTwoStar;
        [SerializeField] private float _scoreForThreeStar;
        [SerializeField] private List<Image> _stars;
        [SerializeField] private string _nameSave;
        [SerializeField] private Color _color = Color.yellow;

        private int _score;
        private int _starsOpen;

        private void OnEnable()
        {
            CalculateResult();
            RenderScore();
            RenderStars();
            SaveData();
        }

        private void CalculateResult()
        {
            _score = 0;

            foreach (var cargo in _cargos)
            {
                _score += cargo.GetHealth();
            }

            _score /= _cargos.Count;

            if (_score >= _scoreForThreeStar)
            {
                _starsOpen = 3;
            }
            else if (_score >= _scoreForTwoStar)
            {
                _starsOpen = 2;
            }
            else if (_score >= _scoreForOneStar)
            {
                _starsOpen = 1;
            }
            else
            {
                _starsOpen = 0;
            }
        }

        private void RenderStars()
        {
            if (_starsOpen == 0)
            {
                return;
            }
            else
            {
                for (int i = 0; i < _starsOpen; i++)
                {
                    _stars[i].color = _color;
                }
            }
        }

        private void RenderScore()
        {
            _textResult.text = "Score: " + _score.ToString() + "%";
        }

        private void SaveData()
        {
            if (AdvancedPlayerPrefs.GetInt(_nameSave) < _starsOpen)
            {
                AdvancedPlayerPrefs.SetInt(_nameSave, _starsOpen);
            }
        }
    }
}
