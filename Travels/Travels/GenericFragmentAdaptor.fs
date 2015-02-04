namespace Travels
{

open System
open System.Collections.Generic
open System.Linq
open System.Text
open Android.App
open Android.OS
open Android.Views
open Android.Support.V4.View
open Android.Support.V4.App

type GenericFragmentPagerAdaptor() =
    inherit FragmentPagerAdapter()
    let List _fragmentList

    public GenericFragmentPagerAdaptor(Android.Support.V4.App.FragmentManager fm)
        : base(fm) {}

    override this.Count
        _fragmentList.Count; 

    override this.GetItem (position) =
        _fragmentList[position];
        
    let AddFragment (GenericViewPagerFragment fragment) =
        _fragmentList.Add(fragment);
        unit
        
    let AddFragmentView (Func<LayoutInflater, ViewGroup, Bundle, View> view) =
        _fragmentList.Add(new GenericViewPagerFragment(view));
        
    
type ViewPageListenerForActionBar() =
    inherit ViewPager.SimpleOnPageChangeListener()
    {
        let  ActionBar _bar;

        ViewPageListenerForActionBar(ActionBar bar)
            _bar = bar;
        
        override OnPageSelected (position) =
            _bar.SetSelectedNavigationItem(position);
            unit
        
    }
    public static class ViewPagerExtensions
    {
        public static ActionBar.Tab GetViewPageTab(this ViewPager viewPager, ActionBar actionBar, string name)
        {
            var tab = actionBar.NewTab();
            tab.SetText(name);
            tab.TabSelected += (o, e) =>
            {
                viewPager.SetCurrentItem(actionBar.SelectedNavigationIndex, false);
            };
            return tab;
        }
    }

}
