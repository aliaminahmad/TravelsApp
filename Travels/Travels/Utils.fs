namespace Travels

open Android.App
open Android.Views
open Android.Widget
open Android.OS
open System
open OxyPlot.Series;
open Persistence;

module Utils =

    let GetPersonDataFromGUI (activity : Activity) = 
        let email = (activity.FindViewById<EditText>(Resource_Id.TextEmail)).Text
        let name = (activity.FindViewById<EditText>(Resource_Id.TextName)).Text 
        name,email

    let GetExpenseDataFromGUI (activity : Activity) =
        let amount = (activity.FindViewById<EditText>(Resource_Id.TextAmountCharge)).Text
        let personsTable = (activity.FindViewById<TableLayout>(Resource_Id.PersonTable))
        let persons = 
            List.init personsTable.ChildCount (fun i -> personsTable.GetChildAt i :?> Switch)
            |> List.filter (fun switch  -> switch.Checked) |> List.map (fun s  -> s.Text)
        amount, persons
