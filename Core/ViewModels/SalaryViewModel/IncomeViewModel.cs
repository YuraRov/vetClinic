﻿using Core.ViewModels.ProcedureViewModels;

namespace Core.ViewModels.SalaryViewModel
{
    public class IncomeViewModel
    {
        public int AppointmenId { get; set; }
        public IEnumerable<ProcedureReadViewModel> ListOfProcedures = new List<ProcedureReadViewModel>();
        public decimal Cost { get; set; }
    }
}
