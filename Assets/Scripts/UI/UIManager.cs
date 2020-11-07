using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public enum UIPanelId
{
    None,
    MainMenu,
    Settings,
    EscMenu,
    Fight,
    Training,
    TrainMenu,
    BattleSummary,
    MiniMap,
}

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private MainMenu_UIPanel mainMenuPanel = default;
    public MainMenu_UIPanel MainMenuPanel { get { return mainMenuPanel; } }

    [SerializeField] private Settings_UIPanel settingsPanel = default;
    public Settings_UIPanel SettingsPanel { get { return settingsPanel; } }

    [SerializeField] private EscMenu_UIPanel escMenuPanel = default;
    public EscMenu_UIPanel EscMenuPanel { get { return escMenuPanel; } }

    [SerializeField] private Fight_UIPanel fightPanel = default;
    public Fight_UIPanel FightPanel { get { return fightPanel; } }

    [SerializeField] private BattleSummary_UIPanel battleSummaryPanel = default;
    public BattleSummary_UIPanel BattleSummaryPanel { get { return battleSummaryPanel; } }

    [SerializeField] private Training_UIPanel trainingPanel = default;
    public Training_UIPanel TrainingPanel { get { return trainingPanel; } }

    [SerializeField] private Base_UIPanel trainMenuPanel = default;
    public Base_UIPanel TrainMenuPanel { get { return trainMenuPanel; } }

    //Panel Awake initialization calls
    private void InitializePanels()
    {
        if (MainMenuPanel != null) MainMenuPanel.AwakeBehavior();
        if (SettingsPanel!=null) SettingsPanel.AwakeBehavior();
        if (EscMenuPanel != null) EscMenuPanel.AwakeBehavior();
        if (FightPanel != null) FightPanel.AwakeBehavior();
        if (BattleSummaryPanel != null) BattleSummaryPanel.AwakeBehavior();
        if (TrainingPanel != null) TrainingPanel.AwakeBehavior();
        if (TrainMenuPanel != null) TrainMenuPanel.AwakeBehavior();
    }

    Base_UIPanel _hostPanel;
    public Base_UIPanel HostPanel { get { return _hostPanel; } }

    Base_UIPanel _currentPanel;
    public Base_UIPanel CurrentPanel { get { return _currentPanel; } }

    [SerializeField] private Base_UIPanel startPanel = default;

    //TransitionVariables
    [SerializeField] private GameObject blackoutImage = default;
    private Animator transitionAnim = default;
    private IEnumerator transitionCoroutine;
    private bool isCrRunning = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else { Destroy(gameObject); }

        transitionAnim = blackoutImage.GetComponent<Animator>();
    }

    
    private void Start()
    {
        InitializePanels();
        TriggerOpenPanel(startPanel);
    }

    private void Update()
    {
        if (_currentPanel) _currentPanel.UpdateBehavior();
    }

    //Triggers a transition to the specified panel or popup.
    //If a Popup is open, first closes  
    public void TriggerPanelTransition(Base_UIPanel panel, bool useFadeTransition = false)
    {
        if (_currentPanel.IsPopup)
        {
            //First closes popup
            OpenPanelOrPopup(_hostPanel);
        }

        if (_currentPanel != panel)
        {
            if (useFadeTransition)
            {
                //Blackout Fade transition
                if (!isCrRunning)
                {
                    transitionAnim.SetTrigger("Fade");
                    transitionCoroutine = WaitTransitionComplete(panel);
                    StartCoroutine(transitionCoroutine);
                }
            }
            else
            {
                OpenPanelOrPopup(panel);
            }
        }
        else
        {
            Debug.LogWarning("Attempted to open panel that's already open.");
        }
    }

    //TODO: For future back button behaviours
    public void TriggerPanelTransitionBack()
    {
        if (_currentPanel.IsPopup)
        {
            TriggerPanelTransition(_hostPanel);
        }
        else 
        {
            switch (_currentPanel.Id)
            {
                case UIPanelId.Settings:
                    break;
                case UIPanelId.Fight:
                    break;
                case UIPanelId.TrainMenu:
                    break;
                case UIPanelId.EscMenu:
                    break;
                default:
                    break;
            }
        }
    }

    //Checks Weather to use Popup or Panel related methods
    void OpenPanelOrPopup(Base_UIPanel panel)
    {
        if (_hostPanel == panel)
            TriggerClosePopup();
        else
        {
            if (panel.IsPopup)
            {
                TriggerOpenPopup(panel);
            }
            else
            {
                TriggerOpenPanel(panel);
            }
        }
    }

    //What happens when a panel is opened from another panel
    void TriggerOpenPanel(Base_UIPanel panel)
    {   
        if (_currentPanel != null) 
            TriggerClosePanel(_currentPanel);
        _currentPanel = panel;
        _currentPanel.OpenBehavior();
    }

    //What happens when a panel is closed
    void TriggerClosePanel(Base_UIPanel panel)
    {
        panel.CloseBehavior();
    }

    //What happens when a popup is opened from a panel
    void TriggerOpenPopup(Base_UIPanel popup)
    {
        _hostPanel = _currentPanel;
        _currentPanel = popup;
        _hostPanel.SetPanelInput(false);
        popup.OpenBehavior();
    }

    //What happens when a popup is closed
    void TriggerClosePopup()
    {
        if (_hostPanel == null)
        {
            Debug.LogException(new NullReferenceException("There is no Host panel set."));
        }
        _currentPanel.CloseBehavior(); //Close popup
        _hostPanel.SetPanelInput(true); //Reactivate host
        _currentPanel = _hostPanel;
        _hostPanel = null;
    }

    //Waits for the between scene transition to complete before loading next UI menu
    IEnumerator WaitTransitionComplete(Base_UIPanel panel)
    {
        isCrRunning = true;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (transitionAnim.GetBool("isFaded")) break;
        }
        OpenPanelOrPopup(panel);
        transitionAnim.SetTrigger("UnFade");
        isCrRunning = false;
    }

}
