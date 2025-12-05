using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Image _gaugeBoxesFill;
        
    [SerializeField] private TMP_Text _lifeText;
    [SerializeField] private Image _gaugeLifeFill;

    [SerializeField] private BoxesManager _boxManager;
    [SerializeField] private DamageTaker _playerDamageTaker;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        SetCounter(_boxManager.BoxesCount, _boxManager.MaxBoxes);
        SetLife(_playerDamageTaker.GetHP());
    }

    private void SetCounter(int count, int maxCount)
    {
        _text.text = count.ToString("D2") + " / " + maxCount.ToString("D2");
        _gaugeBoxesFill.fillAmount = (float)count / maxCount;
    }
    
    private void SetLife(int life)
    {
        _lifeText.text = life.ToString()+" / 100 HP";
        _gaugeLifeFill.fillAmount = (float)life / 100;
    }
}