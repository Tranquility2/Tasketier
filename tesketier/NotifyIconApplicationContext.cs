using System;
using System.Reflection;
using System.Windows.Forms;
using tesketier;

namespace tasketier
{
    class NotifyIconApplicationContext : ApplicationContext
    {
        private NotifyIcon notifyIcon = new NotifyIcon();
        private ContextMenuStrip notifyIconContextMenu = new ContextMenuStrip();
        private Configuration configWindow = new Configuration();
        private TaskPanel newTaskPanel = new TaskPanel();
        private Button newTestButton = new Button();

        public NotifyIconApplicationContext()
        {
            newTestButton.Text = "Add";
            newTestButton.Click += new EventHandler(addTaskTester);

            ContextMenuBuild(); 

            notifyIcon.Text = "Testing";
            notifyIcon.Icon = Tasketier.Properties.Resources.tempIcon;
            notifyIcon.ContextMenuStrip = notifyIconContextMenu;
            notifyIcon.MouseUp += new MouseEventHandler(NotifyIcon_MouseUp);
            notifyIcon.Visible = true;
        }

        private void ContextMenuBuild()
        {
            notifyIconContextMenu.Items.Clear();

            ToolStripMenuItem configMenuItem = new ToolStripMenuItem("Configuration", null, new EventHandler(ShowConfig));
            ToolStripMenuItem exitMenuItem = new ToolStripMenuItem("Exit", null, new EventHandler(Exit));

            notifyIconContextMenu.Items.Add(new ToolStripControlHost(newTaskPanel, "taskPanel"));
            notifyIconContextMenu.Items.Add(new ToolStripControlHost(newTestButton, "testButton"));

            notifyIconContextMenu.Items.AddRange(new ToolStripItem[] { configMenuItem, exitMenuItem });
            notifyIconContextMenu.ShowImageMargin = false;
        }

        void NotifyIcon_MouseUp(object sender, MouseEventArgs e)
        {
            ContextMenuBuild();

            if (e.Button == MouseButtons.Left)
            {
                MethodInfo mi = typeof(NotifyIcon).GetMethod("ShowContextMenu", BindingFlags.Instance | BindingFlags.NonPublic);
                mi.Invoke(notifyIcon, null);
            }
        }

        void addTaskTester(object sender, EventArgs e)
        {
            Random random = new Random();

            for (int i = 0; i < random.Next(10); i++)
            {
                newTaskPanel.AddButton("test" + i.ToString(), Tasketier.Properties.Resources.tempIcon.ToBitmap());
            }

            Console.WriteLine("Notify size = {0} Task Panel size = {1}", notifyIconContextMenu.Size, newTaskPanel.Size);
        }

        void ShowConfig(object sender, EventArgs e)
        {
            if (configWindow.Visible)
            {
                configWindow.Activate();
            }
            else
            {
                configWindow.ShowDialog();
            }
        }

        void Exit(object sender, EventArgs e)
        {
            notifyIcon.Visible = false;
            Application.Exit();
        }
    }
}
