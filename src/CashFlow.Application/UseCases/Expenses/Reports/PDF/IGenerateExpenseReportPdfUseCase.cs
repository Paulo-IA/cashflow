namespace CashFlow.Application.UseCases.Expenses.Reports.PDF;

public interface IGenerateExpenseReportPdfUseCase
{
    public Task<byte[]> Execute(DateTime month);
}
