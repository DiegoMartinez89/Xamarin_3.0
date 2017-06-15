using SALLab04;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCLProject
{
    public class AppValidator
    {
        IDialog dialog;
        public string Email { get; set; }
        public string Password { get; set; }
        public string Device { get; set; }

        public AppValidator(IDialog platformDialog)
        {
            dialog = platformDialog;
        }

        public async void Validate()
        {
            string result;
            var serviceClient = new ServiceClient();
            var SvcResult = await serviceClient.ValidateAsync(Email, Password, Device);
            result = $"{SvcResult.Status}\n{SvcResult.Fullname}\n{SvcResult.Token}";
            dialog.Show(result);
        }
    }
}
