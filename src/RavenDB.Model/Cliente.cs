namespace RavenDB.Model
{
    public class Cliente : ModelBase
    {
        public Cliente()
        {
            Endereco = new Endereco();
        }

        public int Idade { get; set; }
        public string CPF { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public Endereco Endereco { get; set; }
       
        public Cliente Indicador { get; set; }
        public string IndicadorId { get; set; }
    }
}
