﻿using Logic;
using NHibernate;

namespace ConsoleApp.Common;

public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        // SnackMachine snackMachine;
        // using (ISession session = SessionFactory.OpenSession())
        // {
        //     snackMachine = session.Get<SnackMachine>(1L);
        // }
        var viewModel = new SnackMachineViewModel(new SnackMachine());
        _dialogService.ShowDialog(viewModel);
    }
}