using System.Windows.Forms;

namespace View.Controls
{
    public class Validate
    {
        public static bool TextBoxNull(Form formulario)
        {
            foreach (Control control in formulario.Controls)
            {

                if (control.GetType().Equals(typeof(TextBox)))
                {

                    if (control.Text.Equals(""))
                    {

                        return false;
                    }
                }
            }
            return true;
        }

        public static bool ComboBoxNull(Form formulario)
        {
            foreach (Control control in formulario.Controls)
            {

                if (control.GetType().Equals(typeof(ComboBox)))
                {

                    if (control.Text.Equals(""))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool NumUpDownNull(Form formulario)
        {
            foreach (Control control in formulario.Controls)
            {

                if (control.GetType().Equals(typeof(NumericUpDown)))
                {

                    //if (control.Text.Equals(""))
                    if (control.Text.Equals("0.00") | control.Text.Equals("0"))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
