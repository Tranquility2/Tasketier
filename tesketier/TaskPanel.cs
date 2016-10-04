using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tesketier
{
    public partial class TaskPanelButtons : UserControl
    {
        public List<Button> buttonList { get; private set; } = new List<Button>();

        public TaskPanelButtons()
        {
            InitializeComponent();
            
            this.AutoSize = true;
        }

        public void AddButton(string text, Image image)
        {
            Button newButton = new Button();

            newButton.Text = text;
            newButton.Image = image;
            newButton.Size = new Size(100, 100);
            newButton.Click += new EventHandler(NewButton_Click);

            buttonList.Add(newButton);
            this.Controls.Add(newButton);

            this.RearrangeButtonsLocation();
            this.Refresh();
        }

        public void RemoveButton(Button button)
        {
            buttonList.Remove(button);
            this.Controls.Remove(button);

            this.RearrangeButtonsLocation();
            this.Refresh();
        }

        private void NewButton_Click(object sender, EventArgs e)
        {
            // TODO add real usage.
            Button currentButton = sender as Button;
            Console.WriteLine("removing {0} @ {1}", buttonList.IndexOf(currentButton), currentButton.Location.ToString());
            RemoveButton(sender as Button);
        }

        public void RearrangeButtonsLocation()
        {
            Button previousButton = new Button();
            Point newPoint = new Point(0, 0);
            int size_counter = 0;

            foreach (Button button in buttonList)
            {
                button.Location = newPoint;

                newPoint.Y += button.Size.Height;

                size_counter++;
                if (size_counter == 4)
                {
                    newPoint.Y = 0;
                    newPoint.X += button.Size.Width;
                    size_counter = 0;
                }
            }
        }
    }
}
