namespace DocGuideGen2.Models
{
    public class Guide
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Registry { get; set; }
        public string Rut { get; set; }
        public int Type { get; set; } // 1 = Guia, 2 = Assistente de Guia
        public string DisplayType { get; set; } // Para exibir "Guia" ou "Assistente de Guia"
    }
}
