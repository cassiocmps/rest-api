namespace DevIO.Api.Extensions
{
    public class AppSettings
    {
        // chave de criptografia
        public string Secret { get; set; }
        
        public int ExpiracaoHoras { get; set; }
        
        // quem emite a app
        public string Emissor { get; set; }
        
        // quais urls o token é valido
        public string ValidoEm { get; set; }
    }
}