using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlazorParcAutomobile.ApplicationStates
{
    public class AllState
    {
        public Action? Action { get; set; }
        public bool ShowVoitures { get; set; }

        public void VoituresClicked()
        {
            ResetAllVoitures();
            ShowVoitures = true;
            Action?.Invoke();
        }
        public bool ShowUtilisateurs { get; set; }

        public void UtilisateursClicked()
        {
            ResetAllVoitures();
            ShowUtilisateurs = true;
            Action?.Invoke();
        }
        public bool ShowTrajets { get; set; }

        public void TrajetsClicked()
        {
            ResetAllVoitures();
            ShowTrajets = true;
            Action?.Invoke();
        }

        public bool ShowFournisseurs { get; set; }

        public void FournisseursClicked()
        {
            ResetAllVoitures();
            ShowFournisseurs = true;
            Action?.Invoke();
        }

        

        public bool ShowEntretiens { get; set; }

        public void EntretiensClicked()
        {
            ResetAllVoitures();
            ShowEntretiens = true;
            Action?.Invoke();
        }

        public bool ShowDocumentAdministratifs { get; set; }

        public void DocumentAdministratifsClicked()
        {
            ResetAllVoitures();
            ShowDocumentAdministratifs = true;
            Action?.Invoke();
        }
        public bool ShowAffectations { get; set; }

        public void AffectationsClicked()
        {
            ResetAllVoitures(); 
            ShowAffectations = true;
            Action?.Invoke();
        }


        public bool ShowAssurances { get; set; }

        public void AssurancesClicked()
        {
            ResetAllVoitures();
            ShowAssurances = true;
            Action?.Invoke();
        }
        public bool ShowTaxes { get; set; }

        public void TaxesClicked()
        {
            ResetAllVoitures();
            ShowTaxes = true;
            Action?.Invoke();
        }
        public bool ShowVisites { get; set; }

        public void VisitesClicked()
        {
            ResetAllVoitures();
            ShowVisites = true;
            Action?.Invoke();
        }
        public bool ShowHome { get; set; } = true;

        public void HomeClicked()
        {
            ResetAllVoitures();
            ShowHome = true;
            Action?.Invoke();
        }
        public bool ShowAlerte { get; set; }

        public void AlertesClicked()
        {
            ResetAllVoitures();
            ShowAlerte = true;
            Action?.Invoke();
        }



        public bool ShowRapport { get; set; }

        public void RapportClicked()
        {
            ResetAllVoitures();
            ShowRapport = true;
            Action?.Invoke();
        }



        private void ResetAllVoitures()
        {
            ShowVoitures = false;
            ShowUtilisateurs = false;
            ShowAffectations = false;
            ShowDocumentAdministratifs = false;
            ShowEntretiens = false;
            ShowFournisseurs = false;
            ShowTrajets = false;
            ShowAssurances = false;
            ShowVisites = false;
            ShowHome = false;
            ShowTaxes = false;
            ShowAlerte = false;
            ShowRapport = false;


        }
    }
}
