﻿using Core.Interfaces.Services;
using Core.Interfaces.Services.PDF_Service;
using Core.Models;
using Core.Models.Finance;
using Core.Paginator.Parameters;

namespace Application.Services.FinancialStatement_PDF
{
    public class FinancialStatementPDfGenerator: IGenerateFullPDF<FinancialStatementParameters>
    {
        IFinancialService _financialService;
        ICreateTableForPDF<FinancialStatement> _createTable;
        IPDfGenerator _pDfGenerator;

        public FinancialStatementPDfGenerator(
            IFinancialService financialService,
            ICreateTableForPDF<FinancialStatement> createTable, 
            IPDfGenerator pDfGenerator)
        {
            _financialService = financialService;
            _createTable = createTable;
            _pDfGenerator = pDfGenerator;
        }
        
        public async Task<PdfFileModel> GeneratePDF(FinancialStatementParameters parameters)
        {
            var financialStatementList = await _financialService.GetFinancialStatement(parameters);
            var financialStatementTable = _createTable.CreateTable(financialStatementList);
            var pdfFileParams = await _pDfGenerator.CreatePDF(financialStatementTable);

            return pdfFileParams;
        }
    }
}
