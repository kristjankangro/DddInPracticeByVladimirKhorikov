﻿using DomainDrivenDesign.Logic.Utils;
using Logic;
using NHibernate;

namespace ConsoleApp.Common;
//unused
public class MainViewModel : ViewModel
{
    public MainViewModel()
    {
        SnackMachine snackMachine;
        using (ISession session = SessionFactory.OpenSession())
        {
            snackMachine = session.Get<SnackMachine>(1L);
        }
        var viewModel = new SnackMachineViewModel(snackMachine);
        _dialogService.ShowDialog(viewModel);
    }
}