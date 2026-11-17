using Avalonia.Controls;
using Avalonia.Interactivity;

namespace TpCalculatrice;

public partial class MainWindow : Window
{
    private Calculatrice calc = new Calculatrice();

    public MainWindow()
    {
        InitializeComponent();

        // 1. La fenêtre s'abonne aux événements de la calculatrice
        calc.AffichageChange += MettreAJourEcran;
        calc.Erreur += MettreAJourEcran;

        // 2. Les 16 boutons partagent le même gestionnaire de clic
        foreach (Control controle in Touches.Children)
        {
            if (controle is Button bouton)
                bouton.Click += SurClic;
        }
    }

    // Gestionnaire du clic : sender indique QUEL bouton a été cliqué
    private void SurClic(object sender, RoutedEventArgs e)
    {
        Button bouton = (Button)sender;
        string touche = (string)bouton.Content;

        if (touche == "C") calc.Effacer();
        else if (touche == "=") calc.AppuyerEgal();
        else if ("+-*/".Contains(touche)) calc.AppuyerOperateur(touche);
        else calc.AppuyerChiffre(touche);
    }

    // Abonné aux deux événements de la calculatrice : forme Action<string>
    private void MettreAJourEcran(string texte)
    {
        Ecran.Text = texte;
    }
}