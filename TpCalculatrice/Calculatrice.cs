using System;
using System.Collections.Generic;

namespace TpCalculatrice;

public class Calculatrice
{
    // Les événements publiés : la fenêtre s'y abonnera
    public event Action<string> AffichageChange;
    public event Action<string> Erreur;

    private Dictionary<string, Func<decimal, decimal, decimal>> operations =
        new Dictionary<string, Func<decimal, decimal, decimal>>();

    private string  saisie = "";
    private decimal memoire;
    private string  operateur = "";

    public Calculatrice()
    {
        operations.Add("+", (a, b) => a + b);
        operations.Add("-", (a, b) => a - b);
        operations.Add("*", (a, b) => a * b);
        operations.Add("/", (a, b) => a / b);
    }

    public void AppuyerChiffre(string chiffre)
    {
        saisie += chiffre;
        AffichageChange?.Invoke(saisie);
    }

    public void AppuyerOperateur(string op)
    {
        if (saisie == "") return;
        memoire = decimal.Parse(saisie);
        operateur = op;
        saisie = "";
        AffichageChange?.Invoke($"{memoire} {op}");
    }

    public void AppuyerEgal()
    {
        if (operateur == "" || saisie == "") return;
        decimal b = decimal.Parse(saisie);

        try
        {
            decimal resultat = operations[operateur](memoire, b);
            saisie = resultat.ToString("0.######");
            operateur = "";
            AffichageChange?.Invoke(saisie);
        }
        catch (DivideByZeroException)
        {
            Effacer();
            Erreur?.Invoke("Erreur : division par zéro");
        }
    }

    public void Effacer()
    {
        saisie = "";
        memoire = 0;
        operateur = "";
        AffichageChange?.Invoke("0");
    }
}