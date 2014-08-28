using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace DWM.Models.Entidades
{
    [Table("Veiculo")]
    public class Veiculo
    {
        [Key, Column(Order = 0)]
        [DisplayName("ID")]
        public int associadoId { get; set; }

        [Key, Column(Order = 1)]
        [DisplayName("Placa")]
        public string placa { get; set; }

        [DisplayName("Cor")]
        public string cor { get; set; }

        [DisplayName("Descrição")]
        public string descricao { get; set; }
        
        [DisplayName("Marca")]
        public string marca { get; set; }

        [DisplayName("Condutor")]
        public string condutor { get; set; }

        [DisplayName("Num_Serie")]
        public string num_serie { get; set; }
    }
}