using Fina.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace Fina.Core.Requests.Transactions
{
    public class CreateTransactionRequest : Request
    {
        [Required(ErrorMessage = "Titulo inálido")]
        [StringLength(80, ErrorMessage = "O título deve ter entre {2} e {0} caracteres.", MinimumLength = 3)]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo transação inválido.")]
        public ETransactionType TransactionType { get; set; }

        [Required(ErrorMessage = "Valor inválido.")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria inválida.")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data inválida.")]
        public DateTime PaidOrReceivedAt { get; set; }
    }
}
