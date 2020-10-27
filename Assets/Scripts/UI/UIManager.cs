using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private Fight_UIPanel fightPanel = default;
    public Fight_UIPanel FightPanel { get { return fightPanel; } }

    [SerializeField] private Training_UIPanel trainingPanel = default;
    public Training_UIPanel TrainingPanel { get { return trainingPanel; } }
    
    [SerializeField] private Base_UIPanel trainMenuPanel = default;
    public Base_UIPanel TrainMenuPanel { get { return trainMenuPanel; } }

    Base_UIPanel _currentPanel;
    public Base_UIPanel CurrentPanel { get { return _currentPanel; } }

    [SerializeField] private Fight_UIPanel startPanel = default;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }
    }

    private void Start()
    {
        TriggerOpenPanel(startPanel);
        
    }

    private void Update()
    {
        if (_currentPanel) _currentPanel.UpdateBehavior();
    }

    public void TriggerPanelTransition(Base_UIPanel panel)
    {
        TriggerOpenPanel(panel);
    }

    void TriggerOpenPanel(Base_UIPanel panel)
    {
        if (_currentPanel != null) TriggerClosePanel(_currentPanel);
        _currentPanel = panel;
        _currentPanel.OpenBehavior();

    }

    void TriggerClosePanel(Base_UIPanel panel)
    {
        panel.CloseBehavior();
    }

}
