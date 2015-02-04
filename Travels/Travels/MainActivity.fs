namespace Travels

open System
open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open Android.Graphics
open OxyPlot
open OxyPlot.XamarinAndroid
open OxyPlot.Series
open Persistence
open Services
open Utils

type ChartTab(activity : Activity) =
    inherit PlotView(activity)

    member this.PlotModel slices = 
        let m = new PlotModel()
        let ps = new PieSeries()
        // 
        slices |> List.iter ps.Slices.Add
        // view config.
        ps.InnerDiameter <- 0.0
        // this is the parameter that controlls the size.
        ps.ExplodedDistance <- 2.0
        ps.Stroke <- OxyColors.Black
        ps.StrokeThickness <- 4.0
        ps.AngleSpan <- 360.0
        ps.StartAngle <- 0.0
        m.Series.Add(ps);
        this.Model <- m

                 
[<Activity (Label = "Travels", MainLauncher = true)>]
type MainActivity () =
    inherit Activity ()

    let mutable model = { Persons = []; Expenses=[] }

    let SetModel db = 
        model <- db

    let PopulatePaymentTabWithSwitches (activity: Activity) (persons : List<Person>) =
        let ctx : TableLayout = activity.FindViewById<TableLayout> Resource_Id.PersonTable 
        persons |> List.map (fun (p : Person) -> p.Name) |> List.iter(fun name -> 
            let switch = new Switch(ctx.Context)
            switch.Text <- name
            ctx.AddView switch)

    let HandleAddPersonButton (activity : Activity) (model : TravelDb) (eventArgs: View.TouchEventArgs ) =
        match (eventArgs.Event.Action) with
        | MotionEventActions.Up ->
            let name,email = GetPersonDataFromGUI activity   
            SetModel (AddPerson model name email)
            // PopulatePaymentTabWithSwitches activity model.Persons |> ignore
            // activity.SetContentView Resource_Layout.AddPerson
            Toast.MakeText(activity, name + " Added", ToastLength.Short).Show()
        | MotionEventActions.Cancel -> ()
        | MotionEventActions.Down -> ()
        | MotionEventActions.Move -> ()
        | _ -> ()
           
    let HandleAddExpenseButton (activity : Activity) (model : TravelDb) (eventArgs: View.TouchEventArgs ) =
        match (eventArgs.Event.Action) with
        | MotionEventActions.Up ->
            let amount, persons = GetExpenseDataFromGUI activity
            SetModel (AddExpense amount persons model)
            Toast.MakeText(activity, "Expense Added", ToastLength.Short).Show()
        | _ -> ()

    let CreateTab (activity : Activity) (iconResourceId : int) (tabHandler : ActionBar.TabEventArgs -> unit) = 
        let tab = activity.ActionBar.NewTab()
        tab.SetIcon iconResourceId |> ignore
        tab.TabSelected.Add tabHandler
        activity.ActionBar.AddTab tab
        activity 

    let OnPersonTabSelected (activity : Activity) = fun _ -> 
        activity.SetContentView(Resource_Layout.AddPerson)
        let addButton : Button = activity.FindViewById<Button>(Resource_Id.AddPersonButton)
        addButton.Touch.Add(fun e -> HandleAddPersonButton activity model e)

    let OnPaymentTabSelected (activity : Activity) = fun _ ->
        activity.SetContentView(Resource_Layout.AddCharge)
        let button : Button = activity.FindViewById<Button>(Resource_Id.AddExpenseButton)
        button.Touch.Add(fun e -> HandleAddExpenseButton activity model e)
        PopulatePaymentTabWithSwitches activity model.Persons

    let ChartTabHandler (activity : Activity) = fun _ -> 
        let chart = new ChartTab(activity)
        let m = CreatePieSlicesFromModel model
        chart.PlotModel m |> ignore
        activity.SetContentView(chart)

    override this.OnCreate (bundle) =
        base.OnCreate (bundle)
        // Change navigation to tab mode.
        this.ActionBar.NavigationMode <- ActionBarNavigationMode.Tabs 
        this.SetContentView (Resource_Layout.AddPerson)

        let this = CreateTab this Resource_Drawable.people (OnPersonTabSelected this)
        let this = CreateTab this Resource_Drawable.money (OnPaymentTabSelected this)
        let this = CreateTab this Resource_Drawable.chart (ChartTabHandler this)
        0 |> ignore