using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Xamarin.Forms.Sandbox
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			Device.SetFlags(new[] { "Shell_Experimental", "Visual_Experimental", "CollectionView_Experimental" });

			var picker = new Picker() { TitleColor = Color.Green, Title = "I am a title", Items = { "1", "2", "3" } };
			var button = new Button()
			{
				Text = "Clear",
				Command = new Command(() =>
				{
					picker.SelectedItem = null;
				})
			};

			Func<View> sl = () => CreateStackLayout(
				new View[] {
					new DatePicker(){ TextColor = Color.Blue,  Title = "Date Picker Title", TitleColor = Color.Red },
					new TimePicker(){ TextColor = Color.Blue,  Title = "Time Picker Title", TitleColor = Color.Red },
					picker,
					button,
					new Entry() { TextColor = Color.Blue, Placeholder = "Entry field" } });

			//MainPage = CreateListViewPage(sl);
			MainPage = CreateContentPage(sl());

			MainPage.Visual = VisualMarker.Material;
		}

		ContentPage CreateContentPage(View view) =>
			new ContentPage() { Content = view };


		ContentPage CreateListViewPage(Func<View> template)
		{
			var listView = new ListView(ListViewCachingStrategy.RecycleElement);
			listView.RowHeight = 500;
			listView.ItemsSource = Enumerable.Range(0, 1).ToList();
			listView.ItemTemplate = new DataTemplate(() =>
			{
				ViewCell cell = new ViewCell();
				cell.View = template();
				return cell;
			});

			return new ContentPage()
			{
				Content = listView
			};
		}


		StackLayout CreateStackLayout(IEnumerable<View> children)
		{
			var sl = new StackLayout();
			foreach (var child in children)
				sl.Children.Add(child);

			return sl;
		}

		ContentPage CreateStackLayoutPage(IEnumerable<View> children)
		{
			return new ContentPage()
			{
				Content = CreateStackLayout(children),
				Visual = VisualMarker.Material
			};
		}
	}
}
