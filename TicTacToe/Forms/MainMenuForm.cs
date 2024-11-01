using TicTacToe.Forms;

namespace TicTacToe
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Handle label click event
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Handle Start Game button click event
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Open OptionsForm when the Options button is clicked
            OptionsForm optionsForm = new OptionsForm();
            optionsForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Handle Exit button click event
            this.Close();
        }

        private void MainMenuForm_Load(object sender, EventArgs e)
        {
            // Handle form load event
        }
    }
}//teste