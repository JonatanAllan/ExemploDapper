using System;
using System.Collections.Generic;

namespace ExemploDapperDominio.Entidades
{
    public class Proprietario
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataCadastro { get; set; }

        public virtual ICollection<Animal> Animais { get; set; }
    }
}
