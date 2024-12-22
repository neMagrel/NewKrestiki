
using System.Data.Common;

namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
            BlockAllCells();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Text = Logic.ToggleButtonState(button.StyleId);
            if (Logic.blockButtonsFlag)
            {
                BlockAllButtons();
                BlockAllCells();
            }
        }

        private void OnCellClicked(object sender, EventArgs e)
        { 
            Button button = (Button)sender;
            int[] cellCoordinates = { Grid.GetRow(button), Grid.GetColumn(button) };
            SetTextOnCell(cellCoordinates, GlobalState.CurrentSettings.playerSign);
           
            Logic.HandMode([Grid.GetRow(button), Grid.GetColumn(button)]);
        }

        public void SetTextOnCell(int[] cellCoordinates, bool sign)
        {
            foreach (var child in Game.Children)
            {

                if (child is Button button)
                {

                    if (Grid.GetRow(button) == cellCoordinates[0] && Grid.GetColumn(button) == cellCoordinates[1])
                    {
                        button.Text = sign == true ? "X" : "O";
                    }
                }
            }
            
        }

        public void BlockAllButtons()
        {
            foreach (Button button in Buttons)
            {
                button.IsEnabled = button.IsEnabled != true;
            }
        }

        public void BlockAllCells()
        {
            foreach (var child in Game.Children)
            {

                if (child is Button button)
                {
                    button.IsEnabled = button.IsEnabled != true;
                }
            }
        }
        
    }

}
