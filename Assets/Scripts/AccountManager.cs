using Assets.Scripts.Models;
using Assets.Scripts.Models.Dto;
using Assets.Scripts.Services;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AccountManager : MonoBehaviour
{
    [Header("Popup")]
    public GameObject accountPopup;

    [Header("Tabs")]
    public GameObject loginPanel;
    public GameObject registerPanel;
    public Image loginTabImage;
    public Image registerTabImage;
    public Color actieveTabKleur = Color.white;
    public Color inactieveTabKleur = new Color(0.85f, 0.85f, 0.85f);

    [Header("Login velden")]
    public TMP_InputField loginEmail;
    public TMP_InputField loginWachtwoord;

    [Header("Register velden")]
    public TMP_InputField regKindNaam;
    public TMP_InputField regKindLeeftijd;
    public TMP_InputField regArtsNaam;
    public TMP_InputField regBehandelType;
    public TMP_InputField regBehandelDatum;
    public TMP_InputField regEmail;
    public TMP_InputField regWachtwoord;

    [Header("Feedback")]
    public TextMeshProUGUI feedbackText;

    public static Action OnLoginSuccess;

    private AccountService accountService;

    void Start()
    {
        accountPopup.SetActive(false);

        // Find or create AccountService
        accountService = FindAnyObjectByType<AccountService>();
        if (accountService == null)
        {
            GameObject accountGO = new GameObject("AccountService");
            accountService = accountGO.AddComponent<AccountService>();
        }
    }

    // AccountButton
    public void OpenPopup()
    {
        accountPopup.SetActive(true);
        ToonLoginTab();
    }

    public void SluitPopup()
    {
        accountPopup.SetActive(false);
    }

    public void ToonLoginTab()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        loginTabImage.color = actieveTabKleur;
        registerTabImage.color = inactieveTabKleur;
    }

    public void ToonRegisterTab()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
        loginTabImage.color = inactieveTabKleur;
        registerTabImage.color = actieveTabKleur;
    }

    public async void OnLoginGedrukt()
    {
        string email = loginEmail.text.Trim();
        string wachtwoord = loginWachtwoord.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(wachtwoord))
        {
            ToonFeedback("Vul alle velden in!");
            return;
        }

        ToonFeedback("Inloggen...");
        ApiResult result = await accountService.LoginAsync(email, wachtwoord);
        if (result.Ok)
        {
            ApiResult<UserDto> userResult = await accountService.GetCurrentUserAsync();
            if (userResult.Ok)
            {
                var user = userResult.Value;

                ToonFeedback($"Welkom terug, {user.Name}!");
                Debug.Log($"Login poging: {email}");

                PlayerPrefs.SetInt("CurrentLevel", Math.Max(user.CurrentLevel - 1, 0));
                PlayerPrefs.Save();

                OnLoginSuccess?.Invoke();
                return;
            }

            ToonFeedback("Er is iets fout gegaan...");
            Debug.Log($"Login poging: {email}. Ingelogd, maar kon User niet ophalen.");
            return;
        }

        ToonFeedback($"Vul de juiste gegevens in.");
    }

    public async void OnRegistrerenGedrukt()
    {
        string kindNaam = regKindNaam.text.Trim();
        string leeftijd = regKindLeeftijd.text.Trim();
        string artsNaam = regArtsNaam.text.Trim();
        string behandelType = regBehandelType.text.Trim();
        string email = regEmail.text.Trim();
        string wachtwoord = regWachtwoord.text;

        if (!DateTime.TryParse(regBehandelDatum.text.Trim(), out DateTime behandelDatumParsed))
        {
            ToonFeedback("Ongeldige datum!");
            return;
        }

        if (string.IsNullOrEmpty(kindNaam) || string.IsNullOrEmpty(leeftijd) ||
            string.IsNullOrEmpty(artsNaam) || string.IsNullOrEmpty(behandelType) ||
            string.IsNullOrEmpty(regBehandelDatum.text.Trim()) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(wachtwoord))
        {
            ToonFeedback("Vul alle velden in!");
            return;
        }

        var registerDto = new RegisterDto
        {
            Email = email,
            Password = wachtwoord,
            Name = kindNaam,
            Age = int.Parse(leeftijd),
            DoctorName = artsNaam,
            TreatmentDetails = behandelType,
            TreatmentDate = behandelDatumParsed.ToString("yyyy-MM-dd")
        };

        ToonFeedback("Registreren...");
        ApiResult result = await accountService.RegisterAsync(registerDto);
        if (result.Ok)
        {
            ToonFeedback($"Welkom, {kindNaam}! Account aangemaakt.");
            Debug.Log($"Registratie: {kindNaam}, {email}");

            // Na registratie naar login tab
            Invoke(nameof(ToonLoginTab), 2f);
            return;
        }

        Debug.LogError($"Registratie fout: {result.Error} {result.ErrorDetails?.ToString()}");
        ToonFeedback("Registratie mislukt, helaas.");
    }

    void ToonFeedback(string bericht)
    {
        if (feedbackText != null)
        {
            feedbackText.text = bericht;
            feedbackText.gameObject.SetActive(true);
        }
    }
}