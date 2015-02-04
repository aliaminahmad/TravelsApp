module Persistence

open System

    type Person = 
        {
            Name : string
            Email : string
        }

    type Expense =
        {
            Amount : int
            Persons : String list
        }

    type TravelDb = 
        {
            Persons: Person list
            Expenses : Expense list
        }

    let AddPerson database name email = 
        let person = { Email = email; Name = name }
        {
            Persons = database.Persons @ [person];
            Expenses = database.Expenses
        }

    let AddExpense amount persons database = 
        let expense = { Amount = System.Int32.Parse amount; Persons = persons }
        {
            Persons = database.Persons;
            Expenses = database.Expenses @ [expense]
        }

