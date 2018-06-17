using System;
using System.ComponentModel.DataAnnotations;

namespace exac.backoffice.Models
{
    public class QuestionViewModel
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     The question statement
        /// </summary>
        [Required(ErrorMessage = "Campo obrigátório.")]
        public string Statement { get; set; }

        public bool IsActive { get; set; } = true;


        public static QuestionViewModel New()
        {
            var model = new QuestionViewModel();
            return model;
        }
    }
}