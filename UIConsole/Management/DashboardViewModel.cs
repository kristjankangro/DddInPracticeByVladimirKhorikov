using ConsoleApp.Atms;
using ConsoleApp.Common;
using DomainDrivenDesign.Logic.Atms;
using DomainDrivenDesign.Logic.Management;
using DomainDrivenDesign.Logic.SnackMachines;
using Logic;
using Logic.Atms;

namespace ConsoleApp.Management
{
    public class DashboardViewModel : ViewModel
    {
        private readonly SnackMachineRepo _snackMachineRepo;
        private readonly AtmRepo _atmRepo;
        private readonly HeadOfficeRepo _headOfficeRepo;

        public HeadOffice HeadOffice { get; }
        public IReadOnlyList<SnackMachineDto> SnackMachines { get; private set; }
        public IReadOnlyList<AtmDto> Atms { get; private set; }
        public Command<SnackMachineDto> ShowSnackMachineCommand { get; private set; }
        public Command<SnackMachineDto> UnloadCashCommand { get; private set; }
        public Command<AtmDto> ShowAtmCommand { get; private set; }
        public Command<AtmDto> LoadCashToAtmCommand { get; private set; }

        public DashboardViewModel()
        {
            HeadOffice = HeadOfficeInstance.Instance;
            _snackMachineRepo = new SnackMachineRepo();
            _atmRepo = new AtmRepo();
            _headOfficeRepo = new HeadOfficeRepo();

            RefreshAll();

            ShowSnackMachineCommand = new Command<SnackMachineDto>(x => x != null, ShowSnackMachine);
            UnloadCashCommand = new Command<SnackMachineDto>(CanUnloadCash, UnloadCash);
            ShowAtmCommand = new Command<AtmDto>(x => x != null, ShowAtm);
            LoadCashToAtmCommand = new Command<AtmDto>(CanLoadCashToAtm, LoadCashToAtm);
        }

        private bool CanLoadCashToAtm(AtmDto atmDto)
        {
            return atmDto != null && HeadOffice.Cash.Amount > 0;
        }

        private void LoadCashToAtm(AtmDto atmDto)
        {
            Atm atm = _atmRepo.GetById(atmDto.Id);

            if (atm == null)
                return;

            HeadOffice.LoadCashToAtm(atm);
            _atmRepo.Save(atm);
            _headOfficeRepo.Save(HeadOffice);

            RefreshAll();
        }

        private void ShowAtm(AtmDto atmDto)
        {
            Atm atm = _atmRepo.GetById(atmDto.Id);

            if (atm == null)
                return;

            _dialogService.ShowDialog(new AtmViewModel(atm));
            RefreshAll();
        }

        private bool CanUnloadCash(SnackMachineDto snackMachineDto)
        {
            return snackMachineDto != null && snackMachineDto.MoneyInside > 0;
        }

        private void UnloadCash(SnackMachineDto snackMachineDto)
        {
            SnackMachine snackMachine = _snackMachineRepo.GetById(snackMachineDto.Id);

            if (snackMachine == null)
                return;

            HeadOffice.UnloadCashFromSnackMachine(snackMachine);
            _snackMachineRepo.Save(snackMachine);
            _headOfficeRepo.Save(HeadOffice);

            RefreshAll();
        }

        private void ShowSnackMachine(SnackMachineDto snackMachineDto)
        {
            SnackMachine snackMachine = _snackMachineRepo.GetById(snackMachineDto.Id);

            if (snackMachine == null)
            {
                // MessageBox.Show("Snack machine was not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _dialogService.ShowDialog(new SnackMachineViewModel(snackMachine));
            RefreshAll();
        }

        private void RefreshAll()
        {
            SnackMachines = _snackMachineRepo.GetSnackMachineList();
            Atms = _atmRepo.GetAtmList();

            Notify(nameof(Atms));
            Notify(nameof(SnackMachines));
            Notify(nameof(HeadOffice));
        }
    }
}
