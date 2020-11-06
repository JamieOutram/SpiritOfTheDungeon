using System.Collections;
using System.Collections.Generic;
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

    Base_UIPanel _currentPanel;
    public Base_UIPanel CurrentPanel { get { return _currentPanel; } }

    [SerializeField] private Base_UIPanel startPanel = default;
    
    //TransitionVariables
    [SerializeField] private GameObject blackoutImage = default;
    private Animator transitionAnim = default;
    private IEnumerator transitionCoroutine;
    private bool isCrRunning = false;

    private void Awake()
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
        TriggerOpenPanel(startPanel);
        
    }

    private void Update()
    {
        if (_currentPanel) _currentPanel.UpdateBehavior();
    }

    public void TriggerPanelTransition(Base_UIPanel panel, bool useFadeTransition = false)
    {
        if (useFadeTransition) {
            if (!isCrRunning)
            {
                transitionAnim.SetTrigger("Fade");
                transitionCoroutine = WaitTransitionComplete(panel);
                StartCoroutine(transitionCoroutine);
            }
        }
        else
        {
            TriggerOpenPanel(panel);
        }
    }

    public void TriggerBackPanelTransition()
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

    private IEnumerator WaitTransitionComplete(Base_UIPanel panel)
    {
        isCrRunning = true;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (transitionAnim.GetBool("isFaded")) break;
        }
        TriggerOpenPanel(panel);
        transitionAnim.SetTrigger("UnFade");
        isCrRunning = false;
    }

}
