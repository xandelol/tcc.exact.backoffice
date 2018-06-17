using System.Collections.Generic;

namespace Liga.Backoffice.Lanxess.Models.Proxy
{
    public class MeProxy
    {
        /// <summary>
        ///     Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     CPF
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        ///     Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     Settings
        /// </summary>
        public List<SettingProxy> Settings { get; set; }
    }
}