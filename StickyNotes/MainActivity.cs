using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using StickyNotes.Resources.enums;
using System.Collections.Generic;

namespace StickyNotes
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        private StickyNotes.MainLogic.StickyNotesManager stickyNotesManager = StickyNotes.MainLogic.StickyNotesManager.Instance;
        AppCompatEditText editor;
        Dictionary<ButtonNames, Button> buttons = new Dictionary<ButtonNames, Button>();
        TextView results;

        protected void Initialize()
        {
            editor = FindViewById<AppCompatEditText>(Resource.Id.data);
            buttons.Add(ButtonNames.AddNote, FindViewById<Button>(Resource.Id.add_note));
            buttons.Add(ButtonNames.RemoveNotes, FindViewById<Button>(Resource.Id.remove_notes));
            results = FindViewById<TextView>(Resource.Id.results);

            editor.TooltipText = "Please enter a note";
            buttons[ButtonNames.AddNote].Click += AddNote_Click;
            buttons[ButtonNames.RemoveNotes].Click += RemoveNotes_Click;
            buttons[ButtonNames.RemoveNotes].Visibility = ViewStates.Gone;
        }

        private void RemoveNotes_Click(object sender, System.EventArgs e)
        {
            results.Text = stickyNotesManager.RemoveNotes();
        }

        private void AddNote_Click(object sender, System.EventArgs e)
        {
            stickyNotesManager.AddNote(editor.Text);
            editor.Text = "";
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            this.Initialize();
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    buttons[ButtonNames.AddNote].Visibility = ViewStates.Visible;
                    buttons[ButtonNames.RemoveNotes].Visibility = ViewStates.Gone;
                    editor.Visibility = ViewStates.Visible;
                    results.Visibility = ViewStates.Gone;
                    return true;
                case Resource.Id.navigation_dashboard:
                    results.Text = stickyNotesManager.GetNotes();
                    editor.Visibility = ViewStates.Gone;
                    results.Visibility = ViewStates.Visible;
                    buttons[ButtonNames.AddNote].Visibility = ViewStates.Gone;
                    buttons[ButtonNames.RemoveNotes].Visibility = ViewStates.Visible;
                    return true;
            }
            return false;
        }
    }
}

