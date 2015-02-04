namespace Travels

open Android.App
open Android.Views
open Android.Widget
open Android.OS

type SampleTabFragment() = 
    inherit Fragment() 
           
    override this.OnCreateView (inflater, container, savedInstanceState) =
        base.OnCreateView (inflater, container, savedInstanceState) |> ignore
        let view : View = inflater.Inflate (Resource_Layout.Main, container, false);
        let sampleTextView = view.FindViewById<TextView> (Resource_Id.TextView);            
        sampleTextView.Text <- "sample fragment text";
        view

type SampleTabFragment2() = 
    inherit Fragment() 
           
    override this.OnCreateView (inflater, container , savedInstanceState) =
        base.OnCreateView (inflater, container, savedInstanceState) |> ignore
        let view : View = inflater.Inflate (Resource_Layout.Main, container, false);
        //let sampleTextView = view.FindViewById<LinearLayout> (Resource_Id.linearView);            
        // sampleTextViewText <- "sample fragment text";
        view