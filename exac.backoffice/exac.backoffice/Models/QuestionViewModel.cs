using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Liga.Backoffice.Lanxess.Models
{
    public class QuestionViewModel
    {
        /// <summary>
        ///     Primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     The question statement
        /// </summary>
        [Required(ErrorMessage = "Campo obrigátório.")]
        public string Statement { get; set; }

        public string ImageBase64 { get; set; }

        public string UrlImage { get; set; }
        
        /// <summary>
        ///     The possible answers to this question.
        /// </summary>
        public List<AnswerViewModel> Answers { get; set; }

        public bool IsActive { get; set; } = true;


        public static QuestionViewModel New()
        {
            var model = new QuestionViewModel();
            model.Answers = new List<AnswerViewModel>()
            {
                new AnswerViewModel()
                {
                    IsCorrect = true
                },
                new AnswerViewModel(),
                new AnswerViewModel(),
                new AnswerViewModel()
            };

            return model;
        }
    }
}