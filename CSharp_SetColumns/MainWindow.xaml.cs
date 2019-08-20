using System.Windows;
using Shared;
using static Shared.Globals;

namespace CSharp_SetColumns {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();

            btnAnonymousType.Click += (s, e) => setAnonymousType();

            btnArray.Click += (s, e) =>
                dg.SetColumns((Person p) => new object[] { p.FirstName, p.LastName, p.DateOfBirth });

            btnSingleProperty.Click += (s, e) =>
                dg.SetColumns((Person p) => p.Email);

            btnParameter.Click += (s, e) =>
                dg.SetColumns((Person p) => p);

            btnStringFormat.Click += (s, e) =>
                dg.SetColumns((Person p) => new {
                    p.ID,
                    p.LastName,
                    p.FirstName,
                    UnformattedBirthDate = p.DateOfBirth,
                    FormattedBirthDate = $"{p.DateOfBirth:D}"
                });


            dg.ItemsSource = PersonList;

            setAnonymousType();
        }

        void setAnonymousType() => dg.SetColumns((Person p) => new { Number = p.ID, Last = p.LastName, First = p.FirstName });
    }

}
