﻿using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Update;
public interface IUpdateExpenseUseCases
{
    Task Execute(RequestExpenseJson expense, long id);
}
