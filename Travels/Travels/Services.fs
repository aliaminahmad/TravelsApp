namespace Travels

open Android.App
open Android.Views
open Android.Widget
open Android.OS
open System
open OxyPlot.Series
open Persistence

module Services =
    open Persistence
    type AddPersonEventArgs =
        {
            Name: string;
            Email : string
        }
    type AddExpenseEventArgs = 
        {
            Amount : string
            Participants : string list
        }

    let CreatePieSlicesFromModel database  =
            let pieSlices = database.Persons |> List.map( fun (p : Person) -> new PieSlice(p.Name, 0.0))
            database.Expenses |> List.iter (fun e -> 
                let ExpenseForPerson = (float e.Amount) / float (e.Persons.Length)
                e.Persons 
                |> List.iter ( fun personNameForExpense -> 
                    let pieSlice = pieSlices |> List.find( fun pieSliceName -> pieSliceName.Label = personNameForExpense)
                    pieSlice.Value <- pieSlice.Value + ExpenseForPerson
                    )
                )
            pieSlices
