using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    void Start()
    {
        accountPopup.SetActive(false);
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

    public void OnLoginGedrukt()
    {
        string email = loginEmail.text.Trim();
        string wachtwoord = loginWachtwoord.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(wachtwoord))
        {
            ToonFeedback("Vul alle velden in!");
            return;
        }

        // Sla op in PlayerPrefs zodat backend later kan koppelen
        PlayerPrefs.SetString("LoginEmail", email);
        PlayerPrefs.Save();

        ToonFeedback("Inloggen... (backend wordt later gekoppeld)");
        Debug.Log($"Login poging: {email}");
    }

    public void OnRegistrerenGedrukt()
    {
        string kindNaam = regKindNaam.text.Trim();
        string leeftijd = regKindLeeftijd.text.Trim();
        string artsNaam = regArtsNaam.text.Trim();
        string behandelType = regBehandelType.text.Trim();
        string behandelDatum = regBehandelDatum.text.Trim();
        string email = regEmail.text.Trim();
        string wachtwoord = regWachtwoord.text;

        if (string.IsNullOrEmpty(kindNaam) || string.IsNullOrEmpty(leeftijd) ||
            string.IsNullOrEmpty(artsNaam) || string.IsNullOrEmpty(behandelType) ||
            string.IsNullOrEmpty(behandelDatum) || string.IsNullOrEmpty(email) ||
            string.IsNullOrEmpty(wachtwoord))
        {
            ToonFeedback("Vul alle velden in!");
            return;
        }

        // Sla gegevens op in PlayerPrefs zodat backend later kan koppelen
        PlayerPrefs.SetString("KindNaam", kindNaam);
        PlayerPrefs.SetInt("KindLeeftijd", int.Parse(leeftijd));
        PlayerPrefs.SetString("ArtsNaam", artsNaam);
        PlayerPrefs.SetString("BehandelType", behandelType);
        PlayerPrefs.SetString("BehandelDatum", behandelDatum);
        PlayerPrefs.SetString("RegisterEmail", email);
        PlayerPrefs.Save();

        ToonFeedback($"Welkom, {kindNaam}! Account aangemaakt.");
        Debug.Log($"Registratie: {kindNaam}, {email}");

        // Na registratie naar login tab
        Invoke(nameof(ToonLoginTab), 2f);
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