using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCases : IUpdateExpenseUseCases
{
    private readonly IExpensesUpdateOnlyRepository _repository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    public UpdateExpenseUseCases(
        IExpensesUpdateOnlyRepository repository,
        IMapper mapper,
        IUnitOfWork unitOfWork
    )
    {
        _repository = repository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(RequestExpenseJson request, long id)
    {
        Validate(request);

        var expense = await _repository.GetById(id);

        if (expense == null )
        {
            throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);
        }

        _mapper.Map(request, expense);

        _repository.Update(expense);    
    
        await _unitOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if (!result.IsValid) {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
