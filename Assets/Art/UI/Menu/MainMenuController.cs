using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private UIDocument uiDocument;

    private VisualElement root;
    private VisualElement logoContainer;
    private Label version;

    private readonly List<Button> menuButtons = new();
    private int currentIndex;
    private bool inputEnabled;

    private void Awake()
    {
        if (uiDocument == null) uiDocument = GetComponent<UIDocument>();
        
        if (uiDocument == null)
        {
            Debug.LogError("UIDocument is missing!");
            return;
        }

        root = uiDocument.rootVisualElement;
        CacheUI();
        RegisterEvents();
    }

    private void Start()
    {
        currentIndex = 0;
        PlayIntro();
    }

    private void CacheUI()
    {
        logoContainer = root.Q<VisualElement>("logo-container");
        version = root.Q<Label>("version");

        // بنجيب الزرار ونضيفهم بنشوفهم موجودين ولا لا
        Button playBtn = root.Q<Button>("btn-play");
        Button continueBtn = root.Q<Button>("btn-continue");
        Button settingsBtn = root.Q<Button>("btn-settings");
        Button exitBtn = root.Q<Button>("btn-exit");

        if (playBtn != null) menuButtons.Add(playBtn);
        if (continueBtn != null) menuButtons.Add(continueBtn);
        if (settingsBtn != null) menuButtons.Add(settingsBtn);
        if (exitBtn != null) menuButtons.Add(exitBtn);
    }

    private void RegisterEvents()
    {
        // ربط الكليكات بأمان
        if (menuButtons.Count > 0) menuButtons[0].clicked += OnPlay;
        if (menuButtons.Count > 1) menuButtons[1].clicked += OnContinue;
        if (menuButtons.Count > 2) menuButtons[2].clicked += OnSettings;
        if (menuButtons.Count > 3) menuButtons[3].clicked += OnExit;

        // ربط الماوس والكيبورد بكل زرار
        foreach (Button button in menuButtons)
        {
            button.RegisterCallback<MouseEnterEvent>(_ =>
            {
                if (inputEnabled) SelectButton(menuButtons.IndexOf(button));
            });

            button.RegisterCallback<FocusInEvent>(_ =>
            {
                if (inputEnabled) SelectButton(menuButtons.IndexOf(button));
            });
        }

        // التعامل مع أسهم الكيبورد
        root.RegisterCallback<KeyDownEvent>(evt =>
        {
            if (!inputEnabled) return;

            if (evt.keyCode == KeyCode.DownArrow)
            {
                SelectButton((currentIndex + 1) % menuButtons.Count);
                evt.StopPropagation();
            }
            else if (evt.keyCode == KeyCode.UpArrow)
            {
                int newIndex = currentIndex - 1;
                if (newIndex < 0) newIndex = menuButtons.Count - 1;
                SelectButton(newIndex);
                evt.StopPropagation();
            }
        });
    }

    #region Intro Animation

    private void PlayIntro()
    {
        inputEnabled = false;

        logoContainer.style.opacity = 0;
        version.style.opacity = 0;
        
        foreach (var button in menuButtons)
        {
            button.style.opacity = 0;
            button.SetEnabled(false); // << مهم جداً: عطل الزرار وهي مخفية
        }

        root.schedule.Execute(() =>
        {
            logoContainer.style.opacity = 1;
        }).StartingIn(300);

        for (int i = 0; i < menuButtons.Count; i++)
        {
            int index = i;
            root.schedule.Execute(() =>
            {
                menuButtons[index].style.opacity = 1;
            }).StartingIn(700 + index * 150);
        }

        root.schedule.Execute(() =>
        {
            version.style.opacity = 1;
            inputEnabled = true;
            
            // شغل الزرار تاني عشان تتدوس
            foreach (var button in menuButtons)
            {
                button.SetEnabled(true);
            }

            if (menuButtons.Count > 0) menuButtons[0].Focus(); 
        }).StartingIn(1500);
    }

    #endregion

    #region Selection

    private void SelectButton(int index)
    {
        currentIndex = index;
        if (menuButtons[currentIndex] != null)
            menuButtons[currentIndex].Focus();
    }

    #endregion

        #region Button Actions

    private void OnPlay()
    {
        Debug.Log("<color=green>Play Clicked!</color>");
        
        // إخفاء المينو كله
        gameObject.SetActive(false);

        // لو حابب تتحمل سين تاني في المستقبل، هتكتب الكود ده:
        // UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    private void OnContinue()
    {
        Debug.Log("<color=blue>Continue Clicked!</color>");
    }

    private void OnSettings()
    {
        Debug.Log("<color=yellow>Settings Clicked!</color>");
    }

    private void OnExit()
    {
        Debug.Log("<color=red>Exit Clicked!</color>");
        Application.Quit();
    }

    #endregion
}