namespace Dominio.Models
{
    public class Produto
    {

        // Propriedades
        public int Id_Produto { get; set; }
        public string Nome { get; set; }

        public Produto(int Id_Produto, string Nome)
        {
            this.Id_Produto = Id_Produto;
            this.Nome = Nome;
        }

        public Produto()
        {

        }
    }
}